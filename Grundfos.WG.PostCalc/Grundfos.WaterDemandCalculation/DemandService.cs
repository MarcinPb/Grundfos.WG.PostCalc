namespace Grundfos.WaterDemandCalculation
{
    public class DemandService
    {
        public DemandService(Interpolator interpolator, IDemandPatternService demandPatternService)
        {
            this.Interpolator = interpolator;
            this.DemandPatternService = demandPatternService;
        }

        public Interpolator Interpolator { get; }
        public IDemandPatternService DemandPatternService { get; }

        public double GetDemandAt(string patternName, double timeshiftMinutes)
        {
            var pattern = this.DemandPatternService.GetWaterDemandPattern(patternName);
            var result = this.Interpolator.GetValueAt(timeshiftMinutes, pattern);
            return result;
        }
    }
}
