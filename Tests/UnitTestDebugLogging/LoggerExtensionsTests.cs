//
// Copyright (c) .NET Foundation and Contributors
// See LICENSE file in the project root for full license information.
//

using Microsoft.Extensions.Logging;
using nanoFramework.Logging;
using nanoFramework.Logging.Debug;
using nanoFramework.TestFramework;
using System;
using System.Diagnostics;

namespace UnitTestDebugLogging
{
    [TestClass]
    public class LoggerExtensionsTests
    {
        private static ILogger _logger;

        /// <summary>
        /// Initializes a static logger in the debug output. If none of the function throughs, then the output should be correct.
        /// </summary>
        [Setup]
        public void GetLogger()
        {
            _logger = new DebugLogger("LoggerExtensionsTests");
            ((DebugLogger)_logger).MinLogLevel = LogLevel.Trace;
        }

        /// <summary>
        /// LogInformation Test with formatted string with some parameters.
        /// </summary>
        [TestMethod]
        public void LogInformationFormatedStringWithParams()
        {
            _logger.LogInformation("{0}{1}{2}", "nano", "frame", "work");
        }

        /// <summary>
        /// LogWarning Test with formatted string with some parameters.
        /// </summary>
        [TestMethod]
        public void LogWarningFormatedStringWithParams()
        {
            _logger.LogWarning("{0}{1}{2}", "nano", "frame", "work");
        }

        /// <summary>
        /// LogTrace Test with formatted string with some parameters.
        /// </summary>
        [TestMethod]
        public void LogTraceFormatedStringWithParams()
        {
            _logger.LogTrace("{0}{1}{2}", "nano", "frame", "work");
        }

        /// <summary>
        /// LogError Test with formatted string with some parameters.
        /// </summary>
        [TestMethod]
        public void LogErrorFormatedStringWithParams()
        {
            _logger.LogError("{0}{1}{2}", "nano", "frame", "work");
        }

        /// <summary>
        /// LogDebug Test with formatted string with some parameters.
        /// </summary>
        [TestMethod]
        public void LogDebugFormatedStringWithParams()
        {
            _logger.LogDebug("{0}{1}{2}", "nano", "frame", "work");
        }

        /// <summary>
        /// LogCritical Test with formatted string with some parameters.
        /// </summary>
        [TestMethod]
        public void LogCriticalFormatedStringWithParams()
        {
            _logger.LogCritical("{0}{1}{2}", "nano", "frame", "work");
        }

        /// <summary>
        /// Log Test with formatted string with some parameters and all log levels.
        /// </summary>
        [TestMethod]
        public void LogFormatedStringWithParams()
        {
            for (int i = 0; i < 7; i++)
            {
                _logger.Log((LogLevel)i, "{0}{1}{2}", "nano", "frame", "work");
            }
        }

        /// <summary>
        /// string.Format on a json string doesn't work well.
        /// </summary>
        [TestMethod]
        public void LogJsonStringWithParams()
        {
            Assert.ThrowsException(typeof(ArgumentException), LogJsonStringWithParamsThrowEx);
        }

        /// <summary>
        /// Incorrect call.
        /// </summary>
        private void LogJsonStringWithParamsThrowEx()
        {
            _logger.LogError(@"{ ""Message"":""nanoframework"" }", 1, 2);
        }

        /// <summary>
        /// Logging a json formatted string.
        /// </summary>
        [TestMethod]
        public void LogJsonStringWithoutParams()
        {
            _logger.LogInformation(@"{ ""Message"":""nanoframework"" }");
        }

        /// <summary>
        /// Logging with null params.
        /// </summary>
        [TestMethod]
        public void LogNullArgumentExtension()
        {
            _logger.Log(LogLevel.Debug, "Null arguments", null);
        }

        /// <summary>
        /// Logging with empty params.
        /// </summary>
        [TestMethod]
        public void LogEmptyArgumentExtension()
        {
            _logger.Log(LogLevel.Debug, "Null arguments", new object[0]);
        }
    }
}
