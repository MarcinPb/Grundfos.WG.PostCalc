﻿using System;
using System.Linq;

namespace WbEasyCalcRepository.Model
{
    public class WaterBalanceSheet
    {
        private readonly EasyCalcSheetData _data;

        public WaterBalanceSheet(EasyCalcSheetData data)
        {
            this._data = data;
            GetWaterLossesErrorMargin(_data);
        }

        /*
        public double SystemInputVolume_B19 { get; internal set; }
        public double BilledMeteredConsumption_AC4 { get; internal set; }
        public double BilledUnmeteredConsumption_AC9 { get; internal set; }
        public double UnbilledMeteredConsumption_AC14 { get; internal set; }
        public double UnbilledUnmeteredConsumption_AC19 { get; internal set; }
        public double BilledAuthorizedConsumption_T8 { get; internal set; }
        public double UnbilledAuthorizedConsumption_T16 { get; internal set; }
        public double AuthorizedConsumption_K12 { get; internal set; }
        public double WaterLosses_K29 { get; internal set; }
        public double UnauthorizedConsumption_AC24 { get; internal set; }
        public double CustomerMeterInaccuraciesAndErrorsM3_AC29 { get; internal set; }
        public double CommercialLosses_T26 { get; internal set; }
        public double PhysicalLossesM3_T34 { get; internal set; }
        */

        public double SystemInputVolume_B19 { get => _data.SystemInputSheet.SystemInputVolume_D79; }
        public double BilledMeteredConsumption_AC4 { get => _data.BilledConsumptionSheet.BilledMeteredConsumption_D6_D25.Sum();}
        public double BilledUnmeteredConsumption_AC9 { get => _data.BilledConsumptionSheet.BilledUnmeteredConsumption_H6_H25.Sum(); }
        public double UnbilledMeteredConsumption_AC14 { get => _data.UnbilledConsumptionSheet.UnbilledMeteredConsumption_D32; }
        public double UnbilledUnmeteredConsumption_AC19 { get => _data.UnbilledConsumptionSheet.UnbilledUnmeteredConsumptionM3_H6_H23.Sum(); }
        public double BilledAuthorizedConsumption_T8 { get => BilledMeteredConsumption_AC4 + BilledUnmeteredConsumption_AC9; }
        public double UnbilledAuthorizedConsumption_T16 { get => UnbilledMeteredConsumption_AC14 + UnbilledUnmeteredConsumption_AC19; }
        public double AuthorizedConsumption_K12 { get => BilledAuthorizedConsumption_T8 + UnbilledAuthorizedConsumption_T16; }
        public double WaterLosses_K29 { get => SystemInputVolume_B19 - AuthorizedConsumption_K12; }
        public double UnauthorizedConsumption_AC24 { get => _data.UnauthorizedConsumptionSheet.BestEstimateTotal_L31; }
        public double CustomerMeterInaccuraciesAndErrorsM3_AC29 { get => _data.MeterErrorsSheet.BestEstimateTotalM3_L49; }
        public double CommercialLosses_T26 { get => UnauthorizedConsumption_AC24 + CustomerMeterInaccuraciesAndErrorsM3_AC29; }
        public double PhysicalLossesM3_T34 { get => WaterLosses_K29 - CommercialLosses_T26; }


        public double AuthorizedConsumptionErrorMargin_K15 { get; internal set; }
        public double SystemInputVolumeErrorMargin_B21 { get; internal set; }
        public double WaterLossesErrorMargin_K31 { get; internal set; }
        public double UnbilledAuthorizedConsumptionErrorMargin_T20 { get; internal set; }
        public double CommercialLossesErrorMargin_T29 { get; internal set; }
        public double CustomerMeterInaccuraciesAndErrorsErrorMargin_AO30 { get; internal set; }
        public double UnbilledUnmeteredConsumptionErrorMargin_AO20 { get; internal set; }
        public double UnauthorizedConsumptionErrorMargin_AO25 { get; internal set; }
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



        private void GetWaterLossesErrorMargin(EasyCalcSheetData data)
        {
            if (WaterLosses_K29 == 0)
            {
                CommercialLossesErrorMargin_T29 = 0;
                return;
            }

            UnauthorizedConsumptionErrorMargin_AO25 = data.UnauthorizedConsumptionSheet.ErrorMargin_F24;

            SystemInputVolumeErrorMargin_B21 = data.SystemInputSheet.ErrorMargin_F72;

            AuthorizedConsumptionErrorMargin_K15 =
                AuthorizedConsumption_K12 == 0 ?
                0 : data.UnbilledConsumptionSheet.ErrorFactor_O25 * Constants.StandardDistributionFactor / AuthorizedConsumption_K12;

            double bl15 = AuthorizedConsumption_K12 * AuthorizedConsumptionErrorMargin_K15 / Constants.StandardDistributionFactor;
            double bl25 = SystemInputVolumeErrorMargin_B21 * SystemInputVolume_B19 / Constants.StandardDistributionFactor;
            double bm15 = Math.Pow(bl15, 2);
            double bm25 = Math.Pow(bl25, 2);
            double bm34 = bm15 + bm25;
            double bl34 = Math.Sqrt(bm34);
            double bj25 = UnauthorizedConsumption_AC24 * UnauthorizedConsumptionErrorMargin_AO25 / Constants.StandardDistributionFactor;
            double bk25 = Math.Pow(bj25, 2);

            CustomerMeterInaccuraciesAndErrorsErrorMargin_AO30 = data.MeterErrorsSheet.ErrorMarginTotal_N42;
            double bj30 = CustomerMeterInaccuraciesAndErrorsM3_AC29
                * CustomerMeterInaccuraciesAndErrorsErrorMargin_AO30
                / Constants.StandardDistributionFactor;
            double bk30 = Math.Pow(bj30, 2);
            double bk32 = bk25 + bk30;
            double bj32 = Math.Sqrt(bk32);
            WaterLossesErrorMargin_K31 = bl34 * Constants.StandardDistributionFactor / WaterLosses_K29;
            UnbilledAuthorizedConsumptionErrorMargin_T20 = UnbilledAuthorizedConsumption_T16 == 0 ?
                0 : data.UnbilledConsumptionSheet.ErrorFactor_O25 * Constants.StandardDistributionFactor / UnbilledAuthorizedConsumption_T16;
            CommercialLossesErrorMargin_T29 = CommercialLosses_T26 == 0d ?
                0d : bj32 * Constants.StandardDistributionFactor / CommercialLosses_T26;

            UnbilledUnmeteredConsumptionErrorMargin_AO20 = data.UnbilledConsumptionSheet.UnbilledUnmeteredConsumptionErrorMargin_J25;

            PhyscialLossesErrorMargin_AH35 = PhysicalLossesM3_T34 == 0 ?
                0d : PhyscialLossesErrorMarginFactor_BL35 * Constants.StandardDistributionFactor / PhysicalLossesM3_T34;

            RevenueWaterM3_AY8 = BilledAuthorizedConsumption_T8;
            NonRevenueWaterM3_AY24 = SystemInputVolume_B19 - RevenueWaterM3_AY8;

            NonRevenueWaterErrorMargin_AY26 = NonRevenueWaterM3_AY24 == 0 ?
                0d : NonRevenueWaterErrorMarginFactor_BL25 * Constants.StandardDistributionFactor / NonRevenueWaterM3_AY24;
        }


    }
}
