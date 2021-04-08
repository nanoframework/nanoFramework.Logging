//
// Copyright (c) .NET Foundation and Contributors
// See LICENSE file in the project root for full license information.
//

using Microsoft.Extensions.Logging;
using nanoFramework.Logging.Debug;
using nanoFramework.TestFramework;
using System;
using System.Diagnostics;

namespace UnitTestDebugLogging
{
    [TestClass]
    class TestLogLevels
    {
        static DebugLogger _logger;

        [Setup]
        public void SetupLoggingLevel()
        {
            _logger = new DebugLogger("test");
        }

        [TestMethod]
        public void TestMethodTrace()
        {
            _logger.MinLogLevel = LogLevel.Trace;
            LogAll();
        }

        [TestMethod]
        public void TestMethodDebug()
        {
            _logger.MinLogLevel = LogLevel.Debug;
            LogAll();
        }

        [TestMethod]
        public void TestMethodInformation()
        {
            _logger.MinLogLevel = LogLevel.Information;
            LogAll();
        }

        [TestMethod]
        public void TestMethodWarning()
        {
            _logger.MinLogLevel = LogLevel.Warning;
            LogAll();
        }

        [TestMethod]
        public void TestMethodError()
        {
            _logger.MinLogLevel = LogLevel.Error;
            LogAll();
        }

        [TestMethod]
        public void TestMethodCritical()
        {
            _logger.MinLogLevel = LogLevel.Critical;
            LogAll();
        }

        [TestMethod]
        public void TestMethodNone()
        {
            _logger.MinLogLevel = LogLevel.None;
            LogAll();
        }

        private void LogAll()
        {
            Debug.WriteLine($"Expexcted level: {_logger.MinLogLevel}");
            _logger.LogTrace("TRACE {0} {1}", new object[] { "param 1", 42 });
            _logger.LogDebug("DEBUG {0} {1}", new object[] { "param 1", 42 });
            _logger.LogInformation("INFORMATION and nothing else");
            _logger.LogWarning("WARNING {0} {1}", new object[] { "param 1", 42 });
            _logger.LogError(new Exception("Big problem"), "ERROR {0} {1}", new object[] { "param 1", 42 });
            _logger.LogCritical(42, new Exception("Insane problem"), "CRITICAL {0} {1}", new object[] { "param 1", 42 });
        }
    }
}
