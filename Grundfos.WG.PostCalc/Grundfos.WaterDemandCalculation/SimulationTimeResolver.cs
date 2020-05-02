using System;

namespace Grundfos.WaterDemandCalculation
{
    public class SimulationTimeResolver
    {
        private readonly SimulationTimeResolverConfiguration configuration;

        public SimulationTimeResolver(SimulationTimeResolverConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public DateTime GetSimulationTimestamp(DateTime dateTime)
        {
            var start = SimulationStartTimeResolver.GetRoundedSimulationStartTime(this.configuration.SimulationStartTime, this.configuration.SimulationIntervalMinutes);
            var timespan = dateTime - start;
            int intervals = (int)timespan.TotalMinutes / this.configuration.SimulationIntervalMinutes;
            int minutes = intervals * this.configuration.SimulationIntervalMinutes;
            var timestamp = start.AddMinutes(minutes);
            return timestamp;
        }
    }
}
