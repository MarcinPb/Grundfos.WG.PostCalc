using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace DataModel.Files.Osm
{
    [Serializable]
    public class Node
    {
        [XmlAttribute("id")]
        public long Id { get; set; }
        [XmlAttribute("lat")]
        public double Lat { get; set; }
        [XmlAttribute("lon")]
        public double Lon { get; set; }

        [XmlArray("TagList"), XmlArrayItem(typeof(Tag), ElementName = "Tag")]
        public List<Tag> TagList { get; set; }

        public override string ToString()
        {
            return $"{Id} - {Lat} - {Lon}";
        }
    }
}
