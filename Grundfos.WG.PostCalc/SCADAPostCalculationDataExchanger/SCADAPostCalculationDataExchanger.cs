﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using Grundfos.OPC;
using Grundfos.WaterDemandCalculation;
using Grundfos.WaterDemandCalculation.Model;
using Grundfos.WG.ObjectReaders;
using Grundfos.WG.OPC.Publisher;
using Grundfos.WG.OPC.Publisher.Configuration;
using Grundfos.WG.PostCalc.DataExchangers;
using Grundfos.WG.PostCalc.DemandCalculation;
using Grundfos.WG.PostCalc.Exceptions;
using Grundfos.WG.PostCalc.Persistence.MapperProfiles;
using Grundfos.WG.PostCalc.Persistence.Repositories;
using Grundfos.WG.PostCalc.PressureCalculation;
using Grundfos.WG.PostCalc.SQLiteEf;
using Grundfos.Workbooks;
using Haestad.DataIntegration;
using Haestad.Domain;
using Haestad.Support.OOP.CommandLine;
using Haestad.Support.OOP.Configuration;
using Haestad.Support.OOP.FileSystem;
using Haestad.Support.OOP.Logging;

namespace SCADAPostCalculationDataExchanger
{
    public class SCADAPostCalculationDataExchanger : ToFileDataExchangerBase, IInProcessPluginDataExchanger
    {
        public SCADAPostCalculationDataExchanger()
        {
        }

        public void SetDomainDataSet(IDomainDataSet domainDataSet)
        {
            DomainDataSet = domainDataSet;
            ScenarioID = DomainDataSet.ScenarioManager.ActiveScenarioID;
            Scenario = DomainDataSet.ScenarioManager.Element(ScenarioID) as IScenario;
        }

        public override string DataExchangerTitle
        {
            get { return "SCADAPostCalculationDataExchanger"; }
        }

        public override Version DataExchangerVersion
        {
            get { return new Version(1, 0, 0, 0); }
        }

        public override string DataExchangerCopyright
        {
            get { return "Copyright (c) Grundfos"; }
        }

        public string RepositoryPath { get; set; }
        public string DemandConfigurationWorkbook { get; private set; }

        public override bool BeforeDoDataExchange(object dataExchangeContext)
        {
            // exchangeContext already contains ResultCacheDb and DemandConfigurationWorkbook keyValues taken from *.ini file.
            var exchangeContext = (DataExchangerContext)dataExchangeContext;
            this.RepositoryPath = exchangeContext.GetString("ResultCacheDb", @"C:\WG2TW\Grundfos.WG.PostCalc\ResultCache.sqlite");
            this.DemandConfigurationWorkbook = exchangeContext.GetString("DemandConfigurationWorkbook", @"C:\WG2TW\Grundfos.WG.PostCalc\WaterDemandSettings.xlsx");

            return true;
        }

        public override bool DoDataExchange(object dataExchangeContext)
        {
            var zoneReader = new ZoneReader(this.DomainDataSet);
            var wgZones = zoneReader.GetZones();

            var excelReader = new ExcelReader(this.DemandConfigurationWorkbook);

            // dataExchangeContext <- ResultCache.sqlite
            this.PassQualityResults(dataExchangeContext);

            this.PublishOpcResults(dataExchangeContext, excelReader, wgZones);
            this.ExchangeWaterDemands(dataExchangeContext, excelReader, wgZones);
            return true;
        }

        private void PublishOpcResults(object dataExchangeContext, ExcelReader excelReader, Dictionary<int, string> wgZones)
        {
            try
            {
                var mappingReader = new OpcMappingReader(this.Logger, excelReader);
                var mappings = mappingReader.ReadMappings();
                var publishers = this.BuildPublishers(mappings, wgZones);
                this.Logger.WriteMessage(OutputLevel.Info, "Start writing results to OPC.");
                foreach (var publisher in publishers)
                {
                    publisher.PublishResults(this.DomainDataSet, this.Scenario);
                }

                this.Logger.WriteMessage(OutputLevel.Info, "Finished writing results to OPC.");
            }
            catch (Exception ex)
            {
                this.Logger.WriteMessage(OutputLevel.Errors, "Errors occured when publishing the results to OPC.");
                this.Logger.WriteException(ex, true);
            }
        }

