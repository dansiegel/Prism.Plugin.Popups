using Prism.Ioc;
using Prism.Navigation;
using Prism.Plugin.Popups.Tests.Fixtures;
using Prism.Plugin.Popups.Tests.Mocks;
using Prism.Plugin.Popups.Tests.Mocks.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

#if AUTOFAC
namespace Prism.Plugin.Popups.Autofac.Tests.Fixtures
#elif DRYIOC
namespace Prism.Plugin.Popups.Autofac.Tests.Fixtures
#else
namespace Prism.Plugin.Popups.Unity.Tests.Fixtures
#endif
{
    public class RegistrationFixture : FixtureBase
    {
        public RegistrationFixture(ITestOutputHelper testOutputHelper) 
            : base(testOutputHelper)
        {
        }

        [Fact]
        public void Foo()
        {
            AppMock app = null;

            var exceptions = Record.Exception(() => app = GetApp());

            Assert.Null(exceptions);
            Assert.NotNull(app);
        }

        [Fact]
        public void Bar()
        {
            AppMock app = null;
            var exceptions = Record.Exception(() => app = new AppMock(null));

            Assert.Null(exceptions);
            Assert.NotNull(app);
        }

        [Fact]
        public void Application_Has_PopupNavigationService()
        {
            var app = GetApp();
            Assert.IsType<PopupPageNavigationService>(app.GetNavigationService());
        }

        [Fact]
        public void MainPage_Has_PopupNavigationService()
        {
            var app = GetApp();
            var vm = app.MainPage.BindingContext as MainPageViewModel;
            Assert.NotNull(vm);
            Assert.NotNull(vm.NavigationService);
            Assert.IsType<PopupPageNavigationService>(vm.NavigationService);
        }

        [Fact]
        public void Container_Resolves_PopupNavigationService()
        {
            var app = GetApp();
            var navService = app.Container.Resolve<INavigationService>(PrismApplicationBase.NavigationServiceName);
            Assert.NotNull(navService);
            Assert.IsType<PopupPageNavigationService>(navService);
        }
    }
}
