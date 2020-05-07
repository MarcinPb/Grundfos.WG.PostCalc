using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Grundfos.WaterDemandCalculation.Tests_2
{
    [TestClass]
    public class SimulationTimeResolverTests
    {
        [TestMethod]
        public void GetSimulationTimestamp_Tests()
        {
            var config = new SimulationTimeResolverConfiguration
            {
                SimulationStartTime = new DateTime(2019, 09, 10, 12, 12, 07),
                SimulationIntervalMinutes = 10,
            };

            var resolver = new SimulationTimeResolver(config);

            var expected = new DateTime(2019, 09, 10, 12, 10, 00);
            var actual = resolver.GetSimulationTimestamp(new DateTime(2019, 09, 10, 12, 19, 30));
            Assert.AreEqual(expected, actual);

            expected = new DateTime(2019, 09, 10, 12, 20, 00);
            actual = resolver.GetSimulationTimestamp(new DateTime(2019, 09, 10, 12, 21, 30));
            Assert.AreEqual(expected, actual);

            expected = new DateTime(2019, 09, 10, 12, 20, 00);
            actual = resolver.GetSimulationTimestamp(new DateTime(2019, 09, 10, 12, 22, 30));
            Assert.AreEqual(expected, actual);

            expected = new DateTime(2019, 09, 10, 12, 20, 00);
            actual = resolver.GetSimulationTimestamp(new DateTime(2019, 09, 10, 12, 23, 30));
            Assert.AreEqual(expected, actual);

            expected = new DateTime(2019, 09, 10, 12, 20, 00);
            actual = resolver.GetSimulationTimestamp(new DateTime(2019, 09, 10, 12, 24, 30));
            Assert.AreEqual(expected, actual);
        }
    }
}
