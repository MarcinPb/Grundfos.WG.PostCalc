using System;
using WbEasyCalc;

namespace DataModel
{
    public class WaterConsumption : ICloneable
    {
        public int WaterConsumptionId { get; set; }
        public int YearNo { get; set; }
        public int MonthNo { get; set; }
        public int ZoneId { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool IsArchive { get; set; }
        public bool IsAccepted { get; set; }

        public object Clone()
        {
            return new WaterConsumption()
            {
                WaterConsumptionId = WaterConsumptionId,
                ZoneId = ZoneId,
                YearNo = YearNo,
                MonthNo = MonthNo,
                Description = Description,
                IsArchive = IsArchive,
                IsAccepted = IsAccepted,
            };
        }
    }
}