        private List<OpcPublisher> BuildPublishers(ICollection<OpcMapping> mappings, Dictionary<int, string> wgZones)
        {
            var publishers = new List<OpcPublisher>();

            var nodeMapping = mappings.FirstOrDefault(x => x.FieldName.Equals("ZoneAveragePressure", StringComparison.OrdinalIgnoreCase));
            if (nodeMapping != null)
            {
                var nodePressureConfig = new ZonePressurePublisherConfiguration
                {
                    ResultRecordName = StandardResultRecordName.IdahoPressureNodeResults,
                    ResultAttributeRecordName = StandardResultRecordName.IdahoPressureNodeResults_NodePressure,
                    FieldName = StandardFieldName.PipeStatus,
                    ConversionFactor = 1,
                    ElementTypes = new DomainElementType[]
                    {
                        DomainElementType.BaseIdahoNodeElementManager,
                    },
                    Mappings = nodeMapping.Mappings.ToDictionary(x => x.ElementID, x => x),
                    Zones = wgZones,
                };
                var nodePublisher = new ZonePressurePublisher(nodePressureConfig, this.Logger);
                publishers.Add(nodePublisher);
            }

            var tankPercentMapping = mappings.FirstOrDefault(x => x.FieldName.Equals("TankPercentFull", StringComparison.OrdinalIgnoreCase));
            if (tankPercentMapping != null)
            {
                var tankPercentFillConfig = new PublisherConfiguration
                {
                    ResultRecordName = StandardResultRecordName.IdahoConventionalTankResults,
                    ResultAttributeRecordName = StandardResultRecordName.IdahoConventionalTankResults_CalculatedPercentFull,
                    FieldName = StandardFieldName.ElementType_TankPercentFull,
                    ConversionFactor = 1,
                    ElementTypes = new DomainElementType[]
                    {
                        DomainElementType.IdahoTankElementManager,
                    },
                    Mappings = tankPercentMapping.Mappings.ToDictionary(x => x.ElementID, x => x),
                };
                var publisher = new OpcPublisher(tankPercentFillConfig, this.Logger);
                publishers.Add(publisher);
            }

            var pipeMapping = mappings.FirstOrDefault(x => x.FieldName.Equals("PipeStatus", StringComparison.OrdinalIgnoreCase));
            if (pipeMapping != null)
            {
                var pipeOpenStateConfig = new PublisherConfiguration
                {
                    ResultRecordName = StandardResultRecordName.IdahoPipeResults,
                    ResultAttributeRecordName = StandardResultRecordName.PipeResultControlStatus,
                    FieldName = StandardFieldName.PipeStatus,
                    ConversionFactor = 1,
                    ElementTypes = new DomainElementType[]
                    {
                        DomainElementType.IdahoPipeElementManager,
                    },
                    Mappings = pipeMapping.Mappings.ToDictionary(x => x.ElementID, x => x),
                };
                var pipeOpenPublisher = new OpcPublisher(pipeOpenStateConfig, this.Logger);
                publishers.Add(pipeOpenPublisher);
            }

            return publishers;
        }

        private void PassQualityResults(object dataExchangeContext)
        {
            try
            {
                var db = new DatabaseContext(this.RepositoryPath);
                var mapper = BuildMapper();
                var repo = new PostCalcRepository(db, mapper);
                var dataExchangers = this.BuildDataExchangers(repo);
                dataExchangers.ForEach(x => x.DoDataExchange(dataExchangeContext));
            }
            catch (Exception ex)
            {
                this.Logger.WriteMessage(OutputLevel.Errors, "Errors occured when passing quality results.");
                this.Logger.WriteException(ex, true);
            }
        }

        private void ExchangeWaterDemands(object dataExchangeContext, ExcelReader excelReader, Dictionary<int, string> wgZones)
        {
            if (excelReader == null)
            {
                throw new ArgumentNullException(nameof(excelReader));
            }

            try
            {
                //var demandDataReader = new WaterDemandDataReader(this.Logger, this.Scenario, this.DomainDataSet, new WaterDemandDataReaderConfiguration());
                //var demandData = demandDataReader.GetWaterDemands();

                this.Logger.WriteMessage(OutputLevel.Info, $"{wgZones.Count} zones have been read from WaterGEMS model.");

                var objectDataReader = new ObjectDataExcelReader(excelReader);
                // List<WaterDemandData> = Excel.ObjectData
                var objectDemandData = objectDataReader.ReadObjects();
                this.Logger.WriteMessage(OutputLevel.Info, $"{objectDemandData.Count} objects with demand information have been read from Excel file.");

                var patternReader = new WaterDemandPatternReader(this.DomainDataSet);
                // Dictionary<string, int> = WaterGEMS->DemandPattern
                var patterns = patternReader.GetPatterns();
                this.Logger.WriteMessage(OutputLevel.Info, $"{patterns.Count} demand patterns have been read from WaterGEMS model.");

                // Fills objectDemandData DemandPatternID: objectDemandData[n].DemandPatternID <- patterns[where Name==Name].ID
                this.FillPatternIds(objectDemandData, patterns);
                // Fills objectDemandData ZoneID 
                this.FillZoneIdsInWaterDemands(objectDemandData, wgZones.ToDictionary(x => x.Value, x => x.Key, StringComparer.OrdinalIgnoreCase));
                //DumpWaterDemandData(objectDemandData.ToList());

                var waterDemandExcelReader = new DemandPatternExcelReader(excelReader);
                var demandTotalizer = BuildDemandTotalizer(waterDemandExcelReader, excelReader);
                var now = DateTime.Now;
                // List<ZoneDemandData>
                var zoneDemands = this.GetZoneDemands(wgZones, objectDemandData, waterDemandExcelReader, demandTotalizer, now);

                // Log only
                foreach (var item in zoneDemands)
                {
                    string message = $"Total demand for zone {item.ZoneName}: WaterGEMS = {item.WgDemand}, SCADA: {item.ScadaDemand}, ratio: {item.DemandAdjustmentRatio}.";
                    this.Logger.WriteMessage(OutputLevel.Info, message);
                }

                // Two arrays: "Excluded Object IDs" and "Excluded Demand Patterns"
                var demandConfig = this.GetDemandWriterConfig(patterns, waterDemandExcelReader);

                var demandWriter = new WaterDemandDataWriter(this.Logger, this.DomainDataSet, demandConfig);

                foreach (var zoneDemand in zoneDemands.Where(x => x.ScadaDemand > 0.001))
                {
                    demandWriter.WriteDemands(zoneDemand);
                }
            }
            catch (Exception ex)
            {
                this.Logger.WriteMessage(OutputLevel.Errors, "Errors occured when exchanging water demand.");
                this.Logger.WriteException(ex, true);
            }
        }

