using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Prism.Behaviors;
using Prism.Common;
using Prism.Ioc;
using Prism.Logging;
using Prism.Navigation;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace Prism.Plugin.Popups
{
#if __IOS__
    [Foundation.Preserve(AllMembers = true)]
#elif __ANDROID__
    [Android.Runtime.Preserve(AllMembers = true)]
#endif
    public class PopupPageNavigationService : PageNavigationService
    {
        protected IPopupNavigation _popupNavigation { get; }

        public PopupPageNavigationService(IPopupNavigation popupNavigation, IContainerExtension container,
                                          IApplicationProvider applicationProvider, IPageBehaviorFactory pageBehaviorFactor,
                                          ILoggerFacade logger)
            : base(container, applicationProvider, pageBehaviorFactor, logger)
        {
            _popupNavigation = popupNavigation;
        }

        protected override async Task<INavigationResult> GoBackInternal(INavigationParameters parameters, bool? useModalNavigation, bool animated)
        {
            INavigationResult result = null;
            try
            {
                NavigationSource = PageNavigationSource.NavigationService;

                switch (PopupUtilities.TopPage(_popupNavigation, _applicationProvider))
                {
                    case PopupPage popupPage:
                        var segmentParameters = UriParsingHelper.GetSegmentParameters(null, parameters);
                        ((INavigationParametersInternal)segmentParameters).Add("__NavigationMode", NavigationMode.Back);
                        var previousPage = PopupUtilities.GetOnNavigatedToTarget(_popupNavigation, _applicationProvider);

                        PageUtilities.OnNavigatingTo(previousPage, segmentParameters);
                        await DoPop(popupPage.Navigation, false, animated);

                        if (popupPage != null)
                        {
                            PageUtilities.InvokeViewAndViewModelAction<IActiveAware>(popupPage, a => a.IsActive = false);
                            PageUtilities.OnNavigatedFrom(popupPage, segmentParameters);
                            PageUtilities.OnNavigatedTo(previousPage, segmentParameters);
                            PageUtilities.InvokeViewAndViewModelAction<IActiveAware>(previousPage, a => a.IsActive = true);
                            PageUtilities.DestroyPage(popupPage);
                            result = new NavigationResult { Success = true };
                            break;
                        }
                        throw new NullReferenceException("The PopupPage was null following the Pop");
                    default:
                        result = await base.GoBackInternal(parameters, useModalNavigation, animated);
                        break;
                }
            }
            catch (Exception e)
            {
#if DEBUG
                System.Diagnostics.Debugger.Break();
#endif
                _logger.Log(e.ToString(), Category.Exception, Priority.High);
                result = new NavigationResult { Success = false, Exception = e }; ;
            }
            finally
            {
                NavigationSource = PageNavigationSource.Device;
            }
            return result;
        }

        protected override async Task<Page> DoPop(INavigation navigation, bool useModalNavigation, bool animated)
        {
            if (_popupNavigation.PopupStack.Count > 0)
            {
                await _popupNavigation.PopAsync(animated);
                return null;
            }

            return await base.DoPop(navigation, useModalNavigation, animated);
        }

        protected override async Task DoPush(Page currentPage, Page page, bool? useModalNavigation, bool animated, bool insertBeforeLast = false, int navigationOffset = 0)
        {
            switch (page)
            {
                case PopupPage popup:
                    await _popupNavigation.PushAsync(popup, animated);
                    break;
                default:
                    await base.DoPush(currentPage, page, useModalNavigation, animated, insertBeforeLast, navigationOffset);
                    if (_popupNavigation.PopupStack.Any())
                        await _popupNavigation.PopAllAsync(false);
                    break;
            }
        }

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
