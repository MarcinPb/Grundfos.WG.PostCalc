
namespace Grundfos.WaterDemandCalculation.Model
{
    public class WaterDemandData
    {
        public int ObjectID { get; set; }
        public int ObjectTypeID { get; set; }
        public string DemandPatternName { get; set; }
        public int DemandPatternID { get; set; }
        public int ZoneID { get; set; }
        public string ZoneName { get; set; }
        public double BaseDemandValue { get; set; }
        public int AssociatedElementID { get; set; }
        public double ActualDemandValue { get; internal set; }

        public override string ToString()
        {
            return $"{nameof(ObjectID)}:{ObjectID} {nameof(DemandPatternID)}:{DemandPatternID} {nameof(DemandPatternName)}:{DemandPatternName}";
        }
    }
}