        private void DumpWaterDemandData(IList<WaterDemandData> demandData)
        {
            using (var file = new StreamWriter(@"C:\Temp\demand-data.txt"))
            {
                file.WriteLine("ObjectID\tObjectTypeID\tDemandPatternID\tDemandPatternName\tBaseDemandValue\tZoneID\tZoneName");
                foreach (var item in demandData)
                {
                    file.WriteLine($"{item.ObjectID}\t{item.ObjectTypeID}\t{item.DemandPatternID}\t{item.DemandPatternName}\t{item.BaseDemandValue}\t{item.ZoneID}\t{item.ZoneName}");
                }
            }
        }

        private WaterDemandDataWriterConfiguration GetDemandWriterConfig(Dictionary<string, int> patterns, DemandPatternExcelReader excelReader)
        {
            // List<string> <- Excel.ExcludedItems["Excluded Object IDs"].
            // {257=PC, 2719=S5, 518=CP1, 701=CP2, 1323=CP3, 1336=CP4, 2255=S6, 2780=S7, 1247=W1, 1239=W2, 1548=CP6}    
            var excludedObjects = excelReader.ReadExcludedObjects();

            // List<string> <- Excel.ExcludedItems["Excluded Demand Patterns"]. {"nieaktywni", "Straty"}.    
            var excludedPatterns = excelReader.ReadExcludedPatterns();

            // Only that patterns which exist in WG
            var patternsToExclude = patterns
                .Where(x => excludedPatterns.Contains(x.Key))
                .Select(x => x.Value)
                .ToArray();

            var demandConfig = new WaterDemandDataWriterConfiguration
            {
                ExcludedObjectIDs = excludedObjects.ToArray(),
                ExcludedDemandPatterns = patternsToExclude,
            };
            return demandConfig;
        }

