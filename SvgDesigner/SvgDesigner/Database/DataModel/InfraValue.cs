using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.DataModel
{
    public class InfraValue
    {
        public int FieldId { get; set; }
        public int ObjId { get; set; }
        public double? FloatValue { get; set; }
        public string StringValue { get; set; }

    }
}
