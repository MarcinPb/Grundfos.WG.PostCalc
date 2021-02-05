using System;
using System.Linq;
using WbEasyCalc;
using WbEasyCalcRepository.Model;

namespace WbEasyCalcRepository
{
    public static class EasyCalcRefactored
    {   /*
        public static void GetWaterLosses(EasyCalcSheetData data)
        {
            
            //double unauthorizedConsumption_IllegalConnectionsDomesticTotalM3_L6 =
            //    data.UnauthorizedConsumptionSheet.IllegalConnectionsDomesticEstimatedNumber_D6
            //    * data.UnauthorizedConsumptionSheet.IllegalConnectionsDomesticPersonsPerHouse_H6
            //    * data.UnauthorizedConsumptionSheet.IllegalConnectionsDomesticConsumptionLitersPerPersonPerDay_J6
            //    * data.StartSheet.PeriodDays_M21
            //    / 1000;
            //double unauthorizedConsumption_IllegalConnectionsOthersTotalM3_L10 =
            //    data.UnauthorizedConsumptionSheet.IllegalConnectionsOthersEstimatedNumber_D10
            //    * data.UnauthorizedConsumptionSheet.IllegalConnectionsOthersConsumptionLitersPerConnectionPerDay_J10
            //    * data.StartSheet.PeriodDays_M21
            //    / 1000;
            //double unauthorizedConsumption_MeterTamperingBypassesEtcTotalM3_L14 =
            //    data.UnauthorizedConsumptionSheet.MeterTamperingBypassesEtcEstimatedNumber_D14
            //    * data.UnauthorizedConsumptionSheet.MeterTamperingBypassesEtcConsumptionLitersPerCustomerPerDay_J14
            //    * data.StartSheet.PeriodDays_M21
            //    / 1000;
            //double unauthorizedConsumption_OthersM3PerDay_L18L22 =
            //    data.UnauthorizedConsumptionSheet.OthersM3PerDay_J18_J22.Sum()
            //    * data.StartSheet.PeriodDays_M21;

            //double unauthorizedConsumption_bestEstimate_L31 =
            //    unauthorizedConsumption_IllegalConnectionsDomesticTotalM3_L6
            //    + unauthorizedConsumption_IllegalConnectionsOthersTotalM3_L10
            //    + unauthorizedConsumption_MeterTamperingBypassesEtcTotalM3_L14
            //    + unauthorizedConsumption_OthersM3PerDay_L18L22;

            //double billedConsumption_bulkWaterSupplyExport_D6 = data.BilledConsumptionSheet.BilledMeteredConsumptionBulkWaterSupplyExportM3_D6;
            //double unbilledConsumption_bulkWaterSupplyExport_D6 = data.UnbilledConsumptionSheet.MeteredConsumptionBulkWaterSupplyExportM3_D6;

            //double meterErrors_MeteredBulkSupplyExport_F32 = billedConsumption_bulkWaterSupplyExport_D6 + unbilledConsumption_bulkWaterSupplyExport_D6;
            //double meterErrors_MeteredBulkSupplyExportMetereUnderregistration_H32 = data.MeterErrorsSheet.MeteredBulkSupplyExportMetereUnderregistration_H32;

            //double meterErrors_UnbilledMeteredConsumptionWithoutBulkSupply_F34 = 
            //    data.UnbilledConsumptionSheet.UnbilledMeteredConsumptionWithoutBulkSupply_D8_D23.Sum();
            //double meterErrors_UnbilledMeteredConsumptionWithoutBulkSupplyMeterUnderregistration_H34 =
            //    data.MeterErrorsSheet.UnbilledMeteredConsumptionWithoutBulkSupplyMeterUnderregistration_H34;

            //double meterErrors_CorruptMeterReadingPracticesMeterUnderregistration_H38 =
            //    data.MeterErrorsSheet.CorruptMeterReadingPracticesMeterUnderregistration_H38;
            //double billedConsumption_BilledMeteredConsumption_D28 = data.BilledConsumptionSheet.BilledMeteredConsumption_D6_D25.Sum();
            //double billedConsumption_BilledUnmeteredConsumption_H28 =
            //    data.BilledConsumptionSheet.BilledUnmeteredConsumption_H6_H25.Sum();
            //double meterErrors_CorruptMeterReadingPractices_F38 = billedConsumption_BilledMeteredConsumption_D28 + billedConsumption_BilledUnmeteredConsumption_H28;

            //double meterErrors_BilledMeteredConsumptionWithoutBulkSupplyMeterUnderregistration_H8 =
            //    data.MeterErrorsSheet.BilledMeteredConsumptionWithoutBulkSupplyMeterUnderregistration_H8;
            //double meterErrors_BilledMeteredConsumptionWithoutBulkSupply_F8 = data.BilledConsumptionSheet.BilledMeteredConsumptionWithoutBulkSupply_D8_D25.Sum();
            //double meterErrors_BilledMeteredConsumptionWithoutBulkSupply_L8 =
            //    (meterErrors_BilledMeteredConsumptionWithoutBulkSupply_F8 / (1 - meterErrors_BilledMeteredConsumptionWithoutBulkSupplyMeterUnderregistration_H8))
            //    - meterErrors_BilledMeteredConsumptionWithoutBulkSupply_F8;
            

            //data.WaterBalanceSheet.SystemInputVolume_B19 = data.SystemInputSheet.SystemInputVolume_D79;
            //data.WaterBalanceSheet.BilledMeteredConsumption_AC4 = data.BilledConsumptionSheet.BilledMeteredConsumption_D6_D25.Sum();
            //data.WaterBalanceSheet.BilledUnmeteredConsumption_AC9 = data.BilledConsumptionSheet.BilledUnmeteredConsumption_H6_H25.Sum();
            //data.WaterBalanceSheet.UnbilledMeteredConsumption_AC14 = data.UnbilledConsumptionSheet.UnbilledMeteredConsumption_D32;
            //data.WaterBalanceSheet.UnbilledUnmeteredConsumption_AC19 = data.UnbilledConsumptionSheet.UnbilledUnmeteredConsumptionM3_H6_H23.Sum();
            //data.WaterBalanceSheet.BilledAuthorizedConsumption_T8 = data.WaterBalanceSheet.BilledMeteredConsumption_AC4 + data.WaterBalanceSheet.BilledUnmeteredConsumption_AC9;
            //data.WaterBalanceSheet.UnbilledAuthorizedConsumption_T16 = data.WaterBalanceSheet.UnbilledMeteredConsumption_AC14 + data.WaterBalanceSheet.UnbilledUnmeteredConsumption_AC19;
            //data.WaterBalanceSheet.AuthorizedConsumption_K12 = data.WaterBalanceSheet.BilledAuthorizedConsumption_T8 + data.WaterBalanceSheet.UnbilledAuthorizedConsumption_T16;
            //data.WaterBalanceSheet.WaterLosses_K29 = data.WaterBalanceSheet.SystemInputVolume_B19 - data.WaterBalanceSheet.AuthorizedConsumption_K12;
            //data.WaterBalanceSheet.UnauthorizedConsumption_AC24 = data.UnauthorizedConsumptionSheet.BestEstimateTotal_L31;
            //data.WaterBalanceSheet.CustomerMeterInaccuraciesAndErrorsM3_AC29 = data.MeterErrorsSheet.BestEstimateTotalM3_L49;
            //data.WaterBalanceSheet.CommercialLosses_T26 = data.WaterBalanceSheet.UnauthorizedConsumption_AC24 + data.WaterBalanceSheet.CustomerMeterInaccuraciesAndErrorsM3_AC29;
            //data.WaterBalanceSheet.PhysicalLossesM3_T34 = data.WaterBalanceSheet.WaterLosses_K29 - data.WaterBalanceSheet.CommercialLosses_T26;

            //var startM21 = data.StartSheet.PeriodDays_M21;
            //data.WaterBalanceDaySheet.SystemInputVolume_B19 = startM21 > 0 ? data.SystemInputSheet.SystemInputVolume_D79 / startM21 : 0;
            //data.WaterBalanceDaySheet.BilledMeteredConsumption_AC4 = startM21 > 0 ? data.BilledConsumptionSheet.BilledMeteredConsumption_D6_D25.Sum() / startM21 : 0;
            //data.WaterBalanceDaySheet.BilledUnmeteredConsumption_AC9 = startM21 > 0 ? data.BilledConsumptionSheet.BilledUnmeteredConsumption_H6_H25.Sum() / startM21 : 0;
            //data.WaterBalanceDaySheet.UnbilledMeteredConsumption_AC14 = startM21 > 0 ? data.UnbilledConsumptionSheet.UnbilledMeteredConsumption_D32 / startM21 : 0;
            //data.WaterBalanceDaySheet.UnbilledUnmeteredConsumption_AC19 = startM21 > 0 ? data.UnbilledConsumptionSheet.UnbilledUnmeteredConsumptionM3_H6_H23.Sum() / startM21 : 0;
            //data.WaterBalanceDaySheet.BilledAuthorizedConsumption_T8 = data.WaterBalanceSheet.BilledMeteredConsumption_AC4 + data.WaterBalanceSheet.BilledUnmeteredConsumption_AC9;
            //data.WaterBalanceDaySheet.UnbilledAuthorizedConsumption_T16 = data.WaterBalanceSheet.UnbilledMeteredConsumption_AC14 + data.WaterBalanceSheet.UnbilledUnmeteredConsumption_AC19;
            //data.WaterBalanceDaySheet.AuthorizedConsumption_K12 = data.WaterBalanceSheet.BilledAuthorizedConsumption_T8 + data.WaterBalanceSheet.UnbilledAuthorizedConsumption_T16;
            //data.WaterBalanceDaySheet.WaterLosses_K29 = data.WaterBalanceSheet.SystemInputVolume_B19 - data.WaterBalanceSheet.AuthorizedConsumption_K12;
            //data.WaterBalanceDaySheet.UnauthorizedConsumption_AC24 = startM21 > 0 ? data.UnauthorizedConsumptionSheet.BestEstimateTotal_L31 / startM21 : 0;
            //data.WaterBalanceDaySheet.CustomerMeterInaccuraciesAndErrorsM3_AC29 = startM21 > 0 ? data.MeterErrorsSheet.BestEstimateTotalM3_L49 / startM21 : 0;
            //data.WaterBalanceDaySheet.CommercialLosses_T26 = data.WaterBalanceSheet.UnauthorizedConsumption_AC24 + data.WaterBalanceSheet.CustomerMeterInaccuraciesAndErrorsM3_AC29;
            //data.WaterBalanceDaySheet.PhysicalLossesM3_T34 = data.WaterBalanceSheet.WaterLosses_K29 - data.WaterBalanceSheet.CommercialLosses_T26;
        }

        public static void GetWaterLossesErrorMargin(EasyCalcSheetData data)
        {
            if (data.WaterBalanceSheet.WaterLosses_K29 == 0)
            {
                data.WaterBalanceSheet.CommercialLossesErrorMargin_T29 = 0;
                return;
            }

            data.WaterBalanceSheet.UnauthorizedConsumptionErrorMargin_AO25 = data.UnauthorizedConsumptionSheet.ErrorMargin_F24;

            data.WaterBalanceSheet.SystemInputVolumeErrorMargin_B21 = data.SystemInputSheet.ErrorMargin_F72;

            data.WaterBalanceSheet.AuthorizedConsumptionErrorMargin_K15 =
                data.WaterBalanceSheet.AuthorizedConsumption_K12 == 0 ?
                0 : data.UnbilledConsumptionSheet.ErrorFactor_O25 * Constants.StandardDistributionFactor / data.WaterBalanceSheet.AuthorizedConsumption_K12;

            double bl15 = data.WaterBalanceSheet.AuthorizedConsumption_K12 * data.WaterBalanceSheet.AuthorizedConsumptionErrorMargin_K15 / Constants.StandardDistributionFactor;
            double bl25 = data.WaterBalanceSheet.SystemInputVolumeErrorMargin_B21 * data.WaterBalanceSheet.SystemInputVolume_B19 / Constants.StandardDistributionFactor;
            double bm15 = Math.Pow(bl15, 2);
            double bm25 = Math.Pow(bl25, 2);
            double bm34 = bm15 + bm25;
            double bl34 = Math.Sqrt(bm34);
            double bj25 = data.WaterBalanceSheet.UnauthorizedConsumption_AC24 * data.WaterBalanceSheet.UnauthorizedConsumptionErrorMargin_AO25 / Constants.StandardDistributionFactor;
            double bk25 = Math.Pow(bj25, 2);

            data.WaterBalanceSheet.CustomerMeterInaccuraciesAndErrorsErrorMargin_AO30 = data.MeterErrorsSheet.ErrorMarginTotal_N42;
            double bj30 = data.WaterBalanceSheet.CustomerMeterInaccuraciesAndErrorsM3_AC29
                * data.WaterBalanceSheet.CustomerMeterInaccuraciesAndErrorsErrorMargin_AO30
                / Constants.StandardDistributionFactor;
            double bk30 = Math.Pow(bj30, 2);
            double bk32 = bk25 + bk30;
            double bj32 = Math.Sqrt(bk32);
            data.WaterBalanceSheet.WaterLossesErrorMargin_K31 = bl34 * Constants.StandardDistributionFactor / data.WaterBalanceSheet.WaterLosses_K29;
            data.WaterBalanceSheet.UnbilledAuthorizedConsumptionErrorMargin_T20 = data.WaterBalanceSheet.UnbilledAuthorizedConsumption_T16 == 0 ?
                0 : data.UnbilledConsumptionSheet.ErrorFactor_O25 * Constants.StandardDistributionFactor / data.WaterBalanceSheet.UnbilledAuthorizedConsumption_T16;
            data.WaterBalanceSheet.CommercialLossesErrorMargin_T29 = data.WaterBalanceSheet.CommercialLosses_T26 == 0d ?
                0d : bj32 * Constants.StandardDistributionFactor / data.WaterBalanceSheet.CommercialLosses_T26;

            data.WaterBalanceSheet.UnbilledUnmeteredConsumptionErrorMargin_AO20 = data.UnbilledConsumptionSheet.UnbilledUnmeteredConsumptionErrorMargin_J25;

            data.WaterBalanceSheet.PhyscialLossesErrorMargin_AH35 = data.WaterBalanceSheet.PhysicalLossesM3_T34 == 0 ?
                0d : data.WaterBalanceSheet.PhyscialLossesErrorMarginFactor_BL35 * Constants.StandardDistributionFactor / data.WaterBalanceSheet.PhysicalLossesM3_T34;

            data.WaterBalanceSheet.RevenueWaterM3_AY8 = data.WaterBalanceSheet.BilledAuthorizedConsumption_T8;
            data.WaterBalanceSheet.NonRevenueWaterM3_AY24 = data.WaterBalanceSheet.SystemInputVolume_B19 - data.WaterBalanceSheet.RevenueWaterM3_AY8;

            data.WaterBalanceSheet.NonRevenueWaterErrorMargin_AY26 = data.WaterBalanceSheet.NonRevenueWaterM3_AY24 == 0 ?
                0d : data.WaterBalanceSheet.NonRevenueWaterErrorMarginFactor_BL25 * Constants.StandardDistributionFactor / data.WaterBalanceSheet.NonRevenueWaterM3_AY24;
        }
        */

        //public static void GetInfrastructureLeakageIndex(EasyCalcSheetData data)
        //{
        //    var pi = data.PiSheet;
        //    double maplBestEstimate_F19 = (
        //        (data.NetworkSheet.DistributionAndTransmissionMainsBestEstimate_D37 * 18)
        //        + (data.NetworkSheet.ServiceConnectionsBestEstimate_H30 * 0.8)
        //        + (data.NetworkSheet.LenOfServConnFromBoundToMeterKm_H39 * 25)
        //    ) * pi.AveragePressureMBestEstimate_F11 / 24 * pi.AverageSupplyTimeHPerDayBestEstimate_F9 / 1000;
        //    pi.CaplBestEstimate_F17 = data.WaterBalanceSheet.PhysicalLossesM3_T34 / data.StartSheet.PeriodDays_M21;
        //    pi.MaplBestEstimate_F19 = Math.Max(maplBestEstimate_F19, 0);
        //    pi.IliBestEstimate_F25 = pi.MaplBestEstimate_F19 == 0d ? 0d : pi.CaplBestEstimate_F17 / pi.MaplBestEstimate_F19;
        //}
    }
}