        private List<ZoneDemandData> GetZoneDemands(
            Dictionary<int, string> wgZones, 
            ICollection<WaterDemandData> demandData, 
            DemandPatternExcelReader excelReader, 
            TotalDemandCalculation demandTotalizer, 
            DateTime now)
        {
            var zoneDemands = demandData
                .GroupBy(x => x.ZoneName)
                .Select(x => new ZoneDemandData { ZoneName = x.Key, Demands = x.ToList() })
                .ToList();
            var noZoneDemands = zoneDemands.Where(x => string.IsNullOrEmpty(x.ZoneName)).ToList();
            if (noZoneDemands.Count > 0)
            {
                var numberOfItems = noZoneDemands.Sum(x => x.Demands.Count);
                this.Logger.WriteMessage(OutputLevel.Warnings, $"Found {numberOfItems} demands with no zone specified.");
                noZoneDemands.ForEach(x => zoneDemands.Remove(x));
            }

            foreach (var item in zoneDemands)
            {
                item.WgDemand = demandTotalizer.GetTotalDemand(item.Demands, now);
            }

            List<string> missingZoneOpcTags = GetMissingZoneOpcTags(excelReader, zoneDemands);
            if (missingZoneOpcTags.Count > 0)
            {
                string message = string.Format("Could not find OPC tag for zones {0}", string.Join(", ", missingZoneOpcTags));
                this.Logger.WriteMessage(OutputLevel.Warnings, message);
            }

            using (var opc = new OpcReader("Kepware.KEPServerEX.V6"))
            {
                foreach (var item in zoneDemands)
                {
                    item.ScadaDemand = opc.GetDouble(item.OpcTag);
                    item.DemandAdjustmentRatio = item.WgDemand == 0 ? 0 : item.ScadaDemand / item.WgDemand;
                }
            }

            return zoneDemands;
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

        private static TotalDemandCalculation BuildDemandTotalizer(DemandPatternExcelReader waterDemandExcelReader, ExcelReader excelReader)
        {
            var adjuster = new TimeshiftAdjuster(7 * 24 * 60);
            var interpolator = new Interpolator(adjuster);
            var patternService = new DemandPatternService(waterDemandExcelReader);
            var demandService = new DemandService(interpolator, patternService);
            var settings = new SimulationTimeResolverConfiguration
            {
                SimulationStartTime = excelReader.ReadSetting<DateTime>(Grundfos.WG.PostCalc.Constants.ApplicationSetting_SimulationStartDate),
                SimulationIntervalMinutes = excelReader.ReadSetting<int>(Grundfos.WG.PostCalc.Constants.ApplicationSetting_SimulationIntervalMinutes)
            };
            var demandTotalizer = new TotalDemandCalculation(demandService, new SimulationTimeResolver(settings));
            return demandTotalizer;
        }

        private void FillPatternIds(ICollection<WaterDemandData> demands, Dictionary<string, int> patterns)
        {
            foreach (var item in demands)
            {
                if (!patterns.TryGetValue(item.DemandPatternName, out int patternId))
                {
                    throw new DataExchangerException(string.Format("Could not find pattern definition for pattern ID: {0}.", item.DemandPatternName));
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
                    throw new DataExchangerException(string.Format("Could not find zone definition for zone name: {0}.", item.ZoneName));
                }

                item.ZoneID = zoneId;
            }
        }

        private List<Grundfos.WG.PostCalc.DataExchangers.DataExchangerBase> BuildDataExchangers(IPostCalcRepository repository)
        {
            var ageConfig = new DataExchangerConfiguration
            {
                ResultRecordName = StandardResultRecordName.IdahoWaterQualityResults,
                ResultAttributeRecordName = StandardResultRecordName.IdahoWaterQualityResults_CalculatedAge,
                Alternative = AlternativeType.AgeAlternative,
                FieldName = StandardFieldName.Age_InitialAge,
                ConversionFactor = 3600,
            };

            var traceConfig = new DataExchangerConfiguration
            {
                ResultRecordName = StandardResultRecordName.IdahoWaterQualityResults,
                ResultAttributeRecordName = StandardResultRecordName.IdahoWaterQualityResults_CalculatedTrace,
                Alternative = AlternativeType.TraceAlternative,
                FieldName = StandardFieldName.Trace_InitialTrace,
                ConversionFactor = 0.01,
            };

            var concentrationConfig = new DataExchangerConfiguration
            {
                ResultRecordName = StandardResultRecordName.IdahoWaterQualityResults,
                ResultAttributeRecordName = StandardResultRecordName.IdahoWaterQualityResults_CalculatedConcentration,
                Alternative = AlternativeType.ConstituentAlternative,
                FieldName = StandardFieldName.Constituent_InitialConcentration,
                ConversionFactor = 1,
            };

            var dataExchangers = new List<Grundfos.WG.PostCalc.DataExchangers.DataExchangerBase>
            {
                new GenericDataExchanger(this.Logger, this.Scenario, this.DomainDataSet, repository, ageConfig),
                new GenericDataExchanger(this.Logger, this.Scenario, this.DomainDataSet, repository, traceConfig),
                new GenericDataExchanger(this.Logger, this.Scenario, this.DomainDataSet, repository, concentrationConfig),
            };

            return dataExchangers;
        }

        public override object NewDataExchangeContext(string[] arguments)
        {
            const string SETTINGS_FILE_KEY = "INI";
            CommandLineArgumentsHelper commandLineArgumentsHelper = new CommandLineArgumentsHelper(arguments, new string[] { SETTINGS_FILE_KEY });
            string settingsFileName = commandLineArgumentsHelper.GetCommandLineValue(SETTINGS_FILE_KEY);
            IConfigurationReader configurationReader = new SettingsFileReader(Logger, new FilePath(settingsFileName));

            return new DataExchangerContext(Logger, configurationReader);
        }

        private IScenario Scenario { get; set; }

        private IDomainDataSet DomainDataSet { get; set; }

        private int ScenarioID { get; set; }

        private static IMapper BuildMapper()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(typeof(ResultProfile).Assembly);
            });
            var mapper = new Mapper(mapperConfig);
            return mapper;
        }
    }
}
