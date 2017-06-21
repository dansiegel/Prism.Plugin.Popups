using System;
using DryIoc;
using Prism.Common;
using Prism.Logging;
using Rg.Plugins.Popup.Contracts;
using Xamarin.Forms;

namespace Prism.Plugin.Popups
{
    public class DryIocPopupPageNavigationService : PopupPageNavigationServiceBase
    {
        IContainer _container { get; }

        public DryIocPopupPageNavigationService(IPopupNavigation popupNavigation, IApplicationProvider applicationProvider, ILoggerFacade logger, IContainer container)
            : base( popupNavigation, applicationProvider, logger )
        {
            _container = container;
        }

        protected override Page CreatePage(string segmentName)
        {
            return _container.Resolve<object>(segmentName) as Page;
        }
    }
}
