using Autofac;
using Prism.Navigation;
using Prism.Plugin.Popups;

namespace Prism.Autofac
{
    public static class PopupNavigationServiceExtensions
    {
        const string _navigationServiceName = "AutofacPageNavigationService";

        public static ContainerBuilder RegisterPopupNavigationService<TService>(this ContainerBuilder builder)
            where TService : PopupPageNavigationServiceBase
        {
            builder.RegisterType<TService>().Named<INavigationService>(_navigationServiceName);
            return builder;
        }

        public static ContainerBuilder RegisterPopupNavigationService(this ContainerBuilder builder) =>
            builder.RegisterPopupNavigationService<AutofacPopupPageNavigationService>();
    }
}
