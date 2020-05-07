using System;
using NUnit.Framework;

namespace Grundfos.WaterDemandCalculation.Tests
{
    [TestFixture]
    public class SimulationStartTimeResolverTests
    {
        [TestCase("2019-09-10 12:13:07",  5,     "2019-09-10 12:10:00")]
        [TestCase("2019-09-10 12:14:07",  5,     "2019-09-10 12:10:00")]
        [TestCase("2019-09-10 12:15:07",  5,     "2019-09-10 12:15:00")]
        [TestCase("2019-09-10 12:16:07",  5,     "2019-09-10 12:15:00")]
        [TestCase("2019-09-10 12:17:07",  5,     "2019-09-10 12:15:00")]
        [TestCase("2019-09-10 12:18:07",  5,     "2019-09-10 12:15:00")]

        [TestCase("2019-09-10 12:18:07", 10,     "2019-09-10 12:10:00")]
        [TestCase("2019-09-10 12:21:07", 10,     "2019-09-10 12:20:00")]
        public void GetRoundedSimulationStartTime_Test(DateTime start, int interval, DateTime expectedDateTime)
        {
            Assert.AreEqual(
                SimulationStartTimeResolver.GetRoundedSimulationStartTime(start, interval),
                expectedDateTime
            );
        }
    }
}
