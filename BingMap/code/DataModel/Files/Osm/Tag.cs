using System;
using System.Xml.Serialization;

namespace DataModel.Files.Osm
{
    [Serializable]
    public class Tag
    {
        [XmlAttribute("k")]
        public string Key { get; set; }
        [XmlAttribute("v")]
        public string Value { get; set; }
        public override string ToString()
        {
            return $"{Key} - {Value}";
        }
    }
}
