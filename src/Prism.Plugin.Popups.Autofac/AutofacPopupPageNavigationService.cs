using Autofac;
using Prism.Common;
using Prism.Logging;
using Xamarin.Forms;

namespace Prism.Plugin.Popups
{
    public class AutofacPopupPageNavigationService : PopupPageNavigationServiceBase
    {
        IContainer _container { get; }

        public AutofacPopupPageNavigationService( IApplicationProvider applicationProvider, ILoggerFacade logger, IContainer container )
            : base( applicationProvider, logger )
        {
            _container = container;
        }

        protected override Page CreatePage(string segmentName)
        {
            return _container.ResolveNamed<object>(segmentName) as Page;
        }
    }
}
