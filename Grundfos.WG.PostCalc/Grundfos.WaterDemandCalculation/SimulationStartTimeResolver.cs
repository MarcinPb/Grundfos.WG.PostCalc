using System;

namespace Grundfos.WaterDemandCalculation
{
    public static class SimulationStartTimeResolver
    {
        public static DateTime GetRoundedSimulationStartTime(DateTime start, int interval)
        {
            var minutes = start.Minute - (start.Minute % interval);
            return new DateTime(start.Year, start.Month, start.Day, start.Hour, minutes, 0);
        }
    }
}
