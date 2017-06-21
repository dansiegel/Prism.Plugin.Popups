using System;
using Prism.Common;
using Prism.Logging;
using Ninject;
using Xamarin.Forms;
using Rg.Plugins.Popup.Contracts;

namespace Prism.Plugin.Popups
{
    public class NinjectPopupPageNavigationService : PopupPageNavigationServiceBase
    {
        IKernel _kernel { get; }

        public NinjectPopupPageNavigationService( IPopupNavigation popupNavigation, IApplicationProvider applicationProvider, ILoggerFacade logger, IKernel kernel )
            : base( popupNavigation, applicationProvider, logger )
        {
            _kernel = kernel;
        }

        protected override Xamarin.Forms.Page CreatePage(string segmentName)
        {
            return _kernel.Get<object>(segmentName) as Page;
        }
    }
}
