namespace Grundfos.WG.PostCalc.DataExchangers
{
    public class DemandSetting
    {
        public string DemandPatternName { get; set; }
        public double Factor { get; set; }
        public int ObjectTypeID { get; internal set; }
    }
}
