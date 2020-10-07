using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grundfos.WB.EasyCalc.Calculations
{
    public class EasyCalcDataOutput
    {
        public double SystemInputVolume_B19{ get; set; }
        public double SystemInputVolumeErrorMargin_B21{ get; set; } 

        public double AuthorizedConsumption_K12{ get; set; } 
        public double AuthorizedConsumptionErrorMargin_K15{ get; set; }
        public double WaterLosses_K29{ get; set; } 
        public double WaterLossesErrorMargin_K31{ get; set; }

        public double BilledAuthorizedConsumption_T8{ get; set; } 
        public double UnbilledAuthorizedConsumption_T16{ get; set; } 
        public double UnbilledAuthorizedConsumptionErrorMargin_T20{ get; set; }
        public double CommercialLosses_T26{ get; set; } 
        public double CommercialLossesErrorMargin_T29{ get; set; } 
        public double PhysicalLossesM3_T34{ get; set; } 
        public double PhyscialLossesErrorMargin_AH35{ get; set; } 

        public double BilledMeteredConsumption_AC4{ get; set; }
        public double BilledUnmeteredConsumption_AC9{ get; set; }
        public double UnbilledMeteredConsumption_AC14{ get; set; }

        public double UnbilledUnmeteredConsumption_AC19{ get; set; }
        public double UnbilledUnmeteredConsumptionErrorMargin_AO20{ get; set; }
        public double UnauthorizedConsumption_AC24{ get; set; }
        public double UnauthorizedConsumptionErrorMargin_AO25{ get; set; }
        public double CustomerMeterInaccuraciesAndErrorsM3_AC29{ get; set; }
        public double CustomerMeterInaccuraciesAndErrorsErrorMargin_AO30{ get; set; }

        public double RevenueWaterM3_AY8{ get; set; }
        public double NonRevenueWaterM3_AY24{ get; set; }
        public double NonRevenueWaterErrorMargin_AY26{ get; set; } // 6593339 

    }
}
