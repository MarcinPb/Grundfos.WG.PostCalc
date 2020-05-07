using System;
using NUnit.Framework;
using Grundfos.WaterDemandCalculation.ExtensionMethods;

namespace Grundfos.WaterDemandCalculation.Tests
{
    [TestFixture]
    public class DateTimeExtensionsTests
    {
        [TestCase("2020-05-04", "2020-05-04")]
        [TestCase("2020-05-07", "2020-05-04")]
        [TestCase("2020-05-08", "2020-05-04")]
        public void StartOfWeek_Test(DateTime dt, DateTime expected)
        {
            var result = dt.StartOfWeek(DayOfWeek.Monday);
            Assert.AreEqual(expected, result);
        }

        [TestCase("2020-05-04 00:10", 10)]
        [TestCase("2020-05-05 00:10", 24*60 + 10)]
        public void MinutesFromMonday_Test(DateTime dt, double expected)
        {
            var result = dt.MinutesFromMonday();
            Assert.AreEqual(expected, result);
        }
    }
}
