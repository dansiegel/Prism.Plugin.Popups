using System;
using Autofac;
using Prism.Autofac;
using Prism.Logging;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace Prism.Navigation
{
    public static partial class PopupExtensions
    {
        static IContainer s_container
        {
            get { return ( Application.Current as PrismApplication ).Container; }
        }

        static ILoggerFacade s_logger
        {
            get { return ( Application.Current as PrismApplication ).Container.Resolve<ILoggerFacade>(); }
        }

        private static bool IsPageRegistered( string name ) =>
            s_container.IsRegisteredWithName<Page>( name );

        private static PopupPage CreatePopupPageByName( string name )
        {
            VerifyPageIsRegistered( name );
            return s_container.ResolveNamed<Page>( name ) as PopupPage;
        }
    }
}
