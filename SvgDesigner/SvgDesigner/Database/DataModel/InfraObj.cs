using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.DataModel
{
    public class InfraObj
    {
        public int ObjId { get; set; }
        public int ObjTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public double Xx { get; set; }
        public double Yy { get; set; }

    }
}
