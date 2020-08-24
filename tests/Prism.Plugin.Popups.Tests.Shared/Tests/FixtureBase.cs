using System.Reflection;
using Prism.Navigation;
using Prism.Plugin.Popups.Tests.Mocks;
using Prism.Plugin.Popups.Tests.Mocks.Services;
using Rg.Plugins.Popup.Services;
using Xunit.Abstractions;

namespace Prism.Plugin.Popups.Tests.Fixtures
{
    public abstract class FixtureBase
    {
        protected ITestOutputHelper _testOutputHelper { get; }

        public FixtureBase(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            Xamarin.Forms.Mocks.MockForms.Init();

            var type = typeof(PopupNavigation);
            var property = type.GetField("_popupNavigation", BindingFlags.NonPublic | BindingFlags.Static);
            property.SetValue(null, new PluginNavigationMock());
        }

        protected AppMock GetApp()
        {
            PageNavigationRegistry.ClearRegistrationCache();
            return new AppMock(new XUnitPlatformInitializer(_testOutputHelper));
        }
    }
}
