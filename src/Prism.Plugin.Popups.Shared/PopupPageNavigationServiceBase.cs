using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Prism.Common;
using Prism.Logging;
using Prism.Navigation;
using Prism.Plugin.Popups.Extensions;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

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

            return page.GetDisplayedPage();
        }

        protected override Task DoPush(Page currentPage, Page page, bool? useModalNavigation, bool animated)
        {
            if(page is PopupPage)
                return PopupNavigation.PushAsync(page as PopupPage, animated);

            return base.DoPush(currentPage, page, useModalNavigation, animated);
        }
    }
}
