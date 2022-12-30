//
// Copyright (c) .NET Foundation and Contributors
// See LICENSE file in the project root for full license information.
//

using Microsoft.Extensions.Logging;
using nanoFramework.Logging;
using nanoFramework.Logging.Debug;
using nanoFramework.TestFramework;
using System;

namespace UnitTestDebugLogging
{
    [TestClass]
    public class LoggerExtensionsTests
    {
        public LoggerExtensionsTests Instance { get; set; }
        private ILogger _logger;
        private string _jsonString = @"{ ""Message"":""Huhu"" }";

        [Setup]
        public void SetupDebugTest()
        {
            Instance = new LoggerExtensionsTests();
            LogDispatcher.LoggerFactory = new DebugLoggerFactory();
            Instance._logger = this.GetCurrentClassLogger();
        }

        [TestMethod]
        public void LogTest()
        {
            //LogFormatedStringWithParams();
            //LogJsonStringWitoutParams();
            //Assert.ThrowsException(typeof(NullReferenceException), LogJsonStringWithParams);
        }

        private void LogFormatedStringWithParams()
        {
            //DebugLogger _logger;
            //_logger = new DebugLogger("test");

            //var log = LogDispatcher.GetLogger("Testlogger");
            _logger.LogInformation("{1}{2}", 1, 2);
        }
        private void LogJsonStringWithParams()
        {
            //DebugLogger _logger;
            //_logger = new DebugLogger("test");
            //var log = LogDispatcher.GetLogger("Testlogger");
            _logger.LogError(_jsonString, 1, 2);
        }
        private void LogJsonStringWitoutParams()
        {
            //DebugLogger _logger;
            //_logger = new DebugLogger("test");

            //var log = LogDispatcher.GetLogger("Testlogger");
            _logger.LogInformation(_jsonString);
        }

        [Cleanup]
        public void CleanDebugTest()
        {
            LogDispatcher.LoggerFactory = null;
        }
    }
}
