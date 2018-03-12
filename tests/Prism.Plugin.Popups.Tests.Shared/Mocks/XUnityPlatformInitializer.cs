using System;
using System.Collections.Generic;
using System.Text;
using Prism.Ioc;
using Prism.Logging;
using Xunit.Abstractions;

namespace Prism.Plugin.Popups.Tests.Mocks
{
    public class XUnitPlatformInitializer : IPlatformInitializer
    {
        private ITestOutputHelper _testOutputHelper { get; }

        public XUnitPlatformInitializer(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<ILoggerFacade, XUnitLogger>();
        }
    }
}
