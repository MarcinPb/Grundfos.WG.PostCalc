using Grundfos.WaterDemandCalculation.Model;

namespace Grundfos.WaterDemandCalculation
{
    public interface IDemandPatternService
    {
        WaterDemandPattern GetWaterDemandPattern(string patternName);
    }
}