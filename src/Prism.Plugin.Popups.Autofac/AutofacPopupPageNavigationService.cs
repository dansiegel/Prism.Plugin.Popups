using Autofac;
using Prism.Common;
using Prism.Logging;
using Rg.Plugins.Popup.Contracts;
using Xamarin.Forms;

namespace Prism.Plugin.Popups
{
    public class AutofacPopupPageNavigationService : PopupPageNavigationServiceBase
    {
        IComponentContext _context { get; }

        public AutofacPopupPageNavigationService( IPopupNavigation popupNavigation, IApplicationProvider applicationProvider, ILoggerFacade logger, IComponentContext context )
            : base( popupNavigation, applicationProvider, logger )
        {
            _context = context;
        }

        protected override Page CreatePage(string segmentName)
        {
            return _context.ResolveNamed<object>(segmentName) as Page;
        }
    }
}
