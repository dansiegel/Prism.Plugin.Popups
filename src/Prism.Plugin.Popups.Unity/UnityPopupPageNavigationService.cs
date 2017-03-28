using Microsoft.Practices.Unity;
using Prism.Common;
using Prism.Logging;
using Xamarin.Forms;

namespace Prism.Plugin.Popups
{
    public class UnityPopupPageNavigationService : PopupPageNavigationServiceBase
    {
        IUnityContainer _container { get; }

        public UnityPopupPageNavigationService( IApplicationProvider applicationProvider, ILoggerFacade logger, IUnityContainer container )
            : base( applicationProvider, logger )
        {
            _container = container;
        }

        protected override Page CreatePage(string segmentName)
        {
            return _container.Resolve<object>(segmentName) as Page;
        }
    }
}
