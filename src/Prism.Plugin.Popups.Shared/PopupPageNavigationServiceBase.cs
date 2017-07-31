using System.Linq;
using System.Threading.Tasks;
using Prism.Common;
using Prism.Logging;
using Prism.Navigation;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Pages;
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
                return PopupUtilities.TopPage(_popupNavigation, _applicationProvider);
            }

            return await base.DoPop(navigation, useModalNavigation, animated);
        }

        protected override Task DoPush(Page currentPage, Page page, bool? useModalNavigation, bool animated, bool insertBeforeLast = false, int navigationOffset = 0)
        {
            switch(page)
            {
                case PopupPage popup:
                    return _popupNavigation.PushAsync(popup, animated);
                default:
                    return base.DoPush(currentPage, page, useModalNavigation, animated, insertBeforeLast, navigationOffset);
            }
        }

        protected override void ApplyPageBehaviors(Page page)
        {
            switch(page)
            {
                case PopupPage popup:
                    page.Behaviors.Add(new BackgroundPopupDismissalBehavior(_popupNavigation, _applicationProvider));
                    break;
                default:
                    base.ApplyPageBehaviors(page);
                    break;
            }
        }
    }
}
