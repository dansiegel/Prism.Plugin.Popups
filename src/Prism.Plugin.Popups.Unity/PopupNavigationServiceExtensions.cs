using Microsoft.Practices.Unity;
using Prism.Navigation;
using Prism.Plugin.Popups;

namespace Prism.Unity
{
    public static class PopupNavigationServiceExtensions
    {
        const string _navigationServiceName = "UnityPageNavigationService";

        public static IUnityContainer RegisterPopupNavigationService<TService>(this IUnityContainer container)
            where TService : PopupPageNavigationServiceBase =>
            container.RegisterType<INavigationService, TService>(_navigationServiceName);

        public static IUnityContainer RegisterPopupNavigationService(this IUnityContainer container) =>
            container.RegisterPopupNavigationService<UnityPopupPageNavigationService>();
    }
}
