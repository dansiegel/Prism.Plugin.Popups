using System;
using Microsoft.Practices.Unity;
using Prism.Logging;
using Prism.Unity;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace Prism.Navigation
{
    public static partial class PopupExtensions
    {
        static IUnityContainer s_container
        {
            get { return ( Application.Current as PrismApplication ).Container; }
        }

        static ILoggerFacade s_logger
        {
            get { return ( Application.Current as PrismApplication ).Container.Resolve<ILoggerFacade>(); }
        }

        private static bool IsPageRegistered( string name ) =>
            s_container.IsRegistered<object>( name );

        private static PopupPage CreatePopupPageByName( string name )
        {
            VerifyPageIsRegistered( name );
            return s_container.Resolve<object>( name ) as PopupPage;
        }
    }
}
