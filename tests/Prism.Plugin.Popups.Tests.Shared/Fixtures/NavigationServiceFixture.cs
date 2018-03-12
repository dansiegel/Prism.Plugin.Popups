using System;
using System.Collections.Generic;
using System.Text;
using Prism.Ioc;
using Prism.Plugin.Popups.Tests.Fixtures;
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
    public class NavigationServiceFixture : FixtureBase
    {
        public NavigationServiceFixture(ITestOutputHelper testOutputHelper) 
            : base(testOutputHelper)
        {
        }

        //[Fact]
        //public void PopupNavigation_HasEmptyStack
    }
}
