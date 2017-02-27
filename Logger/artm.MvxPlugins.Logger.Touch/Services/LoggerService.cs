using artm.MvxPlugins.Logger.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace artm.MvxPlugins.Logger.Touch.Services
{
    public class LoggerService : ILoggerService
    {
        public void Log(string message, LoggerServiceSeverityLevel level = LoggerServiceSeverityLevel.Debug, Exception exception = null, [CallerMemberName] string memberName = "", [CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0)
        {
            // TODO: Implement
            Debug.WriteLine(message);
        }
    }
}
