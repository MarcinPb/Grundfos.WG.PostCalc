using System;
using System.Collections.Generic;

namespace Grundfos.WaterDemandCalculation.Model
{
    [Serializable]
    public class ZoneDemandData
    {
        public ZoneDemandData()
        {
            this.Demands = new List<WaterDemandData>();
        }
        public int ZoneId { get; set; }
        public string ZoneName { get; set; }
        public double WgDemand { get; set; }
        //public string OpcTag { get; internal set; }
        public string OpcTag { get; set; }
        public double ScadaDemand { get; set; }
        public double DemandAdjustmentRatio { get; set; }
        public double ExcludedDemand { get; set; }
        public List<WaterDemandData> Demands { get; set; }
        public override string ToString()
        {
            return $"{this.ZoneName} {nameof(WgDemand)}:{WgDemand}, {nameof(ScadaDemand)}:{ScadaDemand}, {nameof(DemandAdjustmentRatio)}:{DemandAdjustmentRatio}";
        }
    }
}
