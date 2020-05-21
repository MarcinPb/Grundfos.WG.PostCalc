using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grundfos.OPC;
using Grundfos.WaterDemandCalculation.Model;
using Grundfos.Workbooks;
using Haestad.Support.OOP.Logging;

namespace Grundfos.WaterDemandCalculation
{
    public class ZoneDemandDataListCreator
    {
        private readonly DataContext _dataContext;
        private readonly ActionLogger _logger;

        public ZoneDemandDataListCreator(DataContext dataContext, ActionLogger logger = null)
        {
            _dataContext = dataContext;
            _logger = logger;
        }

        public List<ZoneDemandData> Create()
        {
            try
            {
                _logger?.WriteMessage(OutputLevel.Info, $"Go ....{_dataContext.WgDemandPatternDict.Count}");

                var excelReader = new ExcelReader(_dataContext.ExcelFileName);
                DateTime excelSettingSimulationStartTime = new DateTime(2019, 09, 30, 12, 15, 0);
                int excelSettingSimulationIntervalMinutes = 10;

                // Fill ICollection<WaterDemandData> objectDemandData
                var objectDataExcelReader = new ObjectDataExcelReader(excelReader);
                ICollection<WaterDemandData> objectDemandData = objectDataExcelReader.ReadObjects(); // Excel.ObjectData

                var demandPatternExcelReader = new DemandPatternExcelReader(excelReader);
                // List<string> <- Excel.ExcludedItems["Excluded Object IDs"].
                // {257=PC, 2719=S5, 518=CP1, 701=CP2, 1323=CP3, 1336=CP4, 2255=S6, 2780=S7, 1247=W1, 1239=W2, 1548=CP6}    
                var excludedObjectList = demandPatternExcelReader.ReadExcludedObjects();
                this.UpdateObjectIsExcluded(objectDemandData, excludedObjectList);
                // List<string> <- Excel.ExcludedItems["Excluded Demand Patterns"]. {"nieaktywni", "Straty"}.    
                var excludedDemandPatternList = demandPatternExcelReader.ReadExcludedPatterns();
                this.UpdateDemandPatternIsExcluded(objectDemandData, excludedDemandPatternList);

                this.FillPatternIds(objectDemandData, _dataContext.WgDemandPatternDict);
                this.FillZoneIdsInWaterDemands(objectDemandData, _dataContext.WgZoneDict);

                // List<ZoneDemandData> zoneDemandDataList <-- group by <-- ICollection<WaterDemandData> objectDemandData 
                List<ZoneDemandData> zoneDemandDataList = objectDemandData
                    .GroupBy(x => x.ZoneName)
                    .Select(x => new ZoneDemandData { ZoneId = x.FirstOrDefault()==null ? 0 : x.FirstOrDefault().ZoneID, ZoneName = x.Key, Demands = x.ToList() })
                    .ToList();
                var noZoneDemands = zoneDemandDataList.Where(x => string.IsNullOrEmpty(x.ZoneName)).ToList();
                if (noZoneDemands.Count > 0)
                {
                    var numberOfItems = noZoneDemands.Sum(x => x.Demands.Count);
                    _logger?.WriteMessage(OutputLevel.Warnings, $"Found {numberOfItems} demands with no zone specified.");
                    noZoneDemands.ForEach(x => zoneDemandDataList.Remove(x));
                }

                var waterDemandExcelReader = new DemandPatternExcelReader(excelReader);

                // Fill OpcTag in zoneDemandDataList
                List<string> missingZoneOpcTags = GetMissingZoneOpcTags(waterDemandExcelReader, zoneDemandDataList);
                if (missingZoneOpcTags.Count > 0)
                {
                    string message = $"Could not find OPC tag for zones {string.Join(", ", missingZoneOpcTags)}";
                    _logger?.WriteMessage(OutputLevel.Warnings, message);
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

                // Update zoneDemandData.Demands.DemandFactorValue <- Excel.DemandPatterns.Value
                //foreach (var zoneDemandData in zoneDemandDataList)
                //{
                //    // Inside GetTotalDemand method: zoneDemandData.Demands.ActualDemandValue <- baseDemand * demandFactor.
                //    // The demandFactor is taken from DemandPattern curve.
                //    zoneDemandData.WgDemand = totalDemandCalculation.GetTotalDemand(zoneDemandData.Demands, _dataContext.StartComputeTime);
                //}
                zoneDemandDataList.ForEach(x => totalDemandCalculation.UpdateDemandFactorValue(x.Demands, _dataContext.StartComputeTime));

                // Update zoneDemandData.ScadaDemand <- OPC.10MinValue
                using (var opc = new OpcReader(_dataContext.OpcServerAddress))
                {
                    //foreach (var zoneDemandData in zoneDemandDataList)
                    //{
                    //    zoneDemandData.ScadaDemand = opc.GetDouble(zoneDemandData.OpcTag);
                    //    zoneDemandData.DemandAdjustmentRatio = zoneDemandData.WgDemand == 0 ? 0 : zoneDemandData.ScadaDemand / zoneDemandData.WgDemand;
                    //}
                    zoneDemandDataList.ForEach(x => x.ScadaDemand = opc.GetDouble(x.OpcTag));
                }

                // Calculate and update:
                //  zone.demand.ActualDemandValue
                //  zone.WgDemand
                //  zone.ExcludedDemand
                //  zone.DemandAdjustmentRatio
                zoneDemandDataList.ForEach(zone =>
                {
                    zone.Demands
                        .ForEach(demand => demand.ActualDemandValue = demand.BaseDemandValue * demand.DemandFactorValue);
                    zone.WgDemand = 
                        zone.Demands
                        .Sum(demand => demand.ActualDemandValue);
                    //zone.DemandAdjustmentRatio = 
                    //    zone.ScadaDemand / zone.WgDemand;
                    zone.ExcludedDemand =
                        zone.Demands
                        .Where(demand => demand.ObjectIsExcluded || demand.DemandPatternIsExcluded)
                        .Sum(demand => demand.ActualDemandValue);
                    zone.DemandAdjustmentRatio =
                        Math.Abs(zone.WgDemand - zone.ExcludedDemand) < 0.01 ?
                        0 :
                        (zone.ScadaDemand - zone.ExcludedDemand) / (zone.WgDemand - zone.ExcludedDemand);
                });

                return zoneDemandDataList;
            }
            catch (Exception e)
            {
                _logger?.WriteMessage(OutputLevel.Errors, $"Creating data.\n{e.Message}");
                throw;
            }
        }

        private void UpdateObjectIsExcluded(ICollection<WaterDemandData> objectDemandData, List<int> excludedObjectList)
        {
            objectDemandData.ToList().ForEach(x => x.ObjectIsExcluded = excludedObjectList.Any(y => x.ObjectID==y));
        }

        private void UpdateDemandPatternIsExcluded(ICollection<WaterDemandData> objectDemandData, List<string> excludedDemandPatternList)
        {
            objectDemandData.ToList().ForEach(x => x.DemandPatternIsExcluded = excludedDemandPatternList.Any(y => x.DemandPatternName==y));
        }

        public class DataContext
        {
            public Dictionary<string, int> WgZoneDict { get; set; }
            public Dictionary<string, int> WgDemandPatternDict { get; set; }
            public string ExcelFileName { get; set; }
            public string OpcServerAddress { get; set; }
            public DateTime StartComputeTime { get; set; }
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


        private string _conStr = @"Data Source=.\SQLEXPRESS;Initial Catalog=WG;Integrated Security=True";
        public void SaveToDatabase(List<ZoneDemandData> zoneDemandDataList)
        {
            try
            {
                //string query = "SELECT * FROM dbo.ZoneFlowComparison ORDER BY D_TIME DESC";
                using (SqlConnection sqlConn = new SqlConnection(_conStr))
                {
                    int logHeaderId;

                    using (SqlCommand cmd = new SqlCommand("SaveLogHeader", sqlConn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@DateTimeIn", SqlDbType.DateTime).Value = DateTime.Now;
                        cmd.Parameters.Add("@DateTimeUtcIn", SqlDbType.DateTime).Value = DateTime.UtcNow;

                        cmd.Parameters.Add("@LogId", SqlDbType.Int).Direction = ParameterDirection.Output;
                        cmd.Parameters["@LogId"].Value = 0;

                        sqlConn.Open();
                        cmd.ExecuteNonQuery();
                        logHeaderId = Convert.ToInt32(cmd.Parameters["@LogId"].Value);
                        sqlConn.Close();
                    }

                    foreach (var zone in zoneDemandDataList)
                    {
                        int logZonetId;

                        using (SqlCommand cmd = new SqlCommand("SaveLogZone", sqlConn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add("@LogHeaderId", SqlDbType.Int).Value = logHeaderId;
                            cmd.Parameters.Add("@AutoZoneId", SqlDbType.Int).Value = zone.ZoneId;
                            cmd.Parameters.Add("@DemandScada", SqlDbType.Float).Value = zone.ScadaDemand;
                            cmd.Parameters.Add("@DemandWg", SqlDbType.Float).Value = zone.WgDemand;
                            cmd.Parameters.Add("@DemandExcluded", SqlDbType.Float).Value = zone.ExcludedDemand;

                            cmd.Parameters.Add("@LogId", SqlDbType.Int).Direction = ParameterDirection.Output;
                            cmd.Parameters["@LogId"].Value = 0;

                            sqlConn.Open();
                            cmd.ExecuteNonQuery();
                            sqlConn.Close();
                            logZonetId = Convert.ToInt32(cmd.Parameters["@LogId"].Value);
                        }

                        var objectList = zone
                            .Demands.GroupBy(
                                o1 => new { o1.ObjectID, o1.ObjectTypeID, o1.ObjectIsExcluded },
                                o1 => o1,
                                (key, g) => new { Object1 = key, DemandList = g.ToList() }
                            )
                            .Select(o2 => new {
                                o2.Object1.ObjectID,
                                o2.Object1.ObjectTypeID,
                                o2.Object1.ObjectIsExcluded,
                                BaseDemandValue = o2.DemandList.Sum(d => d.BaseDemandValue),
                                o2.DemandList
                            });

                        foreach (var obj in objectList)
                        {
                            int logObjectId;

                            using (SqlCommand cmd = new SqlCommand("SaveLogObject", sqlConn))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.Parameters.Add("@LogZoneId", SqlDbType.Int).Value = logZonetId;
                                cmd.Parameters.Add("@AutoObjectId", SqlDbType.Int).Value = obj.ObjectID;
                                cmd.Parameters.Add("@Demand", SqlDbType.Float).Value = obj.BaseDemandValue;
                                cmd.Parameters.Add("@IsExcluded", SqlDbType.Bit).Value = obj.ObjectIsExcluded;

                                cmd.Parameters.Add("@LogId", SqlDbType.Int).Direction = ParameterDirection.Output;
                                cmd.Parameters["@LogId"].Value = 0;

                                sqlConn.Open();
                                cmd.ExecuteNonQuery();
                                sqlConn.Close();
                                logObjectId = Convert.ToInt32(cmd.Parameters["@LogId"].Value);
                            }


                            foreach (var demand in obj.DemandList)
                            {
                                using (SqlCommand cmd = new SqlCommand("SaveLogDemand", sqlConn))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;

                                    cmd.Parameters.Add("@LogObjectId", SqlDbType.Int).Value = logObjectId;
                                    cmd.Parameters.Add("@AutoDemandId", SqlDbType.Int).Value = demand.DemandPatternID;
                                    cmd.Parameters.Add("@DemandBase", SqlDbType.Float).Value = demand.BaseDemandValue;
                                    cmd.Parameters.Add("@IsExcluded", SqlDbType.Bit).Value = demand.DemandPatternIsExcluded;

                                    cmd.Parameters.Add("@LogId", SqlDbType.Int).Direction = ParameterDirection.Output;
                                    cmd.Parameters["@LogId"].Value = 0;

                                    sqlConn.Open();
                                    cmd.ExecuteNonQuery();
                                    sqlConn.Close();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                _logger?.WriteMessage(OutputLevel.Errors, $"Saving data to database.\n{e.Message}");
                throw;
            }
        }

    }
}
