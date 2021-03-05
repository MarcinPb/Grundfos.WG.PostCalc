using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.DataModel
{
    public class InfraNode
    {
        public int NodeId { get; set; }
        public int NodeTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public double Xx { get; set; }
        public double Yy { get; set; }

    }
}
