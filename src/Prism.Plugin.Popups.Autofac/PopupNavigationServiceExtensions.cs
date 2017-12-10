using Autofac;
using Prism.Navigation;
using Prism.Plugin.Popups;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Services;

namespace Prism.Autofac
{
    public static class PopupNavigationServiceExtensions
    {
        public static ContainerBuilder RegisterPopupNavigationService<TService>(this ContainerBuilder builder)
            where TService : PopupPageNavigationService
        {
            builder.RegisterInstance(PopupNavigation.Instance).As<IPopupNavigation>().IfNotRegistered(typeof(IPopupNavigation));
            builder.RegisterType<TService>().Named<INavigationService>(PrismApplicationBase.NavigationServiceName);
            return builder;
        }

        public static ContainerBuilder RegisterPopupNavigationService(this ContainerBuilder builder) =>
            builder.RegisterPopupNavigationService<PopupPageNavigationService>();
    }
}
