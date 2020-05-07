using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Grundfos.WaterDemandCalculation.Tests_2
{
    [TestClass]
    public class TimeshiftAdjusterTests
    {
        public const double Period = 10d;

        [TestMethod]
        [DataRow(0.1, 0.1)]
        [DataRow(0.1, 0.1d)]
        [DataRow(Period, 0d)]
        [DataRow(2 * Period, 0d)]
        [DataRow(Period + 1, 1d)]
        [DataRow(-1, Period - 1)]
        [DataRow(-(2 * Period), 0.0d)]
        [DataRow(-(3 * Period) - 2, Period - 2)]
        public void GetAdjustedTime_Tests(double timeshiftMinutes, double expected)
        {
            var adjuster = new TimeshiftAdjuster(10);
            double actual = adjuster.GetAdjustedTime(timeshiftMinutes);
            Assert.AreEqual(expected, actual);
        }
    }
}
