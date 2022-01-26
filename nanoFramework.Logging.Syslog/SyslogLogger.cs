//
// Copyright (c) .NET Foundation and Contributors
// See LICENSE file in the project root for full license information.
//

using System;
using System.Reflection;
using Microsoft.Extensions.Logging;

namespace nanoFramework.Logging.Syslog
{
    /// <summary>
    /// ILogger implmentation that send message log to a Syslog server following RFC3164 convention.
    /// While not mandatory it is recommended to use <see cref="SyslogLoggerFactory"/> to create the SyslogLogger instances.
    /// </summary>
    public class SyslogLogger : ILogger
    {
        private readonly SyslogClient _client;

        /// <summary>
        /// Create new logger based on existing SyslogClient. Recommended to use LoggerFactory patern trough SyslogLoggerFactory instead of direct use.
        /// </summary>
        /// <param name="client">SyslogClient to use for sending message</param>
        /// <param name="categoryName">CategoryName of this logger used as TAG in the Syslog messages</param>
        /// <exception cref="ArgumentNullException"></exception>
        public SyslogLogger(SyslogClient client, string categoryName)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            LoggerCategoryName = categoryName;
        }

        /// <summary>
        /// Minimum log level used by this logger.
        /// </summary>
        public LogLevel MinLogLevel { get; set; } //by default 0 -> Trace

        /// <summary>
        /// CategoryName of this logger used as TAG in the Syslog messages.
        /// </summary>
        public string LoggerCategoryName { get; }

        /// <summary>
        /// Underlying Syslog client used by this logger.
        /// </summary>
        public SyslogClient Client => _client;

        /// <inheritdoc />
        public bool IsEnabled(LogLevel logLevel) => logLevel >= MinLogLevel;

        /// <inheritdoc />
        public void Log(LogLevel logLevel, EventId eventId, string state, Exception exception, MethodInfo format)
        {
            if (logLevel >= MinLogLevel && logLevel != LogLevel.None)
            {
                string message;
                if (format is null)
                {
                    message = exception == null ? $"{state}" : $"{state} {exception}";
                }
                else
                {
                    message = $"{(string)format.Invoke(null, new object[] { LoggerCategoryName, logLevel, eventId, state, exception })}";
                }

                _client.SendMessage(LogLevelToSeverity(logLevel), LoggerCategoryName, message);
            }
        }

        private Severity LogLevelToSeverity(LogLevel level) => level switch
        {
            LogLevel.Trace => Severity.Debug,
            LogLevel.Debug => Severity.Debug,
            LogLevel.Information => Severity.Informational,
            LogLevel.Warning => Severity.Warning,
            LogLevel.Error => Severity.Error,
            LogLevel.Critical => Severity.Critical,
            LogLevel.None => Severity.None,
            _ => Severity.None
        };
    }
}
