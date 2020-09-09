using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grundfos.OPC;
using Grundfos.WB.ReportConnector.Configuration;
using Grundfos.WB.ReportConnector.Mapping;
using Grundfos.WB.ReportConnector.Repository;
using NLog;
using Npgsql;

namespace Grundfos.WB.ReportConnector
{
    class Program
    {
        private static readonly ILogger log = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            try
            {
                string mode = args.FirstOrDefault() ?? string.Empty;
                bool diagnosticMode = args.Any(x => x.Equals("-diag"));
                LogLevel logLevel = diagnosticMode ? LogLevel.Info : LogLevel.Trace;

                string connstring = ConfigurationManager.ConnectionStrings[Constants.GisConnectionString].ConnectionString;
                using (var conn = new NpgsqlConnection(connstring))
                {
                    log.Log(logLevel, "Opening connection: {0} ...", connstring);
                    conn.Open();
                    log.Log(logLevel, "Connection has been opened.");
                    string sql = ConfigurationManager.AppSettings[Constants.ZoneDataQuery];
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
                    switch (mode.ToLowerInvariant())
                    {
                        case "-diag":
                            RunDiagnostics(sql, da);
                            break;
                        case "-moq":
                            var zoneDataReaderMoq = BuildZoneDataReaderMoq();
                            RunOpcPublish(zoneDataReaderMoq);
                            break;
                        default:
                            var zoneDataReader = BuildZoneDataReader(da);
                            RunOpcPublish(zoneDataReader);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Exception has occurred: {0}", ex.Message);
            }
        }

        private static void RunOpcPublish(ZoneDataReader zoneDataReader)
        {
            var values = zoneDataReader.Get().Take(3).ToList();
            string opcAddress = ConfigurationManager.AppSettings[Constants.OpcAddress];
            using (var client = new OpcReader(opcAddress))
            {
                log.Log(LogLevel.Info, "OpcValue start ---------------------------");
                foreach (var opcValue in values)
                {
                    log.Log(LogLevel.Info, opcValue);
                }
                log.Log(LogLevel.Info, "OpcValue end -----------------------------");
                client.WriteValues(values);
            }
        }

        private static ZoneDataReader BuildZoneDataReader(IDataAdapter da)
        {
            var zoneMappings = ((MappingConfigurationSection)ConfigurationManager.GetSection("zoneMapping"))
                .Entries.Cast<MappingConfigurationElement>()
                .Select(x => (IMappingDefinition)x)
                .ToList();
            var zoneFieldMappings = ((MappingConfigurationSection)ConfigurationManager.GetSection("zoneFieldMapping"))
                .Entries.Cast<MappingConfigurationElement>()
                .Select(x => (IMappingDefinition)x)
                .ToList();
            string zoneNameColumn = ConfigurationManager.AppSettings[Constants.ZoneColumnName];
            var mapper = new ZoneDataMapper(zoneMappings, zoneFieldMappings, zoneNameColumn);
            var repository = new ZoneRepository(da);
            var zoneDataReader = new ZoneDataReader(repository, mapper);
            return zoneDataReader;
        }

        private static ZoneDataReader BuildZoneDataReaderMoq()
        {
            var zoneMappings = ((MappingConfigurationSection)ConfigurationManager.GetSection("zoneMapping"))
                .Entries.Cast<MappingConfigurationElement>()
                .Select(x => (IMappingDefinition)x)
                .ToList();
            var zoneFieldMappings = ((MappingConfigurationSection)ConfigurationManager.GetSection("zoneFieldMapping"))
                .Entries.Cast<MappingConfigurationElement>()
                .Select(x => (IMappingDefinition)x)
                .ToList();
            string zoneNameColumn = ConfigurationManager.AppSettings[Constants.ZoneColumnName];
            var mapper = new ZoneDataMapper(zoneMappings, zoneFieldMappings, zoneNameColumn);
            var repository = new ZoneRepositoryMoq();
            var zoneDataReader = new ZoneDataReader(repository, mapper);
            return zoneDataReader;
        }

        private static void RunDiagnostics(string sql, NpgsqlDataAdapter da)
        {
            DataSet ds = new DataSet();
            ds.Reset();
            log.Info($"Reading data from server: {sql} ...");
            da.Fill(ds);

            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            log.Info("Columns:");
            foreach (var column in dt.Columns.Cast<DataColumn>())
            {
                log.Info($" - {column.ColumnName}");
            }

            log.Info($"Number of rows: {dt.Rows.Count}");
        }
    }
}
