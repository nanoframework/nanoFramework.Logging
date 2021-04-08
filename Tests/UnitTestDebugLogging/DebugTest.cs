//
// Copyright (c) .NET Foundation and Contributors
// See LICENSE file in the project root for full license information.
//

using nanoFramework.Logging;
using nanoFramework.Logging.Debug;
using nanoFramework.TestFramework;

namespace UnitTestDebugLogging
{
    [TestClass]
    public class DebugTest
    {
        [Setup]
        public void SetupDebugTest()
        {
            LogDispatcher.LoggerFactory = new DebugLoggerFactory();
        }

        [TestMethod]
        public void TestDebugTest()
        {
            MyTestComponent test = new MyTestComponent();
            test.DoSomeLogging();
        }

        [Cleanup]
        public void CleanDebugTest()
        {
            LogDispatcher.LoggerFactory = null;
        }
    }
}
