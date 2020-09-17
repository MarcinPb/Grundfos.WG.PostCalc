using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Grundfos.OPC;
using Grundfos.WB.DataAccess;
using Grundfos.WB.EasyCalc.Calculations;
using Grundfos.WB.EasyCalc.Console.Configuration;
using Grundfos.Workbooks;
using NLog;

namespace Grundfos.WB.EasyCalc.Console
{
    public class Program
    {
        private static readonly ILogger log = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            try
            {
                var excelReader = new ExcelReader(@"TestData\WB-EasyCalc - sample from GM.xlsm");

                EasyCalcDataInput easyCalcDataInput = new EasyCalcDataInput
                {
                    Start_PeriodDays_M21 = excelReader.ReadCell<int>("Start", "M21"),
                    SysInput_SystemInputVolumeM3_D6 = excelReader.ReadCell<double>("Sys. Input", "D6"),
                    SysInput_SystemInputVolumeError_F6 = excelReader.ReadCell<double>("Sys. Input", "F6"),
                    BilledCons_BilledMetConsBulkWatSupExpM3_D6 = excelReader.ReadCell<double>("Billed Cons", "D6"),
                    BilledCons_BilledUnmetConsBulkWatSupExpM3_H6 = excelReader.ReadCell<double>("Billed Cons", "H6"),
                    UnbilledCons_MetConsBulkWatSupExpM3_D6 = excelReader.ReadCell<double>("Unb. Cons.", "D6"),
                    UnauthCons_IllegalConnDomEstNo_D6 = excelReader.ReadCell<int>("Unauth. Cons.", "D6"),
                    UnauthCons_IllegalConnDomPersPerHouse_H6 = excelReader.ReadCell<double>("Unauth. Cons.", "H6"),
                    UnauthCons_IllegalConnDomConsLitPerPersDay_J6 = excelReader.ReadCell<double>("Unauth. Cons.", "J6"),
                    UnauthCons_IllegalConnDomErrorMargin_F6 = excelReader.ReadCell<double>("Unauth. Cons.", "F6"),
                    UnauthCons_IllegalConnOthersErrorMargin_F10 = excelReader.ReadCell<double>("Unauth. Cons.", "F10"),
                    UnauthCons_MeterTampBypEtcEstNo_D14 = excelReader.ReadCell<double>("Unauth. Cons.", "D14"),
                    UnauthCons_MeterTampBypEtcErrorMargin_F14 = excelReader.ReadCell<double>("Unauth. Cons.", "F14"),
                    UnauthCons_MeterTampBypEtcConsLitPerCustDay_J14 = excelReader.ReadCell<double>("Unauth. Cons.", "J14"),
                    MetErrors_DetailedManualSpec_J6 = excelReader.ReadCell<int>("Meter Errors", "J6")==2,
                    MetErrors_BilledMetConsWoBulkSupMetUndrreg_H8 = excelReader.ReadCell<double>("Meter Errors", "H8"),
                    MetErrors_BilledMetConsWoBulkSupErrorMargin_N8 = excelReader.ReadCell<double>("Meter Errors", "N8"),
                    MetErrors_MetBulkSupExpMetUnderreg_H32 = excelReader.ReadCell<double>("Meter Errors", "H32"),
                    MetErrors_UnbillMetConsWoBulkSupplMetUndrreg_H34 = excelReader.ReadCell<double>("Meter Errors", "H34"),
                    MetErrors_CorruptMetReadPractMetUndrreg_H38 = excelReader.ReadCell<double>("Meter Errors", "H38"),
                    Network_DistributionAndTransmissionMains_D7 = excelReader.ReadCell<double>("Network", "D7"),
                    Network_NoOfConnOfRegCustomers_H10 = excelReader.ReadCell<double>("Network", "H10"),
                    Network_NoOfInactAccountsWSvcConns_H18 = excelReader.ReadCell<double>("Network", "H18"),
                    Network_AvgLenOfSvcConnFromBoundaryToMeterM_H32 = excelReader.ReadCell<double>("Network", "H32"),
                    Prs_ApproxNoOfConn_D7 = excelReader.ReadCell<double>("Pressure", "D7"),
                    Prs_DailyAvgPrsM_F7 = excelReader.ReadCell<double>("Pressure", "F7")
                };

                EasyCalcDataReaderMoq easyCalcDataReaderMoq = new EasyCalcDataReaderMoq();
                EasyCalcDataOutput readEasyCalcDataOutput = easyCalcDataReaderMoq.ReadEasyCalcDataOutput(easyCalcDataInput);




                string address = ConfigurationManager.AppSettings[Constants.OpcAddress];
                string tagFormat = ConfigurationManager.AppSettings[Constants.OpcTagFormat];
                var zoneConfiguration = (Configuration.ZoneConfigurationSection)ConfigurationManager.GetSection("zoneConfiguration");

                var mode = args.FirstOrDefault();
                switch (mode.ToLowerInvariant())
                {
                    case "-clone":
                        CloneSignals(address, tagFormat, zoneConfiguration);
                        break;
                    case "-diag":
                        RunDiagnostics(zoneConfiguration);
                        break;
                    default:
                        CalculateWaterBalance(address, tagFormat, zoneConfiguration);
                        break;
                }


                log.Info("Finished");
            }
            catch (Exception ex)
            {
                log.Error(ex, "Errors occurred when running the application.");
            }
        }

