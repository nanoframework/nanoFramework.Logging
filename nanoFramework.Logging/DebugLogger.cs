//
// Copyright (c) .NET Foundation and Contributors
// See LICENSE file in the project root for full license information.
//

using Microsoft.Extensions.Logging;
using System;
using System.Reflection;

namespace nanoFramework.Logging.Debug
{
    /// <summary>
    /// A logger that prints to the debug console
    /// </summary>
    public class DebugLogger : ILogger
    {
        /// <summary>
        /// Creates a new instance of the <see cref="DebugLogger"/>
        /// </summary>
        /// <param name="loggerName">The logger name</param>
        public DebugLogger(string loggerName)
        {
            LoggerName = loggerName;
            MinLogLevel = LogLevel.Debug;
        }

        /// <summary>
        /// Name of the logger
        /// </summary>
        public string LoggerName { get; }

        /// <summary>
        /// Sets the minimum log level
        /// </summary>
        public LogLevel MinLogLevel { get; set; }

        /// <inheritdoc />
        public bool IsEnabled(LogLevel logLevel) => logLevel >= MinLogLevel;

        /// <inheritdoc />
        public void Log(LogLevel logLevel, EventId eventId, string state, Exception exception, MethodInfo format)
        {
            if (logLevel >= MinLogLevel)
            {
                string msg;
                if (format == null)
                {
                    msg = exception == null ? state : $"{state} {exception}";
                }
                else
                {
                    msg = (string)format.Invoke(null, new object[] { LoggerName, logLevel, eventId, state, exception });
                }

                System.Diagnostics.Debug.WriteLine(msg);
            }
        }
    }
}
