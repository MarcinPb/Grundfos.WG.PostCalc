using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Grundfos.TW.SQL.Model;
using Grundfos.WG2SVG.Configuration;
using NLog;

namespace Grundfos.TW.SQL
{
    public class SignalValueService2
    {
        private static readonly ILogger log = LogManager.GetCurrentClassLogger();
        private readonly SqlConnection connection;
        private readonly TwTableDiscoveryConfiguration tableDiscoveryConfiguration;
        private readonly TwSignalDiscoveryConfiguration signalDiscoveryConfiguration;
        private readonly List<int> floatDataTypes;
        private readonly List<int> integerDataTypes;

        public SignalValueService2(SqlConnection connection, TwTableDiscoveryConfiguration tableDiscoveryConfiguration, TwSignalDiscoveryConfiguration signalDiscoveryConfiguration)
        {
            this.connection = connection;
            this.tableDiscoveryConfiguration = tableDiscoveryConfiguration;
            this.signalDiscoveryConfiguration = signalDiscoveryConfiguration;
            this.floatDataTypes = this.signalDiscoveryConfiguration.FloatDataTypes.Cast<TwDataType>().Select(x => x.Value).ToList();
            this.integerDataTypes = this.signalDiscoveryConfiguration.IntegerDataTypes.Cast<TwDataType>().Select(x => x.Value).ToList();
        }

        public List<SignalValue> GetSignalValues()
        {
            string tableName = this.GetTableName();
            var result = this.GetAllSignalValues(tableName);
            var signals = this.GetSignalValues(result);
            log.Trace("{0} raw signal values retrieved.", signals.Count);
            return signals;
        }

        public string GetTableName()
        {
            try
            {
                var dt = new DataTable();
                var getTableCommand = new SqlCommand(this.tableDiscoveryConfiguration.GetTableSql, connection);
                var da = new SqlDataAdapter(getTableCommand);
                da.Fill(dt);
                var tableName = dt.Rows[0][this.tableDiscoveryConfiguration.TableNameColumn].ToString();
                return tableName;
            }
            catch (Exception ex)
            {
                log.Error(ex, ex.Message);
                throw;
            }
        }

        public DataTable GetAllSignalValues(string tableName)
        {
            var now = DateTime.UtcNow;
#if DEBUG
            now = new DateTime(2019, 07, 14, 07, 00, 00);
#endif
            var interval = TimeSpan.FromSeconds(this.signalDiscoveryConfiguration.IntervalSeconds);
            var time = now - interval;

            var dt = new DataTable();
            string commandSql = string.Format(this.signalDiscoveryConfiguration.SelectCommand, tableName);
            var getDataCommand = new SqlCommand(commandSql, connection);
            getDataCommand.Parameters.AddWithValue("@time", time);
            var da = new SqlDataAdapter(getDataCommand);
            log.Trace("Getting signal values from DB with the following command: {0}", commandSql);
            da.Fill(dt);
            return dt;
        }

        public List<SignalValue> GetSignalValues(DataTable dataTable)
        {
            var latestRows = dataTable.Rows.Cast<DataRow>()
                .GroupBy(x => x[this.signalDiscoveryConfiguration.IdColumn].ToString())
                .Select(g => g.OrderByDescending(r => (DateTime)r[this.signalDiscoveryConfiguration.TimeColumn]).First())
                .ToList();
            var signals = latestRows.Select(x => this.Convert(x)).ToList();
            return signals;
        }

        private SignalValue Convert(DataRow dataRow)
        {
            var signalValue = new SignalValue
            {
                ID = (System.Convert.ToInt64(dataRow[this.signalDiscoveryConfiguration.IdColumn])),
                Timestamp = (DateTime)dataRow[this.signalDiscoveryConfiguration.TimeColumn],
                Value = this.ResolveValue(dataRow),
            };

            return signalValue;
        }

        public double ResolveValue(DataRow dataRow)
        {
            int dataType = (short)dataRow[this.signalDiscoveryConfiguration.DataTypeColumn];
            if (this.floatDataTypes.Contains(dataType))
            { 
                var floatValue = dataRow[this.signalDiscoveryConfiguration.FloatValueColumn];
                if (floatValue != DBNull.Value)
                {
                    return (double)floatValue;
                }

                return double.NaN;
            }

            if (this.integerDataTypes.Contains(dataType))
            {
                var integerValue = dataRow[this.signalDiscoveryConfiguration.IntegerValueColumn];
                if (integerValue != DBNull.Value)
                {
                    return System.Convert.ToDouble(integerValue);
                }

                return double.NaN;
            }

            return double.NaN;
        }
    }
}