        private static void RunDiagnostics(ZoneConfigurationSection zoneConfiguration)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["twdb"].ConnectionString))
            {
                connection.Open();
                var configurationReader = new ZoneConfigurationReader(zoneConfiguration.Zones.Cast<ZoneConfigurationElement>().ToList());
                var reader = new DbEasyCalcSheetDataReader(connection, configurationReader);
                foreach (var zone in zoneConfiguration.Zones.Cast<ZoneConfigurationElement>())
                {
                    reader.ReadSheetData(zone.Name, new DateTime());
                }

            }
        }

        private static void CalculateWaterBalance(string address, string tagFormat, Configuration.ZoneConfigurationSection zoneConfiguration)
        {
            log.Info("Starting water balance calculations.");
            var mapper = new Mapping.EasyCalcDataMapper(tagFormat);
            log.Info("{0} zones will be processed.", zoneConfiguration.Zones.Count);
            using (var client = new OpcReader(address))
            {
                var opcDataReader = new EasyCalcOpcDataReader(client, mapper, tagFormat); // new EasyCalcRefactoredDataReader(); 
                foreach (var zone in zoneConfiguration.Zones.Cast<Configuration.ZoneConfigurationElement>())
                {
                    try
                    {
                        ProcessZone(opcDataReader, zone);
                        log.Info("Finished processing water balance for zone {0}.", zone.Name);
                    }
                    catch (System.Exception ex)
                    {
                        log.Error(ex, "Errors occurred when processing zone {0}.", zone.Name);
                    }
                }
            }
        }

        private static void CloneSignals(string address, string tagFormat, Configuration.ZoneConfigurationSection zoneConfiguration)
        {
            using (var client = new OpcReader(address))
            {
                log.Info("Running the application in clone-signals mode.");
                var zones = zoneConfiguration.Zones.Cast<Configuration.ZoneConfigurationElement>().Select(x =>
                {
                    string sourceGroupName = string.Format(tagFormat, x.Name, null);
                    return sourceGroupName.Remove(sourceGroupName.Length - 1);
                }).ToList();
                var clonerConfig = new OpcSignalCloner.Configuration
                {
                    SourceGroupName = zones[0],
                    DestinationGroupNames = zones.Skip(1).ToArray(),
                };
                var cloner = new OpcSignalCloner(client, clonerConfig);
                cloner.CloneSignals();
            }
        }

        private static void ProcessZone(IEasyCalcDataReader opcDataReader, Configuration.ZoneConfigurationElement zone)
        {
            log.Trace("Reading input data for zone {0}.", zone.Name);
            var data = opcDataReader.ReadSheetData(zone.Name, new DateTime());

            log.Trace("Calculating Water Balance for zone {0}.", zone.Name);
            EasyCalcRefactored.GetWaterLosses(data);
            EasyCalcRefactored.GetWaterLossesErrorMargin(data);
            EasyCalcRefactored.GetInfrastructureLeakageIndex(data);

            log.Trace("Publishing results to OPC for zone {0}.", zone.Name);
            opcDataReader.PublishSheetData(data, zone.Name);
        }
    }
}
