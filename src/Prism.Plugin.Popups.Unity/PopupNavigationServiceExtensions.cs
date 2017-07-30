using Microsoft.Practices.Unity;
using Prism.Navigation;
using Prism.Plugin.Popups;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Services;

namespace Prism.Unity
{
    public static class PopupNavigationServiceExtensions
    {
        const string _navigationServiceName = "UnityPageNavigationService";

        public static IUnityContainer RegisterPopupNavigationService<TService>(this IUnityContainer container)
            where TService : PopupPageNavigationServiceBase
        {
            if(!container.IsRegistered<IPopupNavigation>())
            {
                container.RegisterInstance<IPopupNavigation>(PopupNavigation.Instance);
            }

            return container.RegisterType<INavigationService, TService>(_navigationServiceName);
        }

        public static IUnityContainer RegisterPopupNavigationService(this IUnityContainer container) =>
            container.RegisterPopupNavigationService<UnityPopupPageNavigationService>();
    }
}
