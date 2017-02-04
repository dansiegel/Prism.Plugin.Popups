using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prism.Mvvm;
using Prism.Plugin.Popups;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Prism.Navigation
{
    public static partial class PopupExtensions
    {
        static INavigation s_navigation
        {
            get { return Application.Current.MainPage.Navigation; }
        }

        static IReadOnlyList<PopupPage> s_popupStack
        {
            get { return PopupNavigation.PopupStack; }
        }

        [Obsolete( "Use ClearPopupStackAsync" )]
        public static Task ClearPopupStack( this INavigationService navigationService, NavigationParameters parameters = null, bool animated = true ) =>
            navigationService.ClearPopupStackAsync( parameters, animated );

        public static Task ClearPopupStackAsync( this INavigationService navigationService, string key, object param, bool animated = true ) =>
            navigationService.ClearPopupStackAsync( GetNavigationParameters( key, param ), animated );

        public static async Task ClearPopupStackAsync( this INavigationService navigationService, NavigationParameters parameters = null, bool animated = true )
        {
            while( s_popupStack.Count > 0 )
            {
                await PopupGoBackAsync( navigationService, parameters, animated );
            }
        }

        public static Task PopupGoBackAsync( this INavigationService navigationService, string key, object param, bool animated = true ) =>
            navigationService.PopupGoBackAsync( GetNavigationParameters( key, param ), animated );

        public static async Task PopupGoBackAsync( this INavigationService navigationService, NavigationParameters parameters = null, bool animate = true )
        {
            if( s_popupStack.Count == 0 ) return;

            var page = s_popupStack.Last();
            HandleINavigatedAware( page, parameters, navigatedTo: false );

            await PopupNavigation.PopAsync( animate );

            Page currentPage = s_popupStack.LastOrDefault();
            if( currentPage == null ) currentPage = GetCurrentPage();

            HandleINavigatedAware( currentPage, parameters, navigatedTo: true );
        }

        public static async Task PushPopupPageAsync( this INavigationService navigationService, string name, NavigationParameters parameters = null, bool animated = true )
        {
            try
            {
                var page = CreatePopupPageByName( name );

                // Ensure resolved page is not null. This could happen if the resolved page wasn't a Popup Page
                if( page == null )
                    throw new PopupPageMissingException( $"It seems something went wrong. We seem to have resolved a page for '{name}', but it doesn't seem to be a Popup Page.", new ArgumentNullException( nameof( page ) ) );

                // Make sure the VML is set to either True or False
                if( ViewModelLocator.GetAutowireViewModel( page ) == null )
                    ViewModelLocator.SetAutowireViewModel( page, true );
                
                var currentPage = GetCurrentPage();
                HandleINavigatingAware( page, parameters );
                await PopupNavigation.PushAsync( page, animated );
                HandleINavigatedAware( currentPage, page, parameters );
            }
            catch( Exception ex )
            {
                s_logger.Log( ex.ToString(), Logging.Category.Exception, Logging.Priority.High );
                throw;
            }
        }

        public static Task PushPopupPageAsync( this INavigationService navigationService, string name, string key, object param, bool animated = true ) =>
            navigationService.PushPopupPageAsync( name, GetNavigationParameters( key, param ), animated );

        private static void HandleINavigatingAware( PopupPage page, NavigationParameters parameters )
        {
            //TODO: This will need to be updated to INavigatingAware once Pre2 is released
            ( page as INavigationAware )?.OnNavigatingTo( parameters );
            ( page?.BindingContext as INavigationAware )?.OnNavigatingTo( parameters );
        }

        private static void HandleINavigatedAware( Page pageFrom, Page pageTo, NavigationParameters parameters )
        {
            HandleINavigatedAware( pageFrom, parameters, navigatedTo: false );
            HandleINavigatedAware( pageTo, parameters, navigatedTo: true );
        }

        private static void HandleINavigatedAware( Page page, NavigationParameters parameters, bool navigatedTo )
        {
            if( page == null ) return;

            //TODO: Change this to INavigatedAware once pre2 is released
            var pageAware = page as INavigationAware;
            var contextAware = page.BindingContext as INavigationAware;

            if( pageAware == null && contextAware == null ) return;

            if( parameters == null ) parameters = new NavigationParameters();

            if( navigatedTo )
            {
                pageAware?.OnNavigatedTo( parameters );
                contextAware?.OnNavigatedTo( parameters );
            }
            else
            {
                pageAware?.OnNavigatedFrom( parameters );
                contextAware?.OnNavigatedFrom( parameters );
                HandleIDestructiblePage( page );
            }
        }

        private static void HandleIDestructiblePage( Page page )
        {
            ( page as IDestructible )?.Destroy();
            ( page?.BindingContext as IDestructible )?.Destroy();
        }

        private static Page GetCurrentPage()
        {
            Page page = null;
            if( PopupNavigation.PopupStack.Count > 0 )
                page = PopupNavigation.PopupStack.LastOrDefault();
            else if( s_navigation.ModalStack.Count > 0 )
                page = s_navigation.ModalStack.LastOrDefault();
            else
                page = s_navigation.NavigationStack.LastOrDefault();

            if( page == null )
                page = Application.Current.MainPage;

            return FilterPage( page );
        }

        private static Page FilterPage( Page page )
        {
            if( page == null ) return page;
            var startPage = page;

            while( page is MasterDetailPage || page is NavigationPage || page.GetType() == typeof( MultiPage<> ) )
            {
                if( page.GetType() == typeof( MultiPage<> ) )
                {
                    page = ( page as MultiPage<Page> ).CurrentPage;
                }
                else if( page.GetType() == typeof( MasterDetailPage ) )
                {
                    page = ( page as MasterDetailPage ).Detail;
                }
                else if( page.GetType() == typeof( NavigationPage ) )
                {
                    page = ( page as NavigationPage ).CurrentPage;
                }
            }

            return page ?? startPage;
        }

        private static NavigationParameters GetNavigationParameters( string key, object param ) =>
            new NavigationParameters()
            {
                { key, param }
            };

        private static void VerifyPageIsRegistered( string name )
        {
            if( !IsPageRegistered( name ) )
                throw new PopupPageMissingException( name );
        }

    }
}
