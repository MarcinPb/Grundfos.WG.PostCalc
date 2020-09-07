using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Grundfos.OPC;
using Grundfos.WB.DataAccess;
using Grundfos.WB.EasyCalc.Calculations;
using Grundfos.WB.EasyCalc.Console.Configuration;
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
                    reader.ReadSheetData(zone.Name)
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
            var data = opcDataReader.ReadSheetData(zone.Name);

            log.Trace("Calculating Water Balance for zone {0}.", zone.Name);
            EasyCalcRefactored.GetWaterLosses(data);
            EasyCalcRefactored.GetWaterLossesErrorMargin(data);
            EasyCalcRefactored.GetInfrastructureLeakageIndex(data);

            log.Trace("Publishing results to OPC for zone {0}.", zone.Name);
            opcDataReader.PublishSheetData(data, zone.Name);
        }
    }
}
