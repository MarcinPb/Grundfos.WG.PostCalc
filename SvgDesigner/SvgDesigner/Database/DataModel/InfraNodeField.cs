using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.DataModel
{
    public class InfraNodeField
    {
        public int NodeId { get; set; }
        public int NodeTypeId { get; set; }
        public string FieldName { get; set; }
        public object FieldValue { get; set; }

    }
}
