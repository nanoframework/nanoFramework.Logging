//
// Copyright (c) .NET Foundation and Contributors
// See LICENSE file in the project root for full license information.
//

using nanoFramework.Logging;
using nanoFramework.TestFramework;

namespace UnitTestDebugLogging
{
    [TestClass]
    class TestWithNullLogger
    {
        [TestMethod]
        public void TestNullLogger()
        {
            LogDispatcher.LoggerFactory = null;
            var testee = new MyTestComponent();
            testee.DoSomeLogging();
        }
    }
}
