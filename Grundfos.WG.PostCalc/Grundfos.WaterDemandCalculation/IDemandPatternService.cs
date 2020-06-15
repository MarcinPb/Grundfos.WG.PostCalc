using Grundfos.WG.Model;

namespace Grundfos.WaterDemandCalculation
{
    public interface IDemandPatternService
    {
        WaterDemandPattern GetWaterDemandPattern(string patternName);
    }
}