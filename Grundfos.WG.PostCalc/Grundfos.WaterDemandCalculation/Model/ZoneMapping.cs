namespace Grundfos.WaterDemandCalculation.Model
{
    public class ZoneMapping
    {
        public string ZoneName { get; set; }
        public string OpcTag { get; set; }

        public override string ToString()
        {
            return $"{ZoneName}: {OpcTag}";
        }
    }
}
