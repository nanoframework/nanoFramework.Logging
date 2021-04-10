//
// Copyright (c) .NET Foundation and Contributors
// See LICENSE file in the project root for full license information.
//
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace nanoFramework.Logging.Stream
{
    /// <summary>
    /// A logger that outputs to a <see cref="Stream"/>.
    /// </summary>
    public class StreamLogger : ILogger
    {
        private readonly System.IO.Stream _stream = null;

        /// <summary>
        /// Creates a new instance of the <see cref="ILogger"/>
        /// </summary>
        /// <param name="stream">Stream to output the log to.</param>
        /// <param name="loggerName">The logger name</param>
        public StreamLogger(System.IO.Stream stream, string loggerName)
        {
            _stream = stream;
            LoggerName = loggerName;
            MinLogLevel = LogLevel.Debug;
        }

        /// <summary>
        /// Name of the logger
        /// </summary>
        public string LoggerName { get; }

        /// <summary>
        /// Name of the logger
        /// </summary>
        public System.IO.Stream BaseStream { get; }

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
                string msgStream;
                if (format == null)
                {
                    msgStream = exception == null ? $"{state}\r\n" : $"{state} {exception}\r\n";
                }
                else
                {
                    msgStream = $"{(string)format.Invoke(null, new object[] { LoggerName, logLevel, eventId, state, exception })}\r\n";
                }
                
                byte[] sampleBuffer = Encoding.UTF8.GetBytes(msgStream);
                _stream.Seek(0, SeekOrigin.End);
               _stream.Write(sampleBuffer, 0, sampleBuffer.Length);
            }
        }
    }
}
