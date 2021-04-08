//
// Copyright (c) .NET Foundation and Contributors
// See LICENSE file in the project root for full license information.
//

using Microsoft.Extensions.Logging;
using nanoFramework.TestFramework;

namespace UnitTestDebugLogging
{
    [TestClass]
    class EventIdTests
    {
        [TestMethod]
        public void TestEventIdBasic()
        {
            EventId eventId = 42;
            Assert.Equal(42, eventId.Id);
            Assert.Null(eventId.Name);
            Assert.Equal("42", eventId.ToString());
            Assert.Equal(42, eventId.GetHashCode());
        }

        [TestMethod]
        public void CompareEventId()
        {
            EventId event1 = 1;
            EventId event1Bis = new EventId(1, "one");
            EventId event2 = 2;
            Assert.Equal(event1.Id, event1Bis.Id);
            Assert.True(event1.Equals(event1Bis));
            Assert.True(event1 == event1Bis);
            Assert.True(event1 != event2);
        }
    }
}
