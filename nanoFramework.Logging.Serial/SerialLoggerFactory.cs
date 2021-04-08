//
// Copyright (c) .NET Foundation and Contributors
// See LICENSE file in the project root for full license information.
//

using Microsoft.Extensions.Logging;
using Windows.Devices.SerialCommunication;

namespace nanoFramework.Logging.Serial
{
    /// <summary>
    /// Provides a simple Serial Device logger
    /// </summary>
    public class SerialLoggerFactory : ILoggerFactory
    {
        private SerialDevice _serial;
        private string _comPort;
        private uint _baudRate;
        private ushort _dataBits;
        private SerialParity _parity;
        private SerialStopBitCount _stopBits;
        private SerialHandshake _handshake;

        public SerialLoggerFactory(string comPort, uint baudRate = 9600, ushort dataBits = 8, SerialParity parity = SerialParity.None, SerialStopBitCount stopBits = SerialStopBitCount.One, SerialHandshake handshake = SerialHandshake.None)
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
            _serial = SerialDevice.FromId(_comPort);
            _serial.BaudRate = _baudRate;
            _serial.Parity = _parity;
            _serial.StopBits = _stopBits;
            _serial.Handshake = _handshake;
            _serial.DataBits = _dataBits;
            return new SerialLogger(ref _serial);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _serial.Dispose();
        }
    }
}