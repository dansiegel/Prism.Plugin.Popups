using Unity;
using Prism.Navigation;
using Prism.Plugin.Popups;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Services;

namespace Prism.Unity
{
    public static class PopupNavigationServiceExtensions
    {
        public static IUnityContainer RegisterPopupNavigationService<TService>(this IUnityContainer container)
            where TService : PopupPageNavigationService
        {
            if(!container.IsRegistered<IPopupNavigation>())
            {
                container.RegisterInstance<IPopupNavigation>(PopupNavigation.Instance);
            }

            return container.RegisterType<INavigationService, TService>(PrismApplicationBase.NavigationServiceName);
        }

        public static IUnityContainer RegisterPopupNavigationService(this IUnityContainer container) =>
            container.RegisterPopupNavigationService<PopupPageNavigationService>();
    }
}
