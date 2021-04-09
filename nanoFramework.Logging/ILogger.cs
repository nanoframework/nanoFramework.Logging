﻿//
// Copyright (c) .NET Foundation and Contributors
// See LICENSE file in the project root for full license information.
//

using System;
using System.Reflection;

namespace Microsoft.Extensions.Logging
{
    /// <summary>
    /// Represents a type used to perform logging.
    /// </summary>
    /// <remarks>Aggregates most logging patterns to a single method.</remarks>
    public interface ILogger
    {
        /// <summary>
        /// Writes a log entry.
        /// </summary>
        /// <param name="logLevel">Entry will be written on this level.</param>
        /// <param name="eventId">Id of the event.</param>
        /// <param name="state">The entry to be written.</param>
        /// <param name="exception">The exception related to this entry.</param>
        /// <param name="format"></param>
        void Log(LogLevel logLevel, EventId eventId, string state, Exception exception, MethodInfo format);

        /// <summary>
        /// Checks if the given <paramref name="logLevel"/> is enabled.
        /// </summary>
        /// <param name="logLevel">level to be checked.</param>
        /// <returns><c>true</c> if enabled.</returns>
        bool IsEnabled(LogLevel logLevel);
    }
}
