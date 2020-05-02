using NUnit.Framework;

namespace Grundfos.WaterDemandCalculation.Tests
{
    [TestFixture]
    public class TimeshiftAdjusterTests
    {
        public const double Period = 10d;

        [TestCase(0.1, ExpectedResult = 0.1d)]
        [TestCase(Period, ExpectedResult = 0d)]
        [TestCase(2 * Period, ExpectedResult = 0d)]
        [TestCase(Period + 1, ExpectedResult = 1d)]
        [TestCase(-1, ExpectedResult = Period - 1)]
        [TestCase(-(2 * Period), ExpectedResult = 0.0d)]
        [TestCase(-(3 * Period) - 2, ExpectedResult = Period - 2)]
        public double GetAdjustedTime_Tests(double timeshiftMinutes)
        {
            var adjuster = new TimeshiftAdjuster(10);
            double result = adjuster.GetAdjustedTime(timeshiftMinutes);
            return result;
        }
    }
}
