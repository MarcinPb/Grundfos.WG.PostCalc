using System.Collections.Generic;
using Grundfos.WG.Model;

namespace Grundfos.WaterDemandCalculation.Tests.TestData
{
    public static class WaterDemandPatterns
    {
        public static WaterDemandPattern GetLinearPattern100()
        {
            var pattern = new WaterDemandPattern
            {
                Name = "Test",
                Profile = new List<WaterDemandPatternEntry>
                {
                    new WaterDemandPatternEntry { TimeshiftMinutes = 0, Value = 0 },
                    new WaterDemandPatternEntry { TimeshiftMinutes = 1, Value = 1 },
                    new WaterDemandPatternEntry { TimeshiftMinutes = 100, Value = 100 },
                }
            };

            return pattern;
        }

        public static WaterDemandPattern GetLinearPattern7Days()
        {
            var pattern = new WaterDemandPattern
            {
                Name = "Test",
                Profile = new List<WaterDemandPatternEntry>
                {
                    new WaterDemandPatternEntry { TimeshiftMinutes = 0, Value = 0 },
                    new WaterDemandPatternEntry { TimeshiftMinutes = 1, Value = 1 },
                    new WaterDemandPatternEntry { TimeshiftMinutes = 100, Value = 100 },
                    new WaterDemandPatternEntry { TimeshiftMinutes = 7 * 24 * 60, Value = 7 * 24 * 60 },
                }
            };

            return pattern;
        }


        public static WaterDemandPattern GetTriangularPattern()
        {
            var pattern = new WaterDemandPattern
            {
                Name = "Test",
                Profile = new List<WaterDemandPatternEntry>
                {
                    new WaterDemandPatternEntry { TimeshiftMinutes = 0, Value = 0 },
                    new WaterDemandPatternEntry { TimeshiftMinutes = 50, Value = 50 },
                    new WaterDemandPatternEntry { TimeshiftMinutes = 100, Value = 100 },
                    new WaterDemandPatternEntry { TimeshiftMinutes = 150, Value = 50 },
                    new WaterDemandPatternEntry { TimeshiftMinutes = 200, Value = 0 },
                }
            };

            return pattern;
        }
    }
}
