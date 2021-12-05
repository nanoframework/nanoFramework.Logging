using nanoFramework.TestFramework;
using System;
using System.Diagnostics;
using nanoFramework.Logging;
using Microsoft.Extensions.Logging;
using nanoFramework.Logging.Debug;

namespace UnitTestDebugLogging
{
    [TestClass]
    class FormattingTest
    {
        static DebugLogger _logger;

        [Setup]
        public void SteupFormattingTest()
        {
            _logger = new DebugLogger(nameof(FormattingTest));
            LoggerExtensions.MessageFormatter = typeof(MyFormatter).GetType().GetMethod("MessageFormatterStatic");
            Debug.WriteLine($"{LoggerExtensions.MessageFormatter.Name}");
            _logger.MinLogLevel = LogLevel.Trace;
        }

        [TestMethod]
        public void TestInvoke()
        {
            string msg = (string)LoggerExtensions.MessageFormatter.Invoke(null, new object[] { "test", LogLevel.Trace, new EventId(0), "some text", null });
            Debug.WriteLine(msg);
        }

        [TestMethod]
        public void TestFormatting()
        {
            LogAll();
        }

        [TestMethod]
        public void TestFormattingSimple()
        {
            LoggerExtensions.MessageFormatter = typeof(MyFormatter).GetType().GetMethod("MessageFormatterSimple");            
            LogAll();
        }

        private void LogAll()
        {
            Debug.WriteLine($"Expexcted level: {_logger.MinLogLevel}");
            _logger.LogTrace("{0} {1}", new object[] { "param 1", 42 });
            _logger.LogDebug("{0} {1}", new object[] { "param 1", 42 });
            _logger.LogInformation("Just some information and nothing else");
            _logger.LogWarning("{0} {1}", new object[] { "param 1", 42 });
            _logger.LogError(new Exception("Big problem"), "{0} {1}", new object[] { "param 1", 42 });
            _logger.LogCritical(42, new Exception("Insane problem"), "{0} {1}", new object[] { "param 1", 42 });
        }

        [Cleanup]
        public void CleanupFormattingTest()
        {
            LoggerExtensions.MessageFormatter = null;
        }
    }

    public class MyFormatter : IMessageFormatter
    {
        public string MessageFormatter(string className, LogLevel logLevel, EventId eventId, string state, Exception exception)
            => MessageFormatterStatic(className, logLevel, eventId, state, exception);

        public string MessageFormatterStatic(string className, LogLevel logLevel, EventId eventId, string state, Exception exception)
        {
            string logstr = string.Empty;
            switch (logLevel)
            {
                case LogLevel.Trace:
                    logstr = "TRACE: ";
                    break;
                case LogLevel.Debug:
                    logstr = "I love debug: ";
                    break;
                case LogLevel.Warning:
                    logstr = "WARNING: ";
                    break;
                case LogLevel.Error:
                    logstr = "ERROR: ";
                    break;
                case LogLevel.Critical:
                    logstr = "CRITICAL:";
                    break;
                case LogLevel.None:
                case LogLevel.Information:
                default:
                    break;
            }

            string eventstr = eventId.Id != 0 ? $" Event ID: {eventId}, " : string.Empty;
            string msg = $"[{className}] {eventstr}{logstr} {state}";
            if (exception != null)
            {
                msg += $" {exception}";
            }

            return msg;
        }

        public string MessageFormatterSimple(string className, LogLevel logLevel, EventId eventId, string state, Exception exception)
        {
            string msg = $"[{className}] {logLevel}-{state}";
            if (exception != null)
            {
                msg += $" {exception}";
            }

            return msg;
        }
    }
}
