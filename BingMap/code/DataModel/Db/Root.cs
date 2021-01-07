using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using DataModel.Files;

namespace DataModel.Db
{
    [Serializable]
    [XmlRoot("Root")]
    public class Root
    {
        [XmlArray("StopConnectionList"), XmlArrayItem(typeof(StopConnection), ElementName = "StopConnection")]
        public List<StopConnection> StopConnectionList { get; set; }
    }
}
