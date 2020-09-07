using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Grundfos.WB.EasyCalc.Calculations;
using Grundfos.WB.EasyCalc.Calculations.Model;

namespace Grundfos.WB.DataAccess
{
    public class DbEasyCalcSheetDataReader : IEasyCalcDataReader
    {
        private const string Query = @"
select isnull(sum(t.Value), 0)
from
(
	select p.Value
	from config.CalculatedAverageConfig c
		inner join CalculatedAverage p on c.ID = p.ID
	where c.DestinationVariableName = @destName
		and p.Timestamp >= @timeFrom
		and p.Timestamp < @timeTo
		and Value is not null
) t 
";

// 'SI_SREDN_60M'
        private readonly SqlConnection connection;
        private readonly IConfigurationReader configurationReader;

        public DbEasyCalcSheetDataReader(SqlConnection connection, IConfigurationReader configurationReader)
        {
            this.connection = connection;
            this.configurationReader = configurationReader;
        }

        public void PublishSheetData(EasyCalcSheetData data, string zoneName)
        {
            throw new NotImplementedException();
        }

        public EasyCalcSheetData ReadSheetData(string zone, DateTime yearMonth)
        {
            var configuration = this.configurationReader.GetConfiguration(zone);
            var cmd = new SqlCommand(Query, this.connection);

            var start = new DateTime(yearMonth.Year, yearMonth.Month, 1);
            var end = start.AddMonths(1);
            cmd.Parameters.AddWithValue("destName", configuration.ZoneBalanceVariableName);
            cmd.Parameters.AddWithValue("timeFrom", start);
            cmd.Parameters.AddWithValue("timeTo", end);

            var dt = new DataTable();
            using (var adapter = new SqlDataAdapter(cmd))
            {
                adapter.Fill(dt);
            }

            throw new NotImplementedException();
        }

        public async Task<double> Get(string name, DateTime yearMonth)
        {
            var cmd = new SqlCommand(Query, this.connection);

            var start = new DateTime(yearMonth.Year, yearMonth.Month, 1);
            var end = start.AddMonths(1);
            cmd.Parameters.AddWithValue("destName", configuration.ZoneBalanceVariableName);
            cmd.Parameters.AddWithValue("timeFrom", start);
            cmd.Parameters.AddWithValue("timeTo", end);

            var dt = new DataTable();
            using (var adapter = new SqlDataAdapter(cmd))
            {
                adapter.Fill(dt);
            }
        }
    }
}
