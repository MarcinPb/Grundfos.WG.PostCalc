using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Grundfos.WaterDemandCalculation.Tests
{
    [TestFixture]
    public class SimulationTimeResolverTests
    {
        [TestCase("2019-09-10 12:19:30", "2019-09-10 12:10:00")]
        [TestCase("2019-09-10 12:21:30", "2019-09-10 12:20:00")]
        [TestCase("2019-09-10 12:22:30", "2019-09-10 12:20:00")]
        [TestCase("2019-09-10 12:23:30", "2019-09-10 12:20:00")]
        [TestCase("2019-09-10 12:24:30", "2019-09-10 12:20:00")]
        public void GetSimulationTimestamp_Tests(DateTime input, DateTime output)
        {
            var config = new SimulationTimeResolverConfiguration
            {
                SimulationStartTime = new DateTime(2019, 09, 10,   12, 12, 07),
                SimulationIntervalMinutes = 10,
            };

            var resolver = new SimulationTimeResolver(config);
            var actual = resolver.GetSimulationTimestamp(input);
            Assert.AreEqual(output, actual);
        }
    }
}
