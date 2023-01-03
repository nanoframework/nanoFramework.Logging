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
        public ILogger GetLogger()
        {
            var logger = new DebugLogger("LoggerExtensionsTests");
            logger.MinLogLevel = LogLevel.Trace;
            return logger;
        }
        /// <summary>
        /// LogInformation Test with formatted string with some parameters
        /// </summary>
        [TestMethod]
        public void LogInformationFormatedStringWithParams()
        {
            GetLogger().LogInformation("{0}{1}{2}", "nano", "frame", "work");
        }
        /// <summary>
        /// LogWarning Test with formatted string with some parameters
        /// </summary>
        [TestMethod]
        public void LogWarningFormatedStringWithParams()
        {
            GetLogger().LogWarning("{0}{1}{2}", "nano", "frame", "work");
        }
        /// <summary>
        /// LogTrace Test with formatted string with some parameters
        /// </summary>
        [TestMethod]
        public void LogTraceFormatedStringWithParams()
        {
            GetLogger().LogTrace("{0}{1}{2}", "nano", "frame", "work");
        }
        /// <summary>
        /// LogError Test with formatted string with some parameters
        /// </summary>
        [TestMethod]
        public void LogErrorFormatedStringWithParams()
        {
            GetLogger().LogError("{0}{1}{2}", "nano", "frame", "work");
        }
        /// <summary>
        /// LogDebug Test with formatted string with some parameters
        /// </summary>
        [TestMethod]
        public void LogDebugFormatedStringWithParams()
        {
            GetLogger().LogDebug("{0}{1}{2}", "nano", "frame", "work");
        }
        /// <summary>
        /// LogCritical Test with formatted string with some parameters
        /// </summary>
        [TestMethod]
        public void LogCriticalFormatedStringWithParams()
        {
            GetLogger().LogCritical("{0}{1}{2}", "nano", "frame", "work");
        }
        /// <summary>
        /// Log Test with formatted string with some parameters and all log levels
        /// </summary>
        [TestMethod]
        public void LogFormatedStringWithParams()
        {
            for (int i = 0; i < 7; i++)
            {
                GetLogger().Log((LogLevel)i, "{0}{1}{2}", "nano", "frame", "work");
            }
        }
        /// <summary>
        /// string.Format on a json string doesn't work well
        /// </summary>
        [TestMethod]
        public void LogJsonStringWithParams()
        {
            Assert.ThrowsException(typeof(ArgumentException), LogJsonStringWithParamsThrowEx);
        }
        /// <summary>
        /// Incorrect call
        /// </summary>
        private void LogJsonStringWithParamsThrowEx()
        {
            GetLogger().LogError(@"{ ""Message"":""nanoframework"" }", 1, 2);
        }
        /// <summary>
        /// Logging a json formatted string
        /// </summary>
        [TestMethod]
        public void LogJsonStringWitoutParams()
        {
            GetLogger().LogInformation(@"{ ""Message"":""nanoframework"" }");
        }

    }
}
