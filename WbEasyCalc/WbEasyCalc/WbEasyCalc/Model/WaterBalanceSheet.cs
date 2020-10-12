using System;

namespace WbEasyCalc.Model
{
    public class WaterBalanceSheet
    {
        public double SystemInputVolume_B19 { get; internal set; }
        public double BilledMeteredConsumption_AC4 { get; internal set; }
        public double BilledUnmeteredConsumption_AC9 { get; internal set; }
        public double UnbilledMeteredConsumption_AC14 { get; internal set; }
        public double UnbilledUnmeteredConsumption_AC19 { get; internal set; }
        public double BilledAuthorizedConsumption_T8 { get; internal set; }
        public double UnbilledAuthorizedConsumption_T16 { get; internal set; }
        public double AuthorizedConsumption_K12 { get; internal set; }
        public double WaterLosses_K29 { get; internal set; }
        public double AuthorizedConsumptionErrorMargin_K15 { get; internal set; }
        public double SystemInputVolumeErrorMargin_B21 { get; internal set; }
        public double WaterLossesErrorMargin_K31 { get; internal set; }
        public double UnbilledAuthorizedConsumptionErrorMargin_T20 { get; internal set; }
        public double UnauthorizedConsumption_AC24 { get; internal set; }
        public double CustomerMeterInaccuraciesAndErrorsM3_AC29 { get; internal set; }
        public double CommercialLossesErrorMargin_T29 { get; internal set; }
        public double CustomerMeterInaccuraciesAndErrorsErrorMargin_AO30 { get; internal set; }
        public double UnbilledUnmeteredConsumptionErrorMargin_AO20 { get; internal set; }
        public double UnauthorizedConsumptionErrorMargin_AO25 { get; internal set; }
        public double CommercialLosses_T26 { get; internal set; }
        public double PhysicalLossesM3_T34 { get; internal set; }
        public double PhyscialLossesErrorMarginFactor_BL35 { get => this.GetPhyscialLossesErrorMarginFactor_BL35(); }
        public double PhyscialLossesErrorMargin_AH35 { get; internal set; }
        public double RevenueWaterM3_AY8 { get; internal set; }
        public double NonRevenueWaterM3_AY24 { get; internal set; }
        public double NonRevenueWaterErrorMarginFactor_BL25 { get => this.GetNonRevenueWaterErrorMarginFactor_BL25(); }
        public double NonRevenueWaterErrorMargin_AY26 { get; internal set; }

        private double GetNonRevenueWaterErrorMarginFactor_BL25()
        {
            double bl25 = this.SystemInputVolumeErrorMargin_B21 * this.SystemInputVolume_B19 / Constants.StandardDistributionFactor;
            return bl25;
        }

        private double GetPhyscialLossesErrorMarginFactor_BL35()
        {
            double bm15 = Math.Pow(this.AuthorizedConsumption_K12 * this.AuthorizedConsumptionErrorMargin_K15 / Constants.StandardDistributionFactor, 2);
            double bm25 = Math.Pow(this.SystemInputVolumeErrorMargin_B21 * this.SystemInputVolume_B19 / Constants.StandardDistributionFactor, 2);
            double bm30 = Math.Pow(this.CommercialLossesErrorMargin_T29 * this.CommercialLosses_T26 / Constants.StandardDistributionFactor, 2);
            double bm35 = bm15 + bm25 + bm30;
            double bl35 = Math.Sqrt(bm35);
            return bl35;
        }
    }
}
