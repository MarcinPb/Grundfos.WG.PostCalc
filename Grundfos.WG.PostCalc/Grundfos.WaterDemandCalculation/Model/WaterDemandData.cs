
using System;

namespace Grundfos.WaterDemandCalculation.Model
{
    [Serializable]
    public class WaterDemandData
    {
        public int ZoneID { get; set; }
        public string ZoneName { get; set; }
        public int ObjectID { get; set; }
        public int ObjectTypeID { get; set; }
        public int DemandPatternID { get; set; }
        public string DemandPatternName { get; set; }
        public double BaseDemandValue { get; set; }
        public double DemandFactorValue { get; set; }
        public double ActualDemandValue { get; set; }
        public int AssociatedElementID { get; set; }

        public bool ObjectIsExcluded { get; set; }
        public bool DemandPatternIsExcluded { get; set; }

        public override string ToString()
        {
            return $"{nameof(ObjectID)}:{ObjectID} {nameof(DemandPatternID)}:{DemandPatternID} {nameof(DemandPatternName)}:{DemandPatternName}";
        }
    }
}
