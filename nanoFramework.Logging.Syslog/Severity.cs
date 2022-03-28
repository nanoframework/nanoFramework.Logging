//
// Copyright (c) .NET Foundation and Contributors
// See LICENSE file in the project root for full license information.
//

using Microsoft.Extensions.Logging;

namespace nanoFramework.Logging.Syslog
{
    /// <summary>
    /// Severity represents the Severity in the Syslog messages. This corresponds to the values defined in RFC 3164 except None which means that the message won't be sent.
    /// </summary>
    public enum Severity
    {
        /// <summary>
        /// Not an official RFC3164 facility. No entry will be generated when called with this Severity
        /// </summary>
        None = -1,
        /// <summary>
        /// Emergency: system is unusable. No equivalent in <see cref="LogLevel"/>
        /// </summary>
        Emergency,
        /// <summary>
        /// Alert: action must be taken immediately. No equivalent in <see cref="LogLevel"/>
        /// </summary>
        Alert,
        /// <summary>
        /// Critical: critical conditions. Mapped to <see cref="LogLevel.Critical"/>
        /// </summary>
        Critical,
        /// <summary>
        /// Error: error conditions. Mapped to <see cref="LogLevel.Error"/>
        /// </summary>
        Error,
        /// <summary>
        /// Warning: warning conditions. Mapped to <see cref="LogLevel.Warning"/>
        /// </summary>
        Warning,
        /// <summary>
        /// Notice: normal but significant condition. No equivalent in <see cref="LogLevel"/>
        /// </summary>
        Notice,
        /// <summary>
        /// Informational: informational messages. Mapped to <see cref="LogLevel.Information"/>
        /// </summary>
        Informational,
        /// <summary>
        /// Debug: debug-level messages. Mapped to <see cref="LogLevel.Debug"/> and <see cref="LogLevel.Trace"/>
        /// </summary>
        Debug
    };
}
