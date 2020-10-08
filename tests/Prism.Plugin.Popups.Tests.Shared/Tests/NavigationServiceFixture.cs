using System.Linq;
using System.Threading.Tasks;
using Prism.Navigation;
using Prism.Plugin.Popups.Tests.Fixtures;
using Prism.Plugin.Popups.Tests.Mocks.ViewModels;
using Prism.Plugin.Popups.Tests.Mocks.Views;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xunit;
using Xunit.Abstractions;

namespace Prism.Plugin.Popups.Tests
{
    [Collection(nameof(PrismApp))]
    public class NavigationServiceFixture : FixtureBase
    {
        public NavigationServiceFixture(ITestOutputHelper testOutputHelper)
            : base(testOutputHelper)
        {
        }

        [Retry]
        public async Task PopupNavigationService_SetsStandardPages()
        {
            var app = GetApp();
            var result = await app.GetNavigationService().NavigateAsync("MainPage");
            Assert.Null(result.Exception);

            Assert.NotNull(app.MainPage);
            Assert.IsType<MainPage>(app.MainPage);

            Assert.Empty(PopupNavigation.Instance.PopupStack);
        }

        [Fact]
        public async Task PushingPopupPage_PushesToPopupStack()
        {
            var app = GetApp();
            app.MainPage = new ContentPage();
            Assert.Empty(PopupNavigation.Instance.PopupStack);
            var result = await app.GetNavigationService().NavigateAsync("PopupPageMock");

            Assert.True(result.Success);
            Assert.Single(PopupNavigation.Instance.PopupStack);
            Assert.IsType<PopupPageMock>(PopupNavigation.Instance.PopupStack[0]);
        }

        [Fact]
        public async Task PushingPopupPage_ResultInPopupNavigationExceptionWithNoRootPage()
        {
            var app = GetApp();
            var result = await app.GetNavigationService().NavigateAsync("PopupPageMock");
            Assert.IsType<PopupNavigationException>(result.Exception);
        }

        [Fact]
        public async Task AbsoluteNavigationClearsNavigationStack()
        {
            var app = GetApp();
            app.MainPage = new MainPage();
            await app.GetNavigationService().NavigateAsync("ViewA");
            Assert.Single(app.MainPage.Navigation.ModalStack);
            var viewA = app.MainPage.Navigation.ModalStack.First();
            Assert.IsType<ViewA>(viewA);
            var viewANavService = app.GetNavigationService(viewA);
            await viewANavService.NavigateAsync("PopupPageMock");
            Assert.Single(PopupNavigation.Instance.PopupStack);
            var popupPage = PopupNavigation.Instance.PopupStack.First();
            Assert.IsType<PopupPageMock>(popupPage);
            var popupPageNavigationService = app.GetNavigationService(popupPage);

            var result = await popupPageNavigationService.NavigateAsync("/ViewB");
            Assert.Null(result.Exception);
            Assert.IsType<ViewB>(app.MainPage);
        }

        [Fact]
        public async Task RelativeNavigationDismissesPopupNavigationStack()
        {
            var app = GetApp();
            app.MainPage = new MainPage();
            await app.GetNavigationService().NavigateAsync("PopupPageMock");

            Assert.Single(PopupNavigation.Instance.PopupStack);
            Assert.Empty(app.MainPage.Navigation.ModalStack);
            Assert.Empty(app.MainPage.Navigation.NavigationStack);

            var popupPage = PopupNavigation.Instance.PopupStack.First();
            await app.GetNavigationService(popupPage).NavigateAsync("ViewA");

            Assert.Empty(PopupNavigation.Instance.PopupStack);
            Assert.Single(app.MainPage.Navigation.ModalStack);
            Assert.Empty(app.MainPage.Navigation.NavigationStack);
            Assert.IsType<ViewA>(app.MainPage.Navigation.ModalStack.First());
        }

        // TODO: Due to the Reverse navigation required within a NavigationPage this test isn't really supportable
        //[Fact]
        //public async Task SupportsDeepLinkedPopupPages()
        //{
        //    var app = GetApp();
        //    var navPage = new NavigationPage(new ContentPage());
        //    app.MainPage = navPage;
        //    Assert.Empty(PopupNavigation.Instance.PopupStack);
        //    var result = await app.GetNavigationService(navPage.RootPage).NavigateAsync("../MainPage/PopupPageMock");
        //    Assert.Null(result.Exception);

        //    Assert.Equal(2, PopupNavigation.Instance.PopupStack.Count);
        //}

        [Fact]
        public async Task ClearPopupStackExtension_ClearsPopupStack()
        {
            var app = GetApp();
            app.MainPage = new ContentPage();
            Assert.Empty(PopupNavigation.Instance.PopupStack);
            var result = await app.GetNavigationService().NavigateAsync("PopupPageMock/PopupPageMock");
            Assert.True(result.Success);
            Assert.Equal(2, PopupNavigation.Instance.PopupStack.Count);

            var vm = PopupNavigation.Instance.PopupStack.Last().BindingContext as PopupPageMockViewModel;

            var clearResult = await vm.NavigationService.ClearPopupStackAsync();
            Assert.Null(clearResult.Exception);
            Assert.True(clearResult.Success);
            Assert.Empty(PopupNavigation.Instance.PopupStack);
        }

        [Fact]
        public async Task PopupPageIsDestroyed_WhenNavigatingBack()
        {
            var app = GetApp();
            app.MainPage = new ContentPage();
            await app.GetNavigationService().NavigateAsync("PopupPageMock");

            var popupPage = PopupNavigation.Instance.PopupStack.First();
            Assert.IsType<PopupPageMockViewModel>(popupPage.BindingContext);
            var vm = (PopupPageMockViewModel)popupPage.BindingContext;

            Assert.Equal(0, vm.Destroyed);

            await app.GetNavigationService(popupPage).GoBackAsync();

            Assert.Equal(1, vm.Destroyed);
        }

        [Fact]
        public async Task PopupPageIsDestroyed_WhenNavigatingToContentPage()
        {
            var app = GetApp();
            app.MainPage = new ContentPage();
            await app.GetNavigationService().NavigateAsync("PopupPageMock");

            var popupPage = PopupNavigation.Instance.PopupStack.First();
            Assert.IsType<PopupPageMockViewModel>(popupPage.BindingContext);
            var vm = (PopupPageMockViewModel)popupPage.BindingContext;

            Assert.Equal(0, vm.Destroyed);

            await app.GetNavigationService(popupPage).NavigateAsync("ViewA");

            Assert.Equal(1, vm.Destroyed);
        }
    }
}
