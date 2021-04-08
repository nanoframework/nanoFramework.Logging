//
// Copyright (c) .NET Foundation and Contributors
// See LICENSE file in the project root for full license information.
//

using nanoFramework.Logging;
using nanoFramework.Logging.Stream;
using nanoFramework.TestFramework;
using System.IO;
using System.Text;

namespace UnitTestDebugLogging
{
    [TestClass]
    class MemoryStreamTests
    {
        [TestMethod]
        public void TestStreamFile()
        {
            MemoryStream memoryStream = new MemoryStream();
            LogDispatcher.LoggerFactory = new StreamLoggerFactory(memoryStream);
            MyTestComponent test = new MyTestComponent();
            test.DoSomeLogging();
            // Open the file and check what is inside
            byte[] fsContent = new byte[memoryStream.Length];
            memoryStream.Seek(0, SeekOrigin.Begin);
            memoryStream.Read(fsContent, 0, fsContent.Length);
            var fsText = Encoding.UTF8.GetString(fsContent, 0, fsContent.Length);
            Assert.StartsWith("An informative message", fsText);
            Assert.EndsWith("With exception context System.Exception: Something is not supported", fsText);
        }

        [Cleanup]
        public void CleaupMemoryStream()
        {
            LogDispatcher.LoggerFactory = null;
        }
    }
}
