using Ninject;
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

        private static PopupPage CreatePopupPageByName( string name )
        {
            return s_container.Get<object>( name ) as PopupPage;
        }
    }
}
