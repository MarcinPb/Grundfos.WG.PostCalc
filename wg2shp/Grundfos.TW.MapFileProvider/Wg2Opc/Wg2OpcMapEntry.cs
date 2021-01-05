using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grundfos.TW.DataSourceMap.Wg2Opc
{
    public class Wg2OpcMapEntry
    {
        public int ElementID { get; set; }
        public string ElementLabel { get; set; }
        public int ResultAttributeID { get; set; }
        public string OpcTag { get; set; }
    }
}
