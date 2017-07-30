using Ninject;
using Prism.Navigation;
using Prism.Plugin.Popups;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Services;

namespace Prism.Ninject
{
    public static class PopupNavigationServiceExtensions
    {
        const string _navigationServiceName = "NinjectPageNavigationService";

        public static IKernel RegisterPopupNavigationService<TService>(this IKernel kernel)
            where TService : PopupPageNavigationServiceBase
        {
            if(!kernel.CanResolve<IPopupNavigation>())
            {
                kernel.Bind<IPopupNavigation>().ToConstant(PopupNavigation.Instance).InSingletonScope();
            }

            kernel.Bind<INavigationService>().To<TService>().Named(_navigationServiceName);
            return kernel;
        }

        public static IKernel RegisterPopupNavigationService(this IKernel kernel) =>
            kernel.RegisterPopupNavigationService<NinjectPopupPageNavigationService>();
    }
}
