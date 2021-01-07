using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace DataModel.Files
{
    [Serializable]
    [XmlRoot("Root")]
    public class Root
    {
        [XmlArray("OsmZtmList"), XmlArrayItem(typeof(ZtmOsm), ElementName = "ZtmOsm")]
        public List<ZtmOsm> OsmZtmList { get; set; }
    }
}
