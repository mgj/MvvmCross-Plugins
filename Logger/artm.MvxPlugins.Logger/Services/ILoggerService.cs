using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

namespace artm.MvxPlugins.Logger.Services
{
    public interface ILoggerService
    {
        void Log(string message, LoggerServiceSeverityLevel level = LoggerServiceSeverityLevel.Debug, Exception exception = null, [CallerMemberName] string memberName = "", [CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0);
    }
}