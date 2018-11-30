using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Prism.Plugin.Popups.Tests.Fixtures;
using Prism.Plugin.Popups.Tests.Mocks.ViewModels;
using Prism.Plugin.Popups.Tests.Mocks.Views;
using Rg.Plugins.Popup.Services;
using Xunit;
using Xunit.Abstractions;
using Prism.Navigation;

#if AUTOFAC
namespace Prism.Plugin.Popups.Autofac.Tests.Fixtures
#elif DRYIOC
namespace Prism.Plugin.Popups.DryIoc.Tests.Fixtures
#else
namespace Prism.Plugin.Popups.Unity.Tests.Fixtures
#endif
{
    public class NavigationServiceFixture : FixtureBase
    {
        public NavigationServiceFixture(ITestOutputHelper testOutputHelper) 
            : base(testOutputHelper)
        {
        }

        //[Fact]
        //public void PopupNavigationService_SetsStandardPages()
        //{
        //    var app = GetApp();
        //    Assert.Empty(PopupNavigation.Instance.PopupStack);
        //    Assert.NotNull(app.MainPage);
        //    Assert.IsType<MainPage>(app.MainPage);
        //}

        //[Fact]
        //public async Task PushingPopupPage_PushesToPopupStack()
        //{
        //    var app = GetApp();
        //    Assert.Empty(PopupNavigation.Instance.PopupStack);
        //    var result = await app.GetNavigationService().NavigateAsync("PopupPageMock");

        //    Assert.True(result.Success);
        //    Assert.Single(PopupNavigation.Instance.PopupStack);
        //    Assert.IsType<PopupPageMock>(PopupNavigation.Instance.PopupStack[0]);
        //}

        //[Fact]
        //public async Task SupportsDeepLinkedPopupPages()
        //{
        //    var app = GetApp();
        //    Assert.Empty(PopupNavigation.Instance.PopupStack);
        //    var result = await app.GetNavigationService().NavigateAsync("/NavigationPage/MainPage/PopupPageMock/PopupPageMock");
        //    Assert.True(result.Success);

        //    Assert.Equal(2, PopupNavigation.Instance.PopupStack.Count);
        //}

        //[Fact]
        //public async Task ClearPopupStackExtension_ClearsPopupStack()
        //{
        //    var app = GetApp();
        //    Assert.Empty(PopupNavigation.Instance.PopupStack);
        //    var result = await app.GetNavigationService().NavigateAsync("PopupPageMock/PopupPageMock");
        //    Assert.True(result.Success);
        //    Assert.Equal(2, PopupNavigation.Instance.PopupStack.Count);

        //    var vm = PopupNavigation.Instance.PopupStack.Last().BindingContext as PopupPageMockViewModel;

        //    var clearResult = await vm.NavigationService.ClearPopupStackAsync();
        //    Assert.Null(clearResult.Exception);
        //    Assert.True(clearResult.Success);
        //    Assert.Empty(PopupNavigation.Instance.PopupStack);
        //}
    }
}
