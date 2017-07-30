using Autofac;
using Prism.Navigation;
using Prism.Plugin.Popups;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Services;

namespace Prism.Autofac
{
    public static class PopupNavigationServiceExtensions
    {
        const string _navigationServiceName = "AutofacPageNavigationService";

        public static ContainerBuilder RegisterPopupNavigationService<TService>(this ContainerBuilder builder)
            where TService : PopupPageNavigationServiceBase
        {
            builder.RegisterInstance(PopupNavigation.Instance).As<IPopupNavigation>().IfNotRegistered(typeof(IPopupNavigation));
            builder.RegisterType<TService>().Named<INavigationService>(_navigationServiceName);
            return builder;
        }

        public static ContainerBuilder RegisterPopupNavigationService(this ContainerBuilder builder) =>
            builder.RegisterPopupNavigationService<AutofacPopupPageNavigationService>();
    }
}
