using System;
using System.Threading.Tasks;
using Prism.Common;
using Prism.Ioc;
using Prism.Navigation;
using Prism.Plugin.Popups.Tests.Fixtures;
using Prism.Plugin.Popups.Tests.Mocks.Views;
using Rg.Plugins.Popup.Contracts;
using Xunit;
using Xunit.Abstractions;

namespace Prism.Plugin.Popups.Tests
{
    [Collection(nameof(PrismApp))]
    public class PopupUtilitiesFixture : FixtureBase
    {
        public PopupUtilitiesFixture(ITestOutputHelper testOutputHelper)
            : base(testOutputHelper)
        {
        }

        [Theory]
        [InlineData("/MDP/NavigationPage/MainPage", typeof(MainPage))]
        [InlineData("/MDP/NavigationPage/MainPage/ViewA", typeof(ViewA))]
        [InlineData("/TabbedPage?createTab=ViewA&createTab=ViewB", typeof(ViewA))]
        [InlineData("/NavigationPage/TabbedPage?createTab=ViewA&createTab=ViewB", typeof(ViewA))]
        [InlineData("/MainPage", typeof(MainPage))]
        [InlineData("/MainPage/ViewA", typeof(ViewA))]
        [InlineData("/NavigationPage/MainPage/ViewA/ViewB?useModalNavigation=true", typeof(ViewB))]
        public async Task GetsCorrectTopPage(string uri, Type pageType)
        {
            var app = GetApp();
            var result = await app.GetNavigationService().NavigateAsync(uri);
            Assert.Null(result.Exception);

            var popupNavigation = app.Container.Resolve<IPopupNavigation>();
            var topPage = PopupUtilities.TopPage(popupNavigation, app.MainPage);
            Assert.IsType(pageType, topPage);

            var navService = app.Container.Resolve<INavigationService>(PrismApplicationBase.NavigationServiceName);
            if (navService is IPageAware pa)
            {
                pa.Page = topPage;
            }

            result = await navService.NavigateAsync("PopupPageMock");
            Assert.Null(result.Exception);

            topPage = PopupUtilities.TopPage(popupNavigation, app.MainPage);
            Assert.IsType<PopupPageMock>(topPage);
        }
    }
}
