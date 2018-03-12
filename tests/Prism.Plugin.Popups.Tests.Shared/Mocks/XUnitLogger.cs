using Prism.Logging;
using Xunit.Abstractions;

namespace Prism.Plugin.Popups.Tests.Mocks
{
    public class XUnitLogger : ILoggerFacade
    {
        private ITestOutputHelper _testOutputHelper { get; }

        public XUnitLogger(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        public void Log(string message, Category category, Priority priority)
        {
            _testOutputHelper.WriteLine($"{category} - {priority}: {message}");
        }
    }
}
