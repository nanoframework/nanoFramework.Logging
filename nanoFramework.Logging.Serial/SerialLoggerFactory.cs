//
// Copyright (c) .NET Foundation and Contributors
// See LICENSE file in the project root for full license information.
//

using Microsoft.Extensions.Logging;
using System.IO.Ports;

namespace nanoFramework.Logging.Serial
{
    /// <summary>
    /// Provides a simple Serial Device logger
    /// </summary>
    public class SerialLoggerFactory : ILoggerFactory
    {
        private SerialPort _serial;
        private readonly string _comPort;
        private readonly int _baudRate;
        private readonly ushort _dataBits;
        private readonly Parity _parity;
        private readonly StopBits _stopBits;
        private readonly Handshake _handshake;

        /// <summary>
        /// Create a new instance of <see cref="SerialLoggerFactory"/> from a <see cref="SerialDevice"/>.
        /// </summary>
        /// <param name="comPort"></param>
        /// <param name="baudRate"></param>
        /// <param name="dataBits"></param>
        /// <param name="parity"></param>
        /// <param name="stopBits"></param>
        /// <param name="handshake"></param>
        public SerialLoggerFactory(
            string comPort,
            int baudRate = 9600,
            ushort dataBits = 8,
            Parity parity = Parity.None,
            StopBits stopBits = StopBits.One,
            Handshake handshake = Handshake.None)
        {
            _comPort = comPort;
            _baudRate = baudRate;
            _dataBits = dataBits;
            _parity = parity;
            _stopBits = stopBits;
            _handshake = handshake;
        }

        /// <inheritdoc/>
        public ILogger CreateLogger(string categoryName)
        {
            _serial = new SerialPort(_comPort);
            _serial.BaudRate = _baudRate;
            _serial.Parity = _parity;
            _serial.StopBits = _stopBits;
            _serial.Handshake = _handshake;
            _serial.DataBits = _dataBits;
            return new SerialLogger(ref _serial, categoryName);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _serial.Dispose();
        }
    }
}