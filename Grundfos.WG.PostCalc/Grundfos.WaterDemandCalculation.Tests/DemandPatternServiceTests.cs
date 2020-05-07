using System;
using System.IO;
using System.Linq;
using Grundfos.Workbooks;
using Moq;
using NUnit.Framework;

namespace Grundfos.WaterDemandCalculation.Tests
{
    [TestFixture]
    public class DemandPatternServiceTests
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


        [TestCase(390, 1.01)]
        //[TestCase(391, 1.01666)]
        [TestCase(393, 1.03)]
        [TestCase(420, 1.19)]
        public void DemandReaderTests(double timeFromStart, double expectedValue)
        {
            var testDirectory = TestContext.CurrentContext.TestDirectory;
            var testFileName = @"TestData\WaterDemandSettings.xlsx";
            var testFilePath = Path.Combine(testDirectory, testFileName);
            bool fileEexists = File.Exists(testFilePath);

            var excelReader = new ExcelReader(testFilePath);
            var demandPatternExcelReader = new DemandPatternExcelReader(excelReader);

            var demandPatternService = new DemandPatternService(demandPatternExcelReader);
            var waterDemandPattern = demandPatternService.GetWaterDemandPattern("Urz");
            var resultCount = waterDemandPattern.Profile.Count;
            int expectedCount = 3840;
            Assert.AreEqual(resultCount, expectedCount);

            var resultValue = waterDemandPattern.Profile
                .FirstOrDefault(x => Math.Abs(x.TimeshiftMinutes - timeFromStart) < 0.1)?.Value ?? -1;

            Assert.AreEqual(resultValue, expectedValue);
        }
    }
}
