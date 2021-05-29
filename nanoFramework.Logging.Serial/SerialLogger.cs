//
// Copyright (c) .NET Foundation and Contributors
// See LICENSE file in the project root for full license information.
//

using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.IO.Ports;
using System.Reflection;

namespace nanoFramework.Logging.Serial
{
    /// <summary>
    /// A logger that outputs to a <see cref="SerialDevice"/>.
    /// </summary>
    public class SerialLogger : ILogger
    {
        private readonly SerialPort _serialPort;

        /// <summary>
        /// Creates a new instance of the <see cref="SerialLogger"/>
        /// </summary>
        /// <param name="serialDevice">The serial port to use</param>
        /// <param name="loggerName">The logger name</param>
        public SerialLogger(ref SerialPort serialDevice, string loggerName)
        {
            _serialPort = serialDevice;
            if (!_serialPort.IsOpen)
            {
                _serialPort.Open();
            }

            LoggerName = loggerName;
            MinLogLevel = LogLevel.Debug;
        }

        /// <summary>
        /// Name of the logger
        /// </summary>
        public string LoggerName { get; }

        /// <summary>
        /// Name of the serial device
        /// </summary>
        public SerialPort SerialPort => _serialPort;

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
                string msgSerial;
                if (format == null)
                {
                    msgSerial = exception == null ? $"{state}\r\n" : $"{state} {exception}\r\n";
                }
                else
                {
                    msgSerial = $"{(string)format.Invoke(null, new object[] { LoggerName, logLevel, eventId, state, exception })}\r\n";
                }

                _serialPort.Write(msgSerial);
            }
        }
    }
}
