using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DataModel.Db
{
    [Serializable]
    public class StopConnection
    {
        [XmlAttribute("Id")]
        public int Id { get; set; }
        [XmlElement("StartStop")]
        public StopItem StartStop { get; set; }
        [XmlElement("EndStop")]
        public StopItem EndStop { get; set; }
    }
}
