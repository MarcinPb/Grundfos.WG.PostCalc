﻿using System.Collections.Generic;
using Grundfos.WaterDemandCalculation.Model;

namespace Grundfos.WaterDemandCalculation
{
    public class DemandPatternService : IDemandPatternService
    {
        public DemandPatternService(DemandPatternExcelReader excelReader)
        {
            this.DemandPatterns = excelReader.ReadDemands();
        }

        public Dictionary<string, WaterDemandPattern> DemandPatterns { get; private set; }

        public WaterDemandPattern GetWaterDemandPattern(string patternName)
        {
            return this.DemandPatterns[patternName];
        }
    }
}