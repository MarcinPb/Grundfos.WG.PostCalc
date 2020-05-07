using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Grundfos.WaterDemandCalculation.Model;
using Grundfos.WaterDemandCalculation.Tests.TestData;
using Grundfos.Workbooks;
using NUnit.Framework;

namespace Grundfos.WaterDemandCalculation.Tests
{
    [TestFixture]
    public class TotalDemandCalculationTests
    {
        [TestCase("2019-09-10 12:24:30", "2019-09-10 12:20:00")]
        public void GetTotalDemand_Tests(DateTime input, DateTime output)
        {
            var excelDirectory = TestContext.CurrentContext.TestDirectory;
            var excelFileName = @"TestData\WaterDemandSettings.xlsx";
            var excelFilePath = Path.Combine(excelDirectory, excelFileName);
            var excelReader = new ExcelReader(excelFilePath);

            // Fill ICollection<WaterDemandData> objectDemandData
            var objectDataExcelReader = new ObjectDataExcelReader(excelReader);
            ICollection<WaterDemandData> objectDemandData = objectDataExcelReader.ReadObjects(); // Excel.ObjectData
            this.FillPatternIds(objectDemandData, WgObjects.DemandPatternDict);
            this.FillZoneIdsInWaterDemands(objectDemandData, WgObjects.ZoneDict.ToDictionary(x => x.Value, x => x.Key, StringComparer.OrdinalIgnoreCase));

            // List<ZoneDemandData> zoneDemandDataList <-- group by <-- ICollection<WaterDemandData> objectDemandData 
            //List<ZoneDemandData> zoneDemandDataList




            var waterDemandExcelReader = new DemandPatternExcelReader(excelReader);

            var adjuster = new TimeshiftAdjuster(7 * 24 * 60);
            var interpolator = new Interpolator(adjuster);
            var patternService = new DemandPatternService(waterDemandExcelReader);
            var demandService = new DemandService(interpolator, patternService);
            var settings = new SimulationTimeResolverConfiguration
            {
                SimulationStartTime = new DateTime(2019, 09, 30,  12, 15, 0),
                SimulationIntervalMinutes = 10,
            };
            var totalDemandCalculation = new TotalDemandCalculation(demandService, new SimulationTimeResolver(settings));

            double totalDemand = totalDemandCalculation.GetTotalDemand(objectDemandData.ToList(), new DateTime(2020, 05, 04,  0, 46, 32));

            //var resolver = new SimulationTimeResolver(config);
            //var actual = resolver.GetSimulationTimestamp(input);
            //Assert.AreEqual(output, actual);
        }

        private void FillPatternIds(ICollection<WaterDemandData> demands, Dictionary<string, int> patterns)
        {
            foreach (var item in demands)
            {
                if (!patterns.TryGetValue(item.DemandPatternName, out int patternId))
                {
                    throw new Exception($"Could not find pattern definition for pattern ID: {item.DemandPatternName}.");
                }

                item.DemandPatternID = patternId;
            }
        }

        private void FillZoneIdsInWaterDemands(ICollection<WaterDemandData> demands, Dictionary<string, int> zones)
        {
            foreach (var item in demands.Where(x => !string.IsNullOrWhiteSpace(x.ZoneName)))
            {
                if (!zones.TryGetValue(item.ZoneName, out int zoneId))
                {
                    throw new Exception($"Could not find zone definition for zone name: {item.ZoneName}.");
                }

                item.ZoneID = zoneId;
            }
        }





    }
}
