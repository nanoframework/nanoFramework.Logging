//
// Copyright (c) .NET Foundation and Contributors
// See LICENSE file in the project root for full license information.
//

using System;
using Microsoft.Extensions.Logging;

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
        public void Log(LogLevel logLevel, EventId eventId, string state, Exception exception)
        {
            if (logLevel >= MinLogLevel)
            {
                string msg = exception == null ? state : $"{state} {exception}";
                System.Diagnostics.Debug.WriteLine(msg);
            }
        }
    }
}
