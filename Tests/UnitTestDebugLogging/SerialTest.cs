//
// Copyright (c) .NET Foundation and Contributors
// See LICENSE file in the project root for full license information.
//

using nanoFramework.Logging;
using nanoFramework.Logging.Serial;
using nanoFramework.TestFramework;
using System;
#if BUIID_FOR_ESP32
using nanoFramework.Hardware.Esp32;
#endif

namespace UnitTestDebugLogging
{
    [TestClass]
    class SerialTest
    {
        [Setup]
        public void SetupSerialHardware()
        {
            // Remove this line to run hardware tests
            Assert.SkipTest("No serial hardware test");
            try
            {
#if BUIID_FOR_ESP32
                ////////////////////////////////////////////////////////////////////////////////////////////////////
                // COM2 in ESP32-WROVER-KIT mapped to free GPIO pins
                // mind to NOT USE pins shared with other devices, like serial flash and PSRAM
                // also it's MANDATORY to set pin funcion to the appropriate COM before instanciating it

                // set GPIO functions for COM2 (this is UART1 on ESP32)
                Configuration.SetPinFunction(Gpio.IO04, DeviceFunction.COM2_TX);
                Configuration.SetPinFunction(Gpio.IO05, DeviceFunction.COM2_RX);

                // open COM2
                LogDispatcher.LoggerFactory = new SerialLoggerFactory("COM2");
#else
                ///////////////////////////////////////////////////////////////////////////////////////////////////
                // COM6 in STM32F769IDiscovery board (Tx, Rx pins exposed in Arduino header CN13: TX->D1, RX->D0)
                // open COM6
                LogDispatcher.LoggerFactory = new SerialLoggerFactory("COM6");
#endif
            }
            catch (Exception ex)
            {
                LogDispatcher.LoggerFactory = null;
                Assert.SkipTest($"Can't properly configure the serial port, reason: {ex.Message}");
            }
        }

        [TestMethod]
        public void TestMethodSerialDebug()
        {
            MyTestComponent test = new MyTestComponent();
            test.DoSomeLogging();
        }

        [Cleanup]
        public void CleanLoggingSerial()
        {
            LogDispatcher.LoggerFactory = null;
        }
    }
}
