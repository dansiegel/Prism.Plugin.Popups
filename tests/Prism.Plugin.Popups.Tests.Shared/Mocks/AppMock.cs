using Prism.Ioc;
using Prism.Plugin.Popups.Tests.Mocks.Views;
using Prism.Navigation;
#if AUTOFAC
using Prism.Autofac;
#elif DRYIOC
using Prism.DryIoc;
#elif UNITY
using Prism.Unity;
#endif

namespace Prism.Plugin.Popups.Tests.Mocks
{
    public class AppMock : PrismApplication
    {
        public AppMock(IPlatformInitializer platformInitializer)
            : base(platformInitializer)
        {

        }

        protected override void OnInitialized()
        {
            NavigationService.NavigateAsync("MainPage").ContinueWith(t => { });
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterPopupNavigationService();
            containerRegistry.RegisterForNavigation<MainPage>();
            containerRegistry.RegisterForNavigation<PopupPageMock>();
        }

        public INavigationService GetNavigationService() => NavigationService;
    }
}
