using System;
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
        }

        public static void RegisterPopupNavigationService(this IContainerRegistry containerRegistry) =>
            containerRegistry.RegisterPopupNavigationService<PopupPageNavigationService>();
    }
}
