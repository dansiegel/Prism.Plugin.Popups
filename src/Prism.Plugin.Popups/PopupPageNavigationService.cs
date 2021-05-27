﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Prism.Behaviors;
using Prism.Common;
using Prism.Ioc;
using Prism.Navigation;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace Prism.Plugin.Popups
{
    /// <summary>
    ///     Implements the <see cref="INavigationService" /> for working with <see cref="PopupPage" />
    /// </summary>
    public class PopupPageNavigationService : PageNavigationService
    {
        /// <summary>
        ///     Gets the <see cref="IPopupNavigation" /> service.
        /// </summary>
        protected IPopupNavigation _popupNavigation { get; }

        /// <summary>
        ///     Creates a new instance of the <see cref="PopupPageNavigationService" />
        /// </summary>
        /// <param name="popupNavigation"></param>
        /// <param name="container"></param>
        /// <param name="applicationProvider"></param>
        /// <param name="pageBehaviorFactory"></param>
        public PopupPageNavigationService(IPopupNavigation popupNavigation, IContainerProvider container,
                                          IApplicationProvider applicationProvider, IPageBehaviorFactory pageBehaviorFactory)
            : base(container, applicationProvider, pageBehaviorFactory)
        {
            _popupNavigation = popupNavigation;
        }

        /// <inheritdoc />
        protected override async Task<INavigationResult> GoBackInternal(INavigationParameters parameters, bool? useModalNavigation, bool animated)
        {
            INavigationResult result = null;
            try
            {
                NavigationSource = PageNavigationSource.NavigationService;

                switch (PopupUtilities.TopPage(_popupNavigation, _applicationProvider))
                {
                    case PopupPage popupPage:
                        {
                            INavigationParameters segmentParameters = UriParsingHelper.GetSegmentParameters(null, parameters);
                            ((INavigationParametersInternal)segmentParameters).Add("__NavigationMode", NavigationMode.Back);
                            Page previousPage = PopupUtilities.GetOnNavigatedToTarget(_popupNavigation, _applicationProvider);

                            await DoPop(popupPage.Navigation, false, animated);

                            PageUtilities.InvokeViewAndViewModelAction<IActiveAware>(popupPage, a => a.IsActive = false);
                            PageUtilities.OnNavigatedFrom(popupPage, segmentParameters);
                            PageUtilities.OnNavigatedTo(previousPage, segmentParameters);
                            PageUtilities.InvokeViewAndViewModelAction<IActiveAware>(previousPage, a => a.IsActive = true);
                            PageUtilities.DestroyPage(popupPage);
                            result = new NavigationResult {Success = true};
                            break;
                        }
                    default:
                        {
                            result = await base.GoBackInternal(parameters, useModalNavigation, animated);
                            break;
                        }
                }
            }
            catch (Exception e)
            {
#if DEBUG
                Debugger.Break();
#endif
                result = new NavigationResult {Success = false, Exception = e};
            }
            finally
            {
                NavigationSource = PageNavigationSource.Device;
            }
            return result;
        }

        #region MasterDetailPage Hack

        /// <inheritdoc />
        protected override async Task ProcessNavigationForMasterDetailPage(MasterDetailPage currentPage, string nextSegment, Queue<string> segments, INavigationParameters parameters, bool? useModalNavigation, bool animated)
        {
            var isPresented = GetMasterDetailPageIsPresented(currentPage);

            Page detail = currentPage.Detail;
            if (detail == null)
            {
                Page newDetail = CreatePageFromSegment(nextSegment);
                await ProcessNavigation(newDetail, segments, parameters, useModalNavigation, animated);
                await DoNavigateAction(null, nextSegment, newDetail, parameters, onNavigationActionCompleted: p =>
                                                                                                              {
                                                                                                                  currentPage.IsPresented = isPresented;
                                                                                                                  currentPage.Detail = newDetail;
                                                                                                              });
                return;
            }

            if (useModalNavigation.HasValue && useModalNavigation.Value)
            {
                Page nextPage = CreatePageFromSegment(nextSegment);
                await ProcessNavigation(nextPage, segments, parameters, useModalNavigation, animated);
                await DoNavigateAction(currentPage, nextSegment, nextPage, parameters, async () =>
                                                                                       {
                                                                                           currentPage.IsPresented = isPresented;
                                                                                           await DoPush(currentPage, nextPage, true, animated);
                                                                                       });
                return;
            }

            Type nextSegmentType = PageNavigationRegistry.GetPageType(UriParsingHelper.GetSegmentName(nextSegment));

            //we must recreate the NavigationPage every time or the transitions on iOS will not work properly, unless we meet the two scenarios below
            var detailIsNavPage = false;
            var reuseNavPage = false;
            if (detail is NavigationPage navPage)
            {
                detailIsNavPage = true;

                //we only care if we the next segment is also a NavigationPage.
                if (IsSameOrSubclassOf<NavigationPage>(nextSegmentType))
                {
                    //first we check to see if we are being forced to reuse the NavPage by checking the interface
                    reuseNavPage = !GetClearNavigationPageNavigationStack(navPage);

                    if (!reuseNavPage)
                    {
                        //if we weren't forced to reuse the NavPage, then let's check the NavPage.CurrentPage against the next segment type as we don't want to recreate the entire nav stack
                        //just in case the user is trying to navigate to the same page which may be nested in a NavPage
                        Type nextPageType = PageNavigationRegistry.GetPageType(UriParsingHelper.GetSegmentName(segments.Peek()));
                        Type currentPageType = navPage.CurrentPage.GetType();
                        if (nextPageType == currentPageType)
                        {
                            reuseNavPage = true;
                        }
                    }
                }
            }

            if ((detailIsNavPage && reuseNavPage) || (!detailIsNavPage && detail.GetType() == nextSegmentType))
            {
                await ProcessNavigation(detail, segments, parameters, useModalNavigation, animated);
                await DoNavigateAction(null, nextSegment, detail, parameters, onNavigationActionCompleted: p =>
                                                                                                           {
                                                                                                               if (detail is TabbedPage && nextSegment.Contains(KnownNavigationParameters.SelectedTab))
                                                                                                               {
                                                                                                                   INavigationParameters segmentParams = UriParsingHelper.GetSegmentParameters(nextSegment);
                                                                                                                   SelectPageTab(detail, segmentParams);
                                                                                                               }

                                                                                                               currentPage.IsPresented = isPresented;
                                                                                                           });
            }
            else
            {
                Page newDetail = CreatePageFromSegment(nextSegment);
                INavigationParameters segmentParameters = UriParsingHelper.GetSegmentParameters(nextSegment, parameters);
                await ProcessNavigation(newDetail, segments, segmentParameters, newDetail is NavigationPage ? false : true, animated);

                await DoNavigationActionForNewDetail(currentPage, newDetail, detail, detailIsNavPage, isPresented, animated, nextSegment, segmentParameters);
            }
        }

        private async Task DoNavigationActionForNewDetail(MasterDetailPage currentPage, Page newDetail, Page oldDetail, bool detailIsNavPage, bool isPresented, bool animated, string nextSegment, INavigationParameters parameters)
        {
            if (newDetail is PopupPage)
            {
                await DoNavigateAction(oldDetail, nextSegment, newDetail, parameters, () =>
                                                                                      {
                                                                                          if (detailIsNavPage)
                                                                                          {
                                                                                              OnNavigatedFrom(((NavigationPage)oldDetail).CurrentPage, parameters);
                                                                                          }

                                                                                          currentPage.IsPresented = false;
                                                                                          return DoPush(currentPage, newDetail, false, animated);
                                                                                      });
            }
            else
            {
                await DoNavigateAction(oldDetail, nextSegment, newDetail, parameters, onNavigationActionCompleted: p =>
                                                                                                                   {
                                                                                                                       if (detailIsNavPage)
                                                                                                                       {
                                                                                                                           OnNavigatedFrom(((NavigationPage)oldDetail).CurrentPage, parameters);
                                                                                                                       }

                                                                                                                       currentPage.IsPresented = isPresented;
                                                                                                                       currentPage.Detail = newDetail;
                                                                                                                       PageUtilities.DestroyPage(oldDetail);
                                                                                                                   });
            }
        }

        private static void OnNavigatedFrom(Page fromPage, INavigationParameters parameters)
        {
            PageUtilities.OnNavigatedFrom(fromPage, parameters);

            if (fromPage is TabbedPage tabbedPage)
            {
                if (tabbedPage.CurrentPage is NavigationPage navigationPage)
                {
                    PageUtilities.OnNavigatedFrom(navigationPage.CurrentPage, parameters);
                }
                else if (tabbedPage.BindingContext != tabbedPage.CurrentPage.BindingContext)
                {
                    PageUtilities.OnNavigatedFrom(tabbedPage.CurrentPage, parameters);
                }
            }
            else if (fromPage is CarouselPage carouselPage)
            {
                PageUtilities.OnNavigatedFrom(carouselPage.CurrentPage, parameters);
            }
        }

        private static void SelectPageTab(Page page, INavigationParameters parameters)
        {
            if (page is TabbedPage tabbedPage)
            {
                TabbedPageSelectTab(tabbedPage, parameters);
            }
            else if (page is CarouselPage carouselPage)
            {
                CarouselPageSelectTab(carouselPage, parameters);
            }
        }

        private static void TabbedPageSelectTab(TabbedPage tabbedPage, INavigationParameters parameters)
        {
            var selectedTab = parameters?.GetValue<string>(KnownNavigationParameters.SelectedTab);
            if (!string.IsNullOrWhiteSpace(selectedTab))
            {
                Type selectedTabType = PageNavigationRegistry.GetPageType(UriParsingHelper.GetSegmentName(selectedTab));

                var childFound = false;
                foreach (Page child in tabbedPage.Children)
                {
                    if (!childFound && child.GetType() == selectedTabType)
                    {
                        tabbedPage.CurrentPage = child;
                        childFound = true;
                    }

                    if (child is NavigationPage)
                    {
                        if (!childFound && ((NavigationPage)child).CurrentPage.GetType() == selectedTabType)
                        {
                            tabbedPage.CurrentPage = child;
                            childFound = true;
                        }
                    }
                }
            }
        }

        private static void CarouselPageSelectTab(CarouselPage carouselPage, INavigationParameters parameters)
        {
            var selectedTab = parameters?.GetValue<string>(KnownNavigationParameters.SelectedTab);
            if (!string.IsNullOrWhiteSpace(selectedTab))
            {
                Type selectedTabType = PageNavigationRegistry.GetPageType(UriParsingHelper.GetSegmentName(selectedTab));

                foreach (ContentPage child in carouselPage.Children)
                {
                    if (child.GetType() == selectedTabType)
                    {
                        carouselPage.CurrentPage = child;
                    }
                }
            }
        }

        internal static bool IsSameOrSubclassOf<T>(Type potentialDescendant)
        {
            if (potentialDescendant == null)
            {
                return false;
            }

            Type potentialBase = typeof(T);

            return potentialDescendant.GetTypeInfo().IsSubclassOf(potentialBase)
                   || potentialDescendant == potentialBase;
        }

        #endregion MasterDetailPage Hack

        /// <inheritdoc />
        protected override async Task<Page> DoPop(INavigation navigation, bool useModalNavigation, bool animated)
        {
            if (_popupNavigation.PopupStack.Count > 0 || _page is PopupPage)
            {
                await _popupNavigation.PopAsync(animated);
                return null;
            }

            return await base.DoPop(navigation, useModalNavigation, animated);
        }

        /// <inheritdoc />
        protected override async Task DoPush(Page currentPage, Page page, bool? useModalNavigation, bool animated, bool insertBeforeLast = false, int navigationOffset = 0)
        {
            switch (page)
            {
                case PopupPage popup:
                    if (_applicationProvider.MainPage is null)
                    {
                        throw new PopupNavigationException(popup);
                    }

                    await _popupNavigation.PushAsync(popup, animated);
                    break;
                default:
                    if (_popupNavigation.PopupStack.Any())
                    {
                        foreach (PopupPage pageToPop in _popupNavigation.PopupStack)
                        {
                            PageUtilities.DestroyPage(pageToPop);
                        }

                        await _popupNavigation.PopAllAsync(animated);
                    }

                    if (currentPage is PopupPage)
                    {
                        currentPage = PageUtilities.GetCurrentPage(_applicationProvider.MainPage);
                    }

                    await base.DoPush(currentPage, page, useModalNavigation, animated, insertBeforeLast, navigationOffset);
                    break;
            }
        }

        /// <inheritdoc />
        protected override Page GetCurrentPage()
        {
            if (_popupNavigation.PopupStack.Any())
            {
                return _popupNavigation.PopupStack.LastOrDefault();
            }

            return base.GetCurrentPage();
        }
    }
}