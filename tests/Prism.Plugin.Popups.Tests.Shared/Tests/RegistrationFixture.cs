using System.Threading.Tasks;
using Prism.Behaviors;
using Prism.Ioc;
using Prism.Navigation;
using Prism.Plugin.Popups.Tests.Fixtures;
using Prism.Plugin.Popups.Tests.Mocks;
using Prism.Plugin.Popups.Tests.Mocks.Services;
using Prism.Plugin.Popups.Tests.Mocks.ViewModels;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Services;
using Xunit;
using Xunit.Abstractions;

namespace Prism.Plugin.Popups.Tests
{
    public class RegistrationFixture : FixtureBase
    {
        public RegistrationFixture(ITestOutputHelper testOutputHelper)
            : base(testOutputHelper)
        {

        }

        [Retry]
        public void MockPopupNavigation_IsSet()
        {
            IPopupNavigation popupNavigation = null;
            var ex = Record.Exception(() => popupNavigation = PopupNavigation.Instance);
            Assert.Null(ex);
            Assert.NotNull(popupNavigation);
            Assert.IsType<PluginNavigationMock>(popupNavigation);
        }

        [Retry]
        public void CreateApp_DoesNotThrowException()
        {
            AppMock app = null;

            var exceptions = Record.Exception(() => app = GetApp());

            Assert.Null(exceptions);
            Assert.NotNull(app);
        }

        [Retry]
        public void CreateApp_DoesNotThrowExceptionWithNoPlatformInitializer()
        {
            AppMock app = null;
            var exceptions = Record.Exception(() => app = new AppMock(null));

            Assert.Null(exceptions);
            Assert.NotNull(app);
        }

        [Retry]
        public void Application_Has_PopupNavigationService()
        {
            var app = GetApp();
            Assert.IsType<PopupPageNavigationService>(app.GetNavigationService());
        }

        [Retry]
        public async Task MainPage_Has_PopupNavigationService()
        {
            var app = GetApp();
            await app.GetNavigationService().NavigateAsync("MainPage");

            var vm = app.MainPage.BindingContext as MainPageViewModel;
            Assert.NotNull(vm);
            Assert.NotNull(vm.NavigationService);
            Assert.IsType<PopupPageNavigationService>(vm.NavigationService);
        }

        [Retry]
        public void Container_Resolves_PopupNavigationService()
        {
            var app = GetApp();
            var navService = app.Container.Resolve<INavigationService>(PrismApplicationBase.NavigationServiceName);
            Assert.NotNull(navService);
            Assert.IsType<PopupPageNavigationService>(navService);
        }

        [Retry]
        public void PopupPageBehaviorFactory_IsRegistered()
        {
            var app = GetApp();

            IPageBehaviorFactory behaviorFactory = null;
            var ex = Record.Exception(() => behaviorFactory = app.Container.Resolve<IPageBehaviorFactory>());

            Assert.Null(ex);
            Assert.NotNull(behaviorFactory);
            Assert.IsType<PopupPageBehaviorFactory>(behaviorFactory);
        }

        [Retry]
        public void IPopupNavigation_IsRegistered()
        {
            var app = GetApp();
            IPopupNavigation popupNavigation = null;
            var ex = Record.Exception(() => popupNavigation = app.Container.Resolve<IPopupNavigation>());

            Assert.Null(ex);
            Assert.NotNull(popupNavigation);
        }
    }
}
