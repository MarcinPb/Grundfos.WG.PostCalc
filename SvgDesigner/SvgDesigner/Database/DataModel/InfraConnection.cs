using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.DataModel
{
    public class InfraConnection
    {
        public int ParentNodeId { get; set; }
        public int ChildNodeId { get; set; }
        public int? TypeId { get; set; }

    }
}
