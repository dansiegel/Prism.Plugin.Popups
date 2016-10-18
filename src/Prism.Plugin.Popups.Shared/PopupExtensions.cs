using System.Collections.Generic;
using Xamarin.Forms;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System.Threading.Tasks;
using System.Linq;
using Prism.Mvvm;

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

        public static async Task ClearPopupStack( this INavigationService navigationService, NavigationParameters parameters = null, bool animated = true )
        {
            while( s_popupStack.Count > 0 )
            {
                await PopupGoBackAsync( navigationService, parameters, animated );
            }
        }

        public static async Task PopupGoBackAsync( this INavigationService navigationService, NavigationParameters parameters = null, bool animate = true )
        {
            if( s_popupStack.Count == 0 ) return;

            var page = s_popupStack.Last();
            HandleINavigationAware( page, parameters, navigatedTo: false );

            await PopupNavigation.PopAsync( animate );

            Page currentPage = s_popupStack.LastOrDefault();
            if( currentPage == null ) currentPage = GetCurrentPage();

            HandleINavigationAware( currentPage, parameters, navigatedTo: true );
        }

        public static async Task PushPopupPageAsync( this INavigationService navigationService, string name, NavigationParameters parameters = null, bool animated = true )
        {
            var page = CreatePopupPageByName( name );
            ViewModelLocator.SetAutowireViewModel( page, ViewModelLocator.GetAutowireViewModel( page ) ?? true );
            await PopupNavigation.PushAsync( page, animated );
            HandleINavigationAware( page, parameters, navigatedTo: true );
        }

        public static Task PushPopupPageAsync( this INavigationService navigationService, string name, string key, object param, bool animated = true )
        {
            var parameters = new NavigationParameters()
            {
                { key, param }
            };
            return navigationService.PushPopupPageAsync( name, parameters, animated );
        }

        private static void HandleINavigationAware( Page page, NavigationParameters parameters, bool navigatedTo )
        {
            if( page == null ) return;

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
            }
        }

        private static Page GetCurrentPage()
        {
            Page page = null;
            if( s_navigation.ModalStack.Count > 0 )
                page = s_navigation.ModalStack.LastOrDefault();
            else
                page = s_navigation.NavigationStack.LastOrDefault();

            if( page == null )
                page = Application.Current.MainPage;

            return page;
        }
    }
}
