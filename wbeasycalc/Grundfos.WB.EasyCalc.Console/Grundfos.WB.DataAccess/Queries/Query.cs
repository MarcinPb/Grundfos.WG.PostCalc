using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grundfos.WB.DataAccess.Queries
{
    public class Query
    {
        public const string Sql = @"
select top 1 *
from TELWIN_MAP m
	inner join {0} a on m.D_ID = a.D_VAR_ID
where m.D_NAME = @name
	and a.T_TYPE = 4
	and a.D_TIME < @time
order by a.D_TIME desc";
        private readonly TableNameBuilder tableNameBuilder;

        public Query(TableNameBuilder tableNameBuilder)
        {
            this.tableNameBuilder = tableNameBuilder;
        }

        Task<object> Get(string name, DateTime yearMonth)
        {
            string tableName = this.tableNameBuilder.GetTableName(yearMonth);
            string sql = string.Format(Sql, tableName);

            cmd.Parameters.AddWithValue("destName", configuration.ZoneBalanceVariableName);
            cmd.Parameters.AddWithValue("timeFrom", start);
            cmd.Parameters.AddWithValue("timeTo", end);

            var dt = new DataTable();
            using (var adapter = new SqlDataAdapter(cmd))
            {
                adapter.Fill(dt);
            }

        }

        private 
    }
}
