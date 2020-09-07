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
                log.Warn("No data was found in the table.");
                return new List<DataRow>();
            }

            return dt.Rows.Cast<DataRow>().ToList();
        }
    }
}
