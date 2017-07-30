using DryIoc;
using Prism.Navigation;
using Prism.Plugin.Popups;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Interfaces;
using Rg.Plugins.Popup.Services;

namespace Prism.DryIoc
{
    public static class PopupNavigationServiceExtensions
    {
        const string _navigationServiceName = "DryIocPageNavigationService";

        public static IContainer RegisterPopupNavigationService<TService>(this IContainer container)
            where TService : PopupPageNavigationServiceBase
        {
            container.Register<IPopupNavigation>(reuse: Reuse.Singleton,
                                                 made: Made.Of(() => PopupNavigation.Instance),
                                                 ifAlreadyRegistered: IfAlreadyRegistered.Keep);

            container.Register<INavigationService, TService>(ifAlreadyRegistered: IfAlreadyRegistered.Replace, serviceKey: _navigationServiceName);
            return container;
        }

        public static IContainer RegisterPopupNavigationService(this IContainer container) =>
            container.RegisterPopupNavigationService<DryIocPopupPageNavigationService>();
    }
}
