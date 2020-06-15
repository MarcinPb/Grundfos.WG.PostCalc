using System.Collections.Generic;
using Grundfos.WG.Model;

namespace Grundfos.WaterDemandCalculation
{
    public class DemandPatternService : IDemandPatternService
    {
        public DemandPatternService(DemandPatternExcelReader excelReader)
        {
            this.DemandPatterns = excelReader.ReadDemands();
        }

        //public Dictionary<string, WaterDemandPattern> DemandPatterns { get; private set; }
        private Dictionary<string, WaterDemandPattern> DemandPatterns;

        public WaterDemandPattern GetWaterDemandPattern(string patternName)
        {
            return this.DemandPatterns[patternName];
        }
    }
}
