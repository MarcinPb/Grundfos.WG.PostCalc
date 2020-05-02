using System.IO;
using Grundfos.Workbooks;
using NUnit.Framework;

namespace Grundfos.WaterDemandCalculation.Tests
{
    [TestFixture]
    public class DemandPatternServiceTests
    {
        [Test]
        public void DemandReaderTests()
        {
            var testDirectory = TestContext.CurrentContext.TestDirectory;
            var testFileName = @"TestData\WaterDemandSettings.xlsx";
            var testFilePath = Path.Combine(testDirectory, testFileName);
            bool result = File.Exists(testFilePath);

            var excel = new ExcelReader(testFilePath);
            var excelReader = new DemandPatternExcelReader(excel);

            var service = new DemandPatternService(excelReader);
        }
    }
}
