using Ninject;
using Prism.Navigation;
using Prism.Plugin.Popups;

namespace Prism.Ninject
{
    public static class PopupNavigationServiceExtensions
    {
        const string _navigationServiceName = "NinjectPageNavigationService";

        public static IKernel RegisterPopupNavigationService<TService>(this IKernel kernel)
            where TService : PopupPageNavigationServiceBase
        {
            kernel.Bind<INavigationService>().To<TService>().Named(_navigationServiceName);
            return kernel;
        }

        public static IKernel RegisterPopupNavigationService(this IKernel kernel) =>
            kernel.RegisterPopupNavigationService<NinjectPopupPageNavigationService>();
    }
}
