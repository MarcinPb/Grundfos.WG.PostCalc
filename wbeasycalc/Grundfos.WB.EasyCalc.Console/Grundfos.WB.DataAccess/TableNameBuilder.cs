using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grundfos.WB.DataAccess
{
    public class TableNameBuilder
    {
        public string GetTableName(DateTime date)
        {
            return string.Format("AR_0000_{0}", date.Year);
        }
    }
}
