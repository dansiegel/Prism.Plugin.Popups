using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Prism.Mvvm;
using Prism.Plugin.Popups;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Prism.Common;
using Prism.Plugin.Popups.Extensions;
using Rg.Plugins.Popup.Contracts;
using Prism.Ioc;

namespace Prism.Navigation
{
    public static partial class PopupExtensions
    {

        static INavigation s_navigation
        {
            get { return Application.Current.MainPage.Navigation; }
        }

        static IPopupNavigation s_popupNavigation => PopupNavigation.Instance;

        static IReadOnlyList<PopupPage> s_popupStack => s_popupNavigation.PopupStack;

        public static Task ClearPopupStackAsync(this INavigationService navigationService, string key, object param, bool animated = true) =>
            navigationService.ClearPopupStackAsync(GetNavigationParameters(key, param, NavigationMode.Back), animated);

        public static async Task ClearPopupStackAsync(this INavigationService navigationService, NavigationParameters parameters = null, bool animated = true)
        {
            while (s_popupStack.Count > 0)
            {
                await navigationService.GoBackAsync(parameters, animated: animated);
            }
        }

        //[Obsolete("This method is being deprecated in favor of Registering the PopupNavigationService and using INavigationService.GoBackAsync")]
        //public static Task PopupGoBackAsync( this INavigationService navigationService, string key, object param, bool animated = true ) =>
        //navigationService.PopupGoBackAsync( GetNavigationParameters( key, param, NavigationMode.Back ), animated );

        //[Obsolete("This method is being deprecated in favor of Registering the PopupNavigationService and using INavigationService.GoBackAsync")]
        //public static async Task PopupGoBackAsync( this INavigationService navigationService, NavigationParameters parameters = null, bool animate = true )
        //{
        //    if( s_popupStack.Count == 0 ) return;

        //    var page = s_popupStack.Last();

        //    EnsureParametersContainsMode( parameters ?? ( parameters = PopupUtilities.CreateBackNavigationParameters() ), NavigationMode.Back);

        //    HandleINavigatedAware( page, parameters, navigatedTo: false );
        //    HandleIDestructiblePage( page );

        //    await s_popupNavigation.PopAsync( animate );

        //    Page currentPage = s_popupStack.LastOrDefault();
        //    if( currentPage == null ) currentPage = GetCurrentPage();

        //    HandleINavigatedAware( currentPage, parameters, navigatedTo: true );
        //}

        //[Obsolete("This method is being deprecated in favor of Registering the PopupNavigationService and using INavigationService.NavigateAsync")]
        //public static async Task PushPopupPageAsync( this INavigationService navigationService, string name, NavigationParameters parameters = null, bool animated = true )
        //{
        //    try
        //    {
        //        var page = CreatePopupPageByName( name );

        //        // Ensure resolved page is not null. This could happen if the resolved page wasn't a Popup Page
        //        if( page == null )
        //            throw new PopupPageMissingException( $"It seems something went wrong. We seem to have resolved a page for '{name}', but it doesn't seem to be a Popup Page.", new ArgumentNullException( nameof( page ) ) );

        //        // Make sure the VML is set to either True or False
        //        if( ViewModelLocator.GetAutowireViewModel( page ) == null )
        //            ViewModelLocator.SetAutowireViewModel( page, true );

        //        var currentPage = GetCurrentPage();

        //        EnsureParametersContainsMode( parameters ?? ( parameters = PopupUtilities.CreateNewNavigationParameters() ), NavigationMode.New );

        //        HandleINavigatingAware( page, parameters );
        //        await s_popupNavigation.PushAsync( page, animated );
        //        HandleINavigatedAware( currentPage, page, parameters );
        //    }
        //    catch( Exception ex )
        //    {
        //        s_logger.Log( ex.ToString(), Logging.Category.Exception, Logging.Priority.High );
        //        throw;
        //    }
        //}

        //[Obsolete("This method is being deprecated in favor of Registering the PopupNavigationService and using INavigationService.NavigateAsync")]
        //public static Task PushPopupPageAsync( this INavigationService navigationService, string name, string key, object param, bool animated = true ) =>
        //navigationService.PushPopupPageAsync( name, GetNavigationParameters( key, param, NavigationMode.New ), animated );

        private static void HandleINavigatingAware(PopupPage page, NavigationParameters parameters) =>
            PageUtilities.InvokeViewAndViewModelAction<INavigatingAware>(page, (view) => view.OnNavigatingTo(parameters));

        private static void HandleINavigatedAware(Page pageFrom, Page pageTo, NavigationParameters parameters)
        {
            HandleINavigatedAware(pageFrom, parameters, navigatedTo: false);
            HandleINavigatedAware(pageTo, parameters, navigatedTo: true);
        }

        private static void HandleINavigatedAware(Page page, NavigationParameters parameters, bool navigatedTo)
        {
            if (page == null) return;
            if (parameters == null)
            {
                parameters = navigatedTo ?
                    PopupUtilities.CreateNewNavigationParameters() :
                    PopupUtilities.CreateBackNavigationParameters();
            }

            PageUtilities.InvokeViewAndViewModelAction<INavigatedAware>(page, (view) =>
            {
                if (navigatedTo)
                    view.OnNavigatedTo(parameters);
                else
                    view.OnNavigatedFrom(parameters);
            });

            PageUtilities.InvokeViewAndViewModelAction<IActiveAware>(page, (view) => view.IsActive = navigatedTo);
        }

        private static void HandleIDestructiblePage(PopupPage page) =>
            PageUtilities.InvokeViewAndViewModelAction<IDestructible>(page, (view) => view.Destroy());

        private static Page GetCurrentPage()
        {
            Page page = null;
            if (s_popupNavigation.PopupStack.Count > 0)
                page = s_popupNavigation.PopupStack.LastOrDefault();
            else if (s_navigation.ModalStack.Count > 0)
                page = s_navigation.ModalStack.LastOrDefault();
            else
                page = s_navigation.NavigationStack.LastOrDefault();

            if (page == null)
                page = Application.Current.MainPage;

            return page.GetDisplayedPage();
        }

        //private static void EnsureParametersContainsMode( NavigationParameters parameters, NavigationMode mode )
        //{
        //    if( parameters.ContainsKey( KnownNavigationParameters.NavigationMode ) ) return;

        //    parameters.Add( KnownNavigationParameters.NavigationMode, mode );
        //}

        private static NavigationParameters GetNavigationParameters(string key, object param, NavigationMode mode) =>
            new NavigationParameters()
            {
                { key, param }
            }.AddNavigationMode(mode);

        //private static void VerifyPageIsRegistered( string name )
        //{
        //    if( !IsPageRegistered( name ) )
        //        throw new PopupPageMissingException( name );
        //}
    }
}
