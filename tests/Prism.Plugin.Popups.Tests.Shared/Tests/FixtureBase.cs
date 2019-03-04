using Prism.Plugin.Popups.Tests.Mocks;
using Xunit.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Rg.Plugins.Popup.Services;
using Prism.Plugin.Popups.Tests.Mocks.Services;
using Prism.Navigation;

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
