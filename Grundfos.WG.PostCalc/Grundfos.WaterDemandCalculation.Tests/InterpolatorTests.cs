using Grundfos.WaterDemandCalculation.Tests.TestData;
using NUnit.Framework;

namespace Grundfos.WaterDemandCalculation.Tests
{
    [TestFixture]
    public class InterpolatorTests
    {
        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(1.5, 1.5)]
        [TestCase(50, 50)]
        [TestCase(99, 99)]
        [TestCase(100, 0)]
        [TestCase(100.01, 0.01)]
        [TestCase(101, 1)]
        [TestCase(101.5, 1.5)]
        [TestCase(150, 50)]
        [TestCase(199, 99)]
        [TestCase(200, 0)]
        [TestCase(200.01, 0.01)]

        public void GetValueAt_Tests_100(double shift, double expected)
        {
            var adjuster = new TimeshiftAdjuster(100);
            var interpolator = new Interpolator(adjuster);

            var pattern = WaterDemandPatterns.GetLinearPattern100();

            var result = interpolator.GetValueAt(shift, pattern);
            Assert.AreEqual(expected, result, 0.0000001);
        }
    }
}
