//
// Copyright (c) .NET Foundation and Contributors
// See LICENSE file in the project root for full license information.
//

using System;
using Microsoft.Extensions.Logging;
using Windows.Devices.SerialCommunication;
using Windows.Storage.Streams;

namespace nanoFramework.Logging.Serial
{
    /// <summary>
    /// A logger that prints to the debug console
    /// </summary>
    public class SerialLogger : ILogger
    {
        private readonly DataWriter _outputDataWriter;

        /// <summary>
        /// Creates a new instance of the <see cref="DebugLogger"/>
        /// </summary>
        /// <param name="serialDevice">The serial port to use</param>
        public SerialLogger(ref SerialDevice serialDevice)
        {
            SerialDevice = serialDevice;
            _outputDataWriter = new DataWriter(serialDevice.OutputStream);
            MinLogLevel = LogLevel.Debug;
        }

        /// <summary>
        /// Name of the serial device
        /// </summary>
        public SerialDevice SerialDevice { get; }

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
                _outputDataWriter.WriteString(msg);
                _outputDataWriter.Store();
            }
        }
    }
}
