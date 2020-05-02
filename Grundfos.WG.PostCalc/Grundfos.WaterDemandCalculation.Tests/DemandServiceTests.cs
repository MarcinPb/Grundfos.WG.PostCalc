using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;

namespace Grundfos.WaterDemandCalculation.Tests
{
    [TestFixture]
    public class DemandServiceTests
    {
        public const double Delta = 0.000000000000001;

        [Test]
        public void GetDemandAt_Tests()
        {
            var adjuster = new TimeshiftAdjuster(7 * 24 * 60);
            var interpolator = new Interpolator(adjuster);
            var patternService = new Mock<IDemandPatternService>();
            patternService.Setup(x => x.GetWaterDemandPattern(It.IsAny<string>())).Returns(TestData.WaterDemandPatterns.GetLinearPattern7Days());
            var service = new DemandService(interpolator, patternService.Object);

            double actual10 = service.GetDemandAt("any", 10);
            Assert.AreEqual(10, actual10, Delta);

            double actual15_5 = service.GetDemandAt("any", 15.5);
            Assert.AreEqual(15.5, actual15_5, Delta);

            double actual1500 = service.GetDemandAt("any", 1500);
            Assert.AreEqual(1500, actual1500, Delta);

            double mondayNextWeek = (7 * 24 * 60) + 150;
            double actualMondayNextWeek = service.GetDemandAt("any", mondayNextWeek);
            Assert.AreEqual(150, actualMondayNextWeek, Delta);
        }
    }
}
