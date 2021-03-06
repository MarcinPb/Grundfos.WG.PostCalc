﻿using System;
using System.Collections.Generic;
using System.Linq;
using Grundfos.WaterDemandCalculation.ExtensionMethods;
using Grundfos.WG.Model;

namespace Grundfos.WaterDemandCalculation
{
    public class TotalDemandCalculation
    {
        public TotalDemandCalculation(DemandService demandService, SimulationTimeResolver simulationTimeResolver)
        {
            this.DemandService = demandService;
            this.SimulationTimeResolver = simulationTimeResolver;
        }

        public DemandService DemandService { get; }
        public SimulationTimeResolver SimulationTimeResolver { get; }

        public double GetTotalDemand(IList<WaterDemandData> demands, DateTime time)
        {
            var simulationTimestamp = this.SimulationTimeResolver.GetSimulationTimestamp(time);
            var minutesFromMonday = simulationTimestamp.MinutesFromMonday();
            double totalDemand = 0;
            foreach (var item in demands)
            {
                double baseDemand = item.BaseDemandValue;
                double demandFactor = this.DemandService.GetDemandAt(item.DemandPatternName, minutesFromMonday);
                item.ActualDemandValue = baseDemand * demandFactor;
                totalDemand += item.ActualDemandValue;
            }

            return totalDemand;            
        }
        public void UpdateDemandFactorValue(IList<WaterDemandData> demands, DateTime time)
        {
            var simulationTimestamp = this.SimulationTimeResolver.GetSimulationTimestamp(time);
            var minutesFromMonday = simulationTimestamp.MinutesFromMonday();
            foreach (var item in demands)
            {
                double demandFactor = this.DemandService.GetDemandAt(item.DemandPatternName, minutesFromMonday);
                item.DemandFactorValue = demandFactor;
            }
        }
    }
}
