using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DataModel.Db
{
    [Serializable]
    public class StopItem
    {
        [XmlAttribute("Id")]
        public long Id { get; set; }
        [XmlAttribute("TypeId")]
        public int TypeId { get; set; }
        [XmlAttribute("Lat")]
        public double Lat { get; set; }
        [XmlAttribute("Lon")]
        public double Lon { get; set; }
        [XmlAttribute("Name")]
        public string Name { get; set; }
    }
}
