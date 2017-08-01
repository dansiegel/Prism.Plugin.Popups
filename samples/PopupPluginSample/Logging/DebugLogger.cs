using System;
using Prism.Logging;
using static System.Diagnostics.Debug;

namespace PopupPluginSample.Logging
{
    public class DebugLogger : ILoggerFacade
    {
        public void Log(string message, Category category, Priority priority) =>
            WriteLine($"{category} - {priority}: {message}");
    }
}