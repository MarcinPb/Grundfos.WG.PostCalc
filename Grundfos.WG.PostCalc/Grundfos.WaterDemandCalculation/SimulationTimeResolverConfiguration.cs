using System;

namespace Grundfos.WaterDemandCalculation
{
    public class SimulationTimeResolverConfiguration
    {
        public DateTime SimulationStartTime { get; set; }
        public int SimulationIntervalMinutes { get; set; }
    }
}
