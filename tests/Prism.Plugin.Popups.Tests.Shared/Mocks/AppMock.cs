using Prism.Ioc;
using Prism.Plugin.Popups.Tests.Mocks.Views;
using Prism.Navigation;
using Xamarin.Forms;
using Prism.Logging;
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

        protected override async void OnInitialized()
        {
            var result = await NavigationService.NavigateAsync("MainPage");
            if(!result.Success)
            {
                Container.Resolve<ILoggerFacade>().Log(result.Exception.ToString(), Category.Exception, Priority.High);
            }
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterPopupNavigationService();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage>();
            containerRegistry.RegisterForNavigation<PopupPageMock>();
        }

        public INavigationService GetNavigationService() => NavigationService;
    }
}
