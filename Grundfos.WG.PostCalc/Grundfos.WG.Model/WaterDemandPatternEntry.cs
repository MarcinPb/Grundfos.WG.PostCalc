namespace Grundfos.WG.Model
{
    public class WaterDemandPatternEntry
    {
        public double TimeshiftMinutes { get; set; }
        public double Value { get; set; }

        public override string ToString()
        {
            return $"{this.TimeshiftMinutes}:{this.Value}";
        }
    }
}
