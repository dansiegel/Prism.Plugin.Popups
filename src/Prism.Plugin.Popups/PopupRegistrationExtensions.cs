using System;
using System.Reflection;
using Prism.Behaviors;
using Prism.Ioc;
using Prism.Navigation;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Services;

namespace Prism.Plugin.Popups
{
    public static class PopupRegistrationExtensions
    {
        public static void RegisterPopupNavigationService<TService>(this IContainerRegistry containerRegistry)
            where TService : PopupPageNavigationService
        {
            containerRegistry.RegisterInstance<IPopupNavigation>(PopupNavigation.Instance);
            containerRegistry.Register<INavigationService, TService>(PrismApplicationBase.NavigationServiceName);
            containerRegistry.RegisterSingleton<IPageBehaviorFactory, PopupPageBehaviorFactory>();
            if (IsDryIocContainer(containerRegistry))
                containerRegistry.Register<INavigationService, TService>();
        }

        public static void RegisterPopupNavigationService(this IContainerRegistry containerRegistry) =>
            containerRegistry.RegisterPopupNavigationService<PopupPageNavigationService>();

        public static bool IsDryIocContainer(IContainerRegistry containerRegistry)
        {
            var regType = containerRegistry.GetType();
            var propInfo = regType.GetRuntimeProperty("Instance");
            return propInfo?.PropertyType.FullName.Equals("DryIoc.IContainer", StringComparison.InvariantCultureIgnoreCase) ?? false;
        }
    }
}
