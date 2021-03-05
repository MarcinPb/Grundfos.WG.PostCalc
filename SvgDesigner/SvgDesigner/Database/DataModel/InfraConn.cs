using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.DataModel
{
    public class InfraConn
    {
        public int ParentObjId { get; set; }
        public int ChildObjId { get; set; }
        public int? ConnTypeId { get; set; }

    }
}
