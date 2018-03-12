using Prism.Plugin.Popups.Tests.Mocks;
using Xunit.Abstractions;

namespace Prism.Plugin.Popups.Tests.Fixtures
{
    public abstract class FixtureBase
    {
        private ITestOutputHelper _testOutputHelper { get; }

        public FixtureBase(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            Xamarin.Forms.Mocks.MockForms.Init();
        }

        protected AppMock GetApp() =>
            new AppMock(new XUnitPlatformInitializer(_testOutputHelper));
    }
}
