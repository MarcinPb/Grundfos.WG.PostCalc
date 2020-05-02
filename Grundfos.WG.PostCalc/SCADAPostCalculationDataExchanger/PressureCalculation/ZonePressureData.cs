namespace Grundfos.WG.PostCalc.PressureCalculation
{
    public class ZonePressureData
    {
        public int ZoneID { get; set; }
        public double Sum { get; set; }
        public int Count { get; set; }
        public double AveragePressure { get; set; }
        public override string ToString()
        {
            return $"{ZoneID}: {AveragePressure}";
        }
    }
}
