using DryIoc;
using Prism.Navigation;
using Prism.Plugin.Popups;

namespace Prism.DryIoc
{
    public static class PopupNavigationServiceExtensions
    {
        const string _navigationServiceName = "DryIocPageNavigationService";

        public static IContainer RegisterPopupNavigationService<TService>(this IContainer container)
            where TService : PopupPageNavigationServiceBase
        {
            container.Register<INavigationService, TService>(ifAlreadyRegistered: IfAlreadyRegistered.Replace, serviceKey: _navigationServiceName);
            return container;
        }

        public static IContainer RegisterPopupNavigationService(this IContainer container) =>
            container.RegisterPopupNavigationService<DryIocPopupPageNavigationService>();
    }
}
