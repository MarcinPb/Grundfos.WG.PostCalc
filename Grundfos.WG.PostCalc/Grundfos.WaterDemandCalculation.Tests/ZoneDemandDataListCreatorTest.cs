using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grundfos.WaterDemandCalculation.Model;
using Grundfos.WaterDemandCalculation.Tests.TestData;
using Grundfos.WG.PostCalc;
using Haestad.Support.OOP.FileSystem;
using Haestad.Support.OOP.Logging;
using NUnit.Framework;

namespace Grundfos.WaterDemandCalculation.Tests
{
    [TestFixture]
    public class ZoneDemandDataListCreatorTest
    {
        private string _testedZoneName = "1 - Przybków";
        private string _logFolder = Path.Combine(TestContext.CurrentContext.TestDirectory, @"ZoneDemandDataListCreatorTest.log");
        private string dateFormat = "yyyy-MM-dd_HH-mm-ss_fffffff";

        [TestCase("2019-09-10 12:24:30", "2019-09-10 12:20:00")]
        public void Create_Tests(DateTime input, DateTime output)
        {
            var logger = new ActionLogger();
            logger.InitializeLogger(new FileLogger(new FilePath(_logFolder), 10000), OutputLevel.Info);
            
            ZoneDemandDataListCreator.DataContext dataContext = new ZoneDemandDataListCreator.DataContext()
            {
                WgZoneDict = WgObjects.ZoneDict.ToDictionary(x => x.Value, x => x.Key, StringComparer.OrdinalIgnoreCase),
                WgDemandPatternDict = WgObjects.DemandPatternDict, 
                ExcelFileName = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestData\WaterDemandSettings.xlsx"),
                OpcServerAddress = "Kepware.KEPServerEX.V6",

                StartComputeTime = new DateTime(2020, 05, 04,    0, 46, 32),
            };

            ZoneDemandDataListCreator zoneDemandDataListCreator = new ZoneDemandDataListCreator(dataContext, logger);
            List<ZoneDemandData> zoneDemandDataList = zoneDemandDataListCreator.Create();

            Helper.DumpToFile(zoneDemandDataList.FirstOrDefault(x => x.ZoneName == _testedZoneName), Path.Combine(TestContext.CurrentContext.TestDirectory, $"Dump_{DateTime.Now.ToString(dateFormat)}_ZoneDemandData.xml"));
        }
    }
}
