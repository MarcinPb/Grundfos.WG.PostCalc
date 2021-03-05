using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.DataModel
{
    public class InfraNodeFieldValue
    {
        public int NodeFieldId { get; set; }
        public int NodeId { get; set; }
        public double? FloatValue { get; set; }
        public string StringValue { get; set; }

    }
}
