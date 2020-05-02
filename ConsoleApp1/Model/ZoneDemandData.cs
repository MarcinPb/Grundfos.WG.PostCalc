using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ConsoleApp1.Model
{
    [Serializable]
    public class ZoneDemandData
    {
        public ZoneDemandData()
        {
            this.Demands = new List<WaterDemandData>();
        }
        [XmlAttribute]
        public string ZoneName { get; set; }
        [XmlAttribute]
        public string OpcTag { get; set; }
        [XmlAttribute]
        public double WgDemand { get; set; }
        [XmlAttribute]
        public double ScadaDemand { get; set; }
        [XmlAttribute]
        public double DemandAdjustmentRatio { get; set; }
        public List<WaterDemandData> Demands { get; set; }
        public override string ToString()
        {
            return $"{this.ZoneName} {nameof(WgDemand)}:{WgDemand}, {nameof(ScadaDemand)}:{ScadaDemand}, {nameof(DemandAdjustmentRatio)}:{DemandAdjustmentRatio}";
        }
    }
}
