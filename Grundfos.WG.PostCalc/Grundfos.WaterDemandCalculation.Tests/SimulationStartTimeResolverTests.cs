using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Grundfos.WaterDemandCalculation.Tests
{
    [TestFixture]
    public class SimulationStartTimeResolverTests
    {
        [Test]
        public void GetRoundedSimulationStartTime_Test()
        {
            Assert.AreEqual(
                new DateTime(2019, 09, 10, 12, 10, 00),
                SimulationStartTimeResolver.GetRoundedSimulationStartTime(new DateTime(2019, 09, 10, 12, 13, 07), 5)
            );

            Assert.AreEqual(
                new DateTime(2019, 09, 10, 12, 10, 00),
                SimulationStartTimeResolver.GetRoundedSimulationStartTime(new DateTime(2019, 09, 10, 12, 14, 07), 5)
            );

            Assert.AreEqual(
                new DateTime(2019, 09, 10, 12, 15, 00),
                SimulationStartTimeResolver.GetRoundedSimulationStartTime(new DateTime(2019, 09, 10, 12, 15, 07), 5)
            );

            Assert.AreEqual(
                new DateTime(2019, 09, 10, 12, 15, 00),
                SimulationStartTimeResolver.GetRoundedSimulationStartTime(new DateTime(2019, 09, 10, 12, 16, 07), 5)
            );

            Assert.AreEqual(
                new DateTime(2019, 09, 10, 12, 15, 00),
                SimulationStartTimeResolver.GetRoundedSimulationStartTime(new DateTime(2019, 09, 10, 12, 17, 07), 5)
            );

            Assert.AreEqual(
                new DateTime(2019, 09, 10, 12, 15, 00),
                SimulationStartTimeResolver.GetRoundedSimulationStartTime(new DateTime(2019, 09, 10, 12, 18, 07), 5)
            );
        }
    }
}
