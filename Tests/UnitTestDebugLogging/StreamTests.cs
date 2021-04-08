//
// Copyright (c) .NET Foundation and Contributors
// See LICENSE file in the project root for full license information.
//

using nanoFramework.Logging;
using nanoFramework.Logging.Stream;
using nanoFramework.TestFramework;
using System;
using System.IO;
using System.Text;

namespace UnitTestDebugLogging
{
    [TestClass]
    class StreamTests
    {
        const string logFilePath = "C:\\logFile.txt";

        [Setup]
        public void SetupStreamTests()
        {
            try
            {
                LogDispatcher.LoggerFactory = new StreamLoggerFactory(logFilePath);
            }
            catch (Exception ex)
            {
                LogDispatcher.LoggerFactory = null;
                Assert.SkipTest($"Platform not supported, reason: {ex}");
            }           
        }

        [TestMethod]
        public void TestStreamFile()
        {
            MyTestComponent test = new MyTestComponent();
            test.DoSomeLogging();
            // now close the logger
            LogDispatcher.LoggerFactory = null;
            // Open the file and check what is inside
            var fs = new FileStream(logFilePath, FileMode.Open, FileAccess.ReadWrite);
            byte[] fsContent = new byte[fs.Length];
            fs.Read(fsContent, 0, fsContent.Length);
            fs.Dispose();
            var fsText = Encoding.UTF8.GetString(fsContent, 0, fsContent.Length);
            Assert.StartsWith("An informative message", fsText);
            Assert.EndsWith("With exception context System.Exception: Something is not supported", fsText);
        }

        [Cleanup]
        public void CleanupStreamTests()
        {
            LogDispatcher.LoggerFactory = null;
        }
    }
}
