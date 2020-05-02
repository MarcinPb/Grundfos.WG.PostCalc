using System.Collections.Generic;
using Grundfos.WaterDemandCalculation.Model;

namespace Grundfos.WG.PostCalc.DemandCalculation
{
    public class ZoneDemandData
    {
        public ZoneDemandData()
        {
            this.Demands = new List<WaterDemandData>();
        }
        public string ZoneName { get; set; }
        public string OpcTag { get; internal set; }
        public List<WaterDemandData> Demands { get; set; }
        public double WgDemand { get; set; }
        public double ScadaDemand { get; set; }
        public double DemandAdjustmentRatio { get; set; }
        public override string ToString()
        {
            return $"{this.ZoneName} {nameof(WgDemand)}:{WgDemand}, {nameof(ScadaDemand)}:{ScadaDemand}, {nameof(DemandAdjustmentRatio)}:{DemandAdjustmentRatio}";
        }
    }
}
