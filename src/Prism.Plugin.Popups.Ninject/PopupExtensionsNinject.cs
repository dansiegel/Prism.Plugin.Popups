using Ninject;
using Prism.Logging;
using Prism.Ninject;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace Prism.Navigation
{
    public static partial class PopupExtensions
    {
        static IKernel s_container
        {
            get { return ( Application.Current as PrismApplication ).Container; }
        }

        static ILoggerFacade s_logger
        {
            get { return ( Application.Current as PrismApplication ).Container.Get<ILoggerFacade>(); }
        }

        private static bool IsPageRegistered( string name ) =>
            s_container.CanResolve<object>( name );

        private static PopupPage CreatePopupPageByName( string name )
        {
            VerifyPageIsRegistered( name );
            return s_container.Get<object>( name ) as PopupPage;
        }
    }
}
