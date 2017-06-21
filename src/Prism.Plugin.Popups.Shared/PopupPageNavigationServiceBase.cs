using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Prism.Common;
using Prism.Logging;
using Prism.Navigation;
using Prism.Plugin.Popups.Extensions;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Prism.Plugin.Popups
{
    public abstract class PopupPageNavigationServiceBase : PageNavigationService
    {
        protected IPopupNavigation _popupNavigation { get; }

        public PopupPageNavigationServiceBase( IPopupNavigation popupNavigation, IApplicationProvider applicationProvider, ILoggerFacade logger ) 
            : base( applicationProvider, logger )
        {
            _popupNavigation = popupNavigation;
        }

        protected override async Task<Page> DoPop(INavigation navigation, bool useModalNavigation, bool animated)
        {
            if(_popupNavigation.PopupStack.Count > 0)
            {
                await _popupNavigation.PopAsync(animated);
                return TopPage();
            }

            return await base.DoPop(navigation, useModalNavigation, animated);
        }

        protected virtual Page TopPage()
        {
            Page page = null;
            if(_popupNavigation.PopupStack.Count > 0)
                page = _popupNavigation.PopupStack.LastOrDefault();
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
                return _popupNavigation.PushAsync(page as PopupPage, animated);

            return base.DoPush(currentPage, page, useModalNavigation, animated);
        }
    }
}
