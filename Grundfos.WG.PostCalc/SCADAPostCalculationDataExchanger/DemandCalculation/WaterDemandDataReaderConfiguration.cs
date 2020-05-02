using System.Collections.Generic;

namespace Grundfos.WG.PostCalc.DemandCalculation
{
    public class WaterDemandDataReaderConfiguration
    {
        public WaterDemandDataReaderConfiguration()
        {
            this.ExcludedObjectIDs = new List<int>();
        }

        public List<int> ExcludedObjectIDs { get; set; }
    }
}
