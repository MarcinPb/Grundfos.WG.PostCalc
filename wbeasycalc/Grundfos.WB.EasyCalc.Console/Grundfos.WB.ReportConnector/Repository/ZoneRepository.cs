using System.Collections.Generic;
using System.Data;
using System.Linq;
using NLog;

namespace Grundfos.WB.ReportConnector.Repository
{
    public class ZoneRepository : IRepository<DataRow>
    {
        private static readonly ILogger log = LogManager.GetCurrentClassLogger();

        private readonly IDataAdapter dataAdapter;

        public ZoneRepository(IDataAdapter dataAdapter)
        {
            this.dataAdapter = dataAdapter;
        }

        public ICollection<DataRow> GetAll()
        {
            var ds = new DataSet();
            var data = this.dataAdapter.Fill(ds);
            var dt = ds.Tables.Count > 0 ? ds.Tables[0] : null;
            if (dt == null)
            {
                //log.Warn("No data was found in the table.");
                log.Log(LogLevel.Info, "No data was found in the table.");
                return new List<DataRow>();
            }
            log.Log(LogLevel.Info, $"Data was found: {dt.Rows.Count} rows.");
            var res = dt.Rows.Cast<DataRow>().ToList();

            foreach (var row in res.Take(10))
            {
                log.Log(LogLevel.Info, $"{row[0]} - {row[1]} - {row[2]} - {row[3]} - {row[4]} - {row[5]} - {row[6]}");
            }

            return dt.Rows.Cast<DataRow>().ToList();
        }
    }
}
