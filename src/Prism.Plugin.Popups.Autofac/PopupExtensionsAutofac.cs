using System;
using Autofac;
using Prism.Autofac;
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

        private static PopupPage CreatePopupPageByName( string name )
        {
            if( !s_container.IsRegisteredWithName<Page>( name ) )
                throw new NullReferenceException( $"The requested page '{name}' has not been registered." );

            return s_container.ResolveNamed<Page>( name ) as PopupPage;
        }
    }
}
