using System;
using System.Reflection;
using Prism.Behaviors;
using Prism.Ioc;
using Prism.Navigation;
using Prism.Services.Dialogs;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Services;
using Prism.Services.Dialogs.Popups;

namespace Prism.Plugin.Popups
{
    public static class PopupRegistrationExtensions
    {
        public static IContainerRegistry RegisterPopupNavigationService<TService>(this IContainerRegistry containerRegistry)
            where TService : PopupPageNavigationService
        {
            containerRegistry.RegisterPopupNavigation();
            containerRegistry.RegisterSingleton<IPageBehaviorFactory, PopupPageBehaviorFactory>();

            if (IsDryIocContainer(containerRegistry))
            {
                containerRegistry.Register<INavigationService, TService>();
            }

            return containerRegistry.Register<INavigationService, TService>(PrismApplicationBase.NavigationServiceName);
        }

        public static IContainerRegistry RegisterPopupNavigationService(this IContainerRegistry containerRegistry) =>
            containerRegistry.RegisterPopupNavigationService<PopupPageNavigationService>();

        public static IContainerRegistry RegisterPopupDialogService(this IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterPopupNavigation();
            return containerRegistry.RegisterSingleton<IDialogService, PopupDialogService>();
        }

        private static void RegisterPopupNavigation(this IContainerRegistry containerRegistry)
        {
            if (!containerRegistry.IsRegistered<IPopupNavigation>())
            {
                containerRegistry.RegisterInstance<IPopupNavigation>(PopupNavigation.Instance);
            }
        }

        private static bool IsDryIocContainer(IContainerRegistry containerRegistry)
        {
            var type = Type.GetType("DryIoc.IContainer, DryIoc", throwOnError: false);
            if (type is null) return false;

            var regType = containerRegistry.GetType();
            var propInfo = regType.GetRuntimeProperty("Instance");

            return propInfo.PropertyType == type;
        }
    }
}
