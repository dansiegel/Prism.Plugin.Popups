using System;
using System.Threading.Tasks;
using Prism.Common;
using Prism.Logging;
using Prism.Navigation;
using Xamarin.Forms;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System.Linq;
using System.Reflection;

namespace Prism.Plugin.Popups
{
    public abstract class PopupPageNavigationServiceBase : PageNavigationService
    {
        public PopupPageNavigationServiceBase( IApplicationProvider applicationProvider, ILoggerFacade logger ) 
            : base( applicationProvider, logger )
        {
        }

        protected override async Task<Page> DoPop(INavigation navigation, bool useModalNavigation, bool animated)
        {
            if(PopupNavigation.PopupStack.Count > 0)
            {
                await PopupNavigation.PopAsync(animated);
                return TopPage();
            }

            return await base.DoPop(navigation, useModalNavigation, animated);
        }

        protected virtual Page TopPage()
        {
            Page page = null;
            if(PopupNavigation.PopupStack.Count > 0)
                page = PopupNavigation.PopupStack.LastOrDefault();
            else if(_applicationProvider.MainPage.Navigation.ModalStack.Count > 0)
                page = _applicationProvider.MainPage.Navigation.ModalStack.LastOrDefault();
            else
                page = _applicationProvider.MainPage.Navigation.NavigationStack.LastOrDefault();

            if(page == null)
                page = Application.Current.MainPage;

            return FilterPage(page);
        }

        protected virtual Page FilterPage(Page page)
        {
            if(page == null) return page;
            var startPage = page;

            var pageTypeInfo = page.GetType().GetTypeInfo();
            while(pageTypeInfo.IsSubclassOf(typeof(MasterDetailPage)) ||
                  pageTypeInfo.IsSubclassOf(typeof(NavigationPage)) ||
                  pageTypeInfo.IsSubclassOf(typeof(MultiPage<>)))
            {
                if(page.GetType().GetTypeInfo().IsSubclassOf(typeof(MultiPage<>)))
                {
                    page = (page as MultiPage<Page>).CurrentPage;
                }
                else if(page.GetType().GetTypeInfo().IsSubclassOf(typeof(MasterDetailPage)))
                {
                    page = (page as MasterDetailPage).Detail;
                }
                else if(page.GetType().GetTypeInfo().IsSubclassOf(typeof(NavigationPage)))
                {
                    page = (page as NavigationPage).CurrentPage;
                }
                pageTypeInfo = page.GetType().GetTypeInfo();
            }

            return page ?? startPage;
        }

        protected override Task DoPush(Page currentPage, Page page, bool? useModalNavigation, bool animated)
        {
            if(page is PopupPage)
                return PopupNavigation.PushAsync(page as PopupPage, animated);

            return base.DoPush(currentPage, page, useModalNavigation, animated);
        }
    }
}
