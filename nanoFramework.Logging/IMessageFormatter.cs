using Microsoft.Extensions.Logging;
using System;

namespace nanoFramework.Logging
{
    /// <summary>
    /// Provide the skeleton for a Message Formatter logger
    /// </summary>
    public interface IMessageFormatter
    {
        /// <summary>
        /// The function to format the message
        /// </summary>
        /// <param name="className">The name of the logger</param>
        /// <param name="logLevel">The log level</param>
        /// <param name="eventId">The event id</param>
        /// <param name="state">The message itself</param>
        /// <param name="exception">The exception</param>
        /// <returns>A formatted string</returns>
        string MessageFormatter(string className, LogLevel logLevel, EventId eventId, string state, Exception exception);
    }
}
