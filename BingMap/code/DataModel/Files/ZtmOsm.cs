using System;
using System.Xml.Serialization;
using DataModel.Files.Osm;
using DataModel.Files.Ztm;

namespace DataModel.Files
{
    [Serializable]
    public class ZtmOsm
    {
        [XmlAttribute("Distance")]
        public double Distance { get; set; }
        [XmlElement("Ztm")]
        public Stop Ztm { get; set; }
        [XmlElement("Osm")]
        public Node Osm { get; set; }

        public ZtmOsm()
        {
        }
        public ZtmOsm(Stop ztm, Node osm, double distance)
        {
            Distance = distance;
            Ztm = ztm;
            Osm = osm;
        }
        public override string ToString()
        {
            return $"{Ztm} -- {Osm} -- {Distance}";
        }
    }
}
