//
// Copyright (c) .NET Foundation and Contributors
// See LICENSE file in the project root for full license information.
//
using System;
using System.IO;
using System.Text;
using Microsoft.Extensions.Logging;

namespace nanoFramework.Logging.Stream
{
    /// <summary>
    /// A logger that prints to the debug console
    /// </summary>
    public class StreamLogger : ILogger, IDisposable
    {
        private System.IO.Stream _stream = null;
       
        /// <summary>
        /// Creates a new instance of the <see cref="DebugLogger"/>
        /// </summary>
        /// <param name="fileName">fileName</param>
        public StreamLogger(System.IO.Stream stream)
        {
            _stream = stream;
            MinLogLevel = LogLevel.Debug;
        }

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
        public void Log(LogLevel logLevel, EventId eventId, string state, Exception exception)
        {
            if (logLevel >= MinLogLevel)
            {
                string msg = exception == null ? $"{state}\r\n" : $"{state} {exception}\r\n";
                byte[] sampleBuffer = Encoding.UTF8.GetBytes(msg);
                _stream.Seek(0, SeekOrigin.End);
               _stream.Write(sampleBuffer, 0, sampleBuffer.Length);
            }
        }

        /// <summary>
        /// Dispose properly the stream
        /// </summary>
        public void Dispose()
        {
            if(_stream != null)
            {
                _stream.Dispose();
                _stream = null;
            }
        }
    }
}
