using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Grundfos.OPC;
using Grundfos.WG.Model;
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
            DateTime startComputeTime = new DateTime(2020, 05, 04,   0, 46, 32);

            DateTime excelSettingSimulationStartTime = new DateTime(2019, 09, 30,   12, 15, 0);
            int excelSettingSimulationIntervalMinutes = 10;

            // From WG:
            Dictionary<string, int> wgDemandPatternDict = WgObjects.DemandPatternDict;
            Dictionary<string, int> wgZoneDict = 
                WgObjects.ZoneDict.ToDictionary(x => x.Value, x => x.Key, StringComparer.OrdinalIgnoreCase);

            // From excel
            var excelDirectory = TestContext.CurrentContext.TestDirectory;
            var excelFileName = @"TestData\WaterDemandSettings.xlsx";
            var excelFilePath = Path.Combine(excelDirectory, excelFileName);
            var excelReader = new ExcelReader(excelFilePath);

            // Fill ICollection<WaterDemandData> objectDemandData
            var objectDataExcelReader = new ObjectDataExcelReader(excelReader);
            ICollection<WaterDemandData> objectDemandData = objectDataExcelReader.ReadObjects(); // Excel.ObjectData
            this.FillPatternIds(objectDemandData, wgDemandPatternDict);
            this.FillZoneIdsInWaterDemands(objectDemandData, wgZoneDict);

            // List<ZoneDemandData> zoneDemandDataList <-- group by <-- ICollection<WaterDemandData> objectDemandData 
            List<ZoneDemandData> zoneDemandDataList = objectDemandData
                .GroupBy(x => x.ZoneName)
                .Select(x => new ZoneDemandData { ZoneName = x.Key, Demands = x.ToList() })
                .ToList();
            var noZoneDemands = zoneDemandDataList.Where(x => string.IsNullOrEmpty(x.ZoneName)).ToList();
            if (noZoneDemands.Count > 0)
            {
                var numberOfItems = noZoneDemands.Sum(x => x.Demands.Count);
                //this.Logger.WriteMessage(OutputLevel.Warnings, $"Found {numberOfItems} demands with no zone specified.");
                noZoneDemands.ForEach(x => zoneDemandDataList.Remove(x));
            }

            var waterDemandExcelReader = new DemandPatternExcelReader(excelReader);

            // Fill OpcTag in zoneDemandDataList
            List<string> missingZoneOpcTags = GetMissingZoneOpcTags(waterDemandExcelReader, zoneDemandDataList);
            if (missingZoneOpcTags.Count > 0)
            {
                string message = $"Could not find OPC tag for zones {string.Join(", ", missingZoneOpcTags)}";
                //this.Logger.WriteMessage(OutputLevel.Warnings, message);
            }


            // Demand patterns and interpolation
            var adjuster = new TimeshiftAdjuster(7 * 24 * 60);
            var interpolator = new Interpolator(adjuster);
            var patternService = new DemandPatternService(waterDemandExcelReader);
            var demandService = new DemandService(interpolator, patternService);
            var settings = new SimulationTimeResolverConfiguration
            {
                SimulationStartTime = excelSettingSimulationStartTime,              // 2019-09-30 12:15
                SimulationIntervalMinutes = excelSettingSimulationIntervalMinutes,  // 10
            };
            var totalDemandCalculation = new TotalDemandCalculation(demandService, new SimulationTimeResolver(settings));


            //double totalDemand = totalDemandCalculation.GetTotalDemand(objectDemandData.ToList(), startComputeTime);
            foreach (var zoneDemandData in zoneDemandDataList)
            {
                // Inside GetTotalDemand method: zoneDemandData.Demands.ActualDemandValue <- baseDemand * demandFactor.
                // The demandFactor is taken from DemandPattern curve.
                zoneDemandData.WgDemand = totalDemandCalculation.GetTotalDemand(zoneDemandData.Demands, startComputeTime);
            }

            using (var opc = new OpcReader("Kepware.KEPServerEX.V6"))
            {
                foreach (var zoneDemandData in zoneDemandDataList)
                {
                    zoneDemandData.ScadaDemand = opc.GetDouble(zoneDemandData.OpcTag);
                    zoneDemandData.DemandAdjustmentRatio = zoneDemandData.WgDemand == 0 ? 0 : zoneDemandData.ScadaDemand / zoneDemandData.WgDemand;
                }
            }

            //return zoneDemandDataList;
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

        private static List<string> GetMissingZoneOpcTags(DemandPatternExcelReader excelReader, List<ZoneDemandData> zoneDemands)
        {
            var zoneDefinitions = excelReader.ReadZones();
            var missingZones = new List<string>();
            foreach (var item in zoneDemands)
            {
                var zoneDefinition = zoneDefinitions.FirstOrDefault(x => x.ZoneName.Equals(item.ZoneName, StringComparison.OrdinalIgnoreCase));
                if (zoneDefinition == null)
                {
                    missingZones.Add(item.ZoneName);
                    continue;
                }

                item.OpcTag = zoneDefinition.OpcTag;
            }

            return missingZones;
        }
    }
}
