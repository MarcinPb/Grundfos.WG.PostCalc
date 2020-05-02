
using System;
using System.Xml.Serialization;

namespace ConsoleApp1.Model
{
    [Serializable]
    public class WaterDemandData
    {
        [XmlAttribute]
        public int ObjectID { get; set; }
        [XmlAttribute]
        public int ObjectTypeID { get; set; }
        [XmlAttribute]
        public string DemandPatternName { get; set; }
        [XmlAttribute]
        public int DemandPatternID { get; set; }
        [XmlAttribute]
        public int ZoneID { get; set; }
        [XmlAttribute]
        public string ZoneName { get; set; }
        [XmlAttribute]
        public double BaseDemandValue { get; set; }
        [XmlAttribute]
        public int AssociatedElementID { get; set; }
        [XmlAttribute]
        public double ActualDemandValue { get; set; }

        public override string ToString()
        {
            return $"{nameof(ObjectID)}:{ObjectID} {nameof(DemandPatternID)}:{DemandPatternID} {nameof(DemandPatternName)}:{DemandPatternName}";
        }
    }
}
