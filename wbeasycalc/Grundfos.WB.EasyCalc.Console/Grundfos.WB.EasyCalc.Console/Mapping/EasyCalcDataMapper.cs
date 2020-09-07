using System;
using System.Collections.Generic;
using System.Linq;
using Grundfos.OPC.Model;
using Grundfos.WB.EasyCalc.Calculations.Model;
using static Grundfos.WB.EasyCalc.Console.Configuration.OpcTagNames;

namespace Grundfos.WB.EasyCalc.Console.Mapping
{
    public class EasyCalcDataMapper
    {
        private readonly string tagFormat;

        public EasyCalcDataMapper(string tagFormat)
        {
            this.tagFormat = tagFormat;
        }

        public string Zone { get; set; }

        public ICollection<OpcValue> Map(EasyCalcSheetData model)
        {
            var wb = model.WaterBalanceSheet;
            var pi = model.PiSheet;
            var result = new List<OpcValue>
            {
                new OpcValue { Value = wb.AuthorizedConsumptionErrorMargin_K15, Tag = this.GetTagFormat(WatBal_AuthorizedConsumptionErrorMargin_K15) },
                new OpcValue { Value = wb.AuthorizedConsumption_K12, Tag = this.GetTagFormat(WatBal_AuthorizedConsumption_K12) },
                new OpcValue { Value = wb.BilledAuthorizedConsumption_T8, Tag = this.GetTagFormat(WatBal_BilledAuthorizedConsumption_T8) },
                new OpcValue { Value = wb.BilledMeteredConsumption_AC4, Tag = this.GetTagFormat(WatBal_BilledMeteredConsumption_AC4) },
                new OpcValue { Value = wb.BilledUnmeteredConsumption_AC9, Tag = this.GetTagFormat(WatBal_BilledUnmeteredConsumption_AC9) },
                new OpcValue { Value = wb.CommercialLossesErrorMargin_T29, Tag = this.GetTagFormat(WatBal_CommercialLossesErrorMargin_T29) },
                new OpcValue { Value = wb.CommercialLosses_T26, Tag = this.GetTagFormat(WatBal_CommercialLosses_T26) },
                new OpcValue { Value = wb.CustomerMeterInaccuraciesAndErrorsErrorMargin_AO30, Tag = this.GetTagFormat(WatBal_CustomerMeterInaccuraciesAndErrorsErrorMargin_AO30) },
                new OpcValue { Value = wb.CustomerMeterInaccuraciesAndErrorsM3_AC29, Tag = this.GetTagFormat(WatBal_CustomerMeterInaccuraciesAndErrorsM3_AC29) },
                new OpcValue { Value = wb.NonRevenueWaterErrorMargin_AY26, Tag = this.GetTagFormat(WatBal_NonRevenueWaterErrorMargin_AY26) },
                new OpcValue { Value = wb.NonRevenueWaterM3_AY24, Tag = this.GetTagFormat(WatBal_NonRevenueWaterM3_AY24) },
                new OpcValue { Value = wb.PhyscialLossesErrorMargin_AH35, Tag = this.GetTagFormat(WatBal_PhyscialLossesErrorMargin_AH35) },
                new OpcValue { Value = wb.PhysicalLossesM3_T34, Tag = this.GetTagFormat(WatBal_PhysicalLossesM3_T34) },
                new OpcValue { Value = wb.RevenueWaterM3_AY8, Tag = this.GetTagFormat(WatBal_RevenueWaterM3_AY8) },
                new OpcValue { Value = wb.SystemInputVolumeErrorMargin_B21, Tag = this.GetTagFormat(WatBal_SystemInputVolumeErrorMargin_B21) },
                new OpcValue { Value = wb.SystemInputVolume_B19, Tag = this.GetTagFormat(WatBal_SystemInputVolume_B19) },
                new OpcValue { Value = wb.UnauthorizedConsumptionErrorMargin_AO25, Tag = this.GetTagFormat(WatBal_UnauthorizedConsumptionErrorMargin_AO25) },
                new OpcValue { Value = wb.UnauthorizedConsumption_AC24, Tag = this.GetTagFormat(WatBal_UnauthorizedConsumption_AC24) },
                new OpcValue { Value = wb.UnbilledAuthorizedConsumptionErrorMargin_T20, Tag = this.GetTagFormat(WatBal_UnbilledAuthorizedConsumptionErrorMargin_T20) },
                new OpcValue { Value = wb.UnbilledAuthorizedConsumption_T16, Tag = this.GetTagFormat(WatBal_UnbilledAuthorizedConsumption_T16) },
                new OpcValue { Value = wb.UnbilledMeteredConsumption_AC14, Tag = this.GetTagFormat(WatBal_UnbilledMeteredConsumption_AC14) },
                new OpcValue { Value = wb.UnbilledUnmeteredConsumptionErrorMargin_AO20, Tag = this.GetTagFormat(WatBal_UnbilledUnmeteredConsumptionErrorMargin_AO20) },
                new OpcValue { Value = wb.UnbilledUnmeteredConsumption_AC19, Tag = this.GetTagFormat(WatBal_UnbilledUnmeteredConsumption_AC19) },
                new OpcValue { Value = wb.WaterLossesErrorMargin_K31, Tag = this.GetTagFormat(WatBal_WaterLossesErrorMargin_K31) },
                new OpcValue { Value = wb.WaterLosses_K29, Tag = this.GetTagFormat(WatBal_WaterLosses_K29) },
                new OpcValue { Value = pi.CaplBestEstimate_F17, Tag = this.GetTagFormat(PIs_CaplBestEstimate_F17) },
                new OpcValue { Value = pi.MaplBestEstimate_F19, Tag = this.GetTagFormat(PIs_MaplBestEstimate_F19) },
                new OpcValue { Value = pi.IliBestEstimate_F25, Tag = this.GetTagFormat(PIs_IliBestEstimate_F25) },
            };
            return result;
        }

        public EasyCalcSheetData Map(ICollection<OpcValue> opcValues)
        {
            var dictionary = opcValues.ToDictionary(x => x.Tag, x => Convert.ToDouble(x.Value));
            var model = new EasyCalcSheetData();

            model.StartSheet = new StartSheet
            {
                PeriodDays_M21 = (int)dictionary[this.GetTagFormat(Start_PeriodDays_M21)],
            };
            model.SystemInputSheet = new SystemInputSheet
            {
                SystemInputVolumeM3_D6_D70 = new List<double>
                    {
                        dictionary[this.GetTagFormat(SysInput_SystemInputVolumeM3_D6)],
                        dictionary[this.GetTagFormat(SysInput_SystemInputVolumeM3_D7)],
                        dictionary[this.GetTagFormat(SysInput_SystemInputVolumeM3_D8)],
                        dictionary[this.GetTagFormat(SysInput_SystemInputVolumeM3_D9)],
                        dictionary[this.GetTagFormat(SysInput_SystemInputVolumeM3_D10)],
                        dictionary[this.GetTagFormat(SysInput_SystemInputVolumeM3_D11)],
                        dictionary[this.GetTagFormat(SysInput_SystemInputVolumeM3_D12)],
                        dictionary[this.GetTagFormat(SysInput_SystemInputVolumeM3_D13)],
                        dictionary[this.GetTagFormat(SysInput_SystemInputVolumeM3_D14)],
                        dictionary[this.GetTagFormat(SysInput_SystemInputVolumeM3_D15)],
                        dictionary[this.GetTagFormat(SysInput_SystemInputVolumeM3_D16)],
                        dictionary[this.GetTagFormat(SysInput_SystemInputVolumeM3_D17)],
                        dictionary[this.GetTagFormat(SysInput_SystemInputVolumeM3_D18)],
                        dictionary[this.GetTagFormat(SysInput_SystemInputVolumeM3_D19)],
                        dictionary[this.GetTagFormat(SysInput_SystemInputVolumeM3_D20)],
                        dictionary[this.GetTagFormat(SysInput_SystemInputVolumeM3_D21)],
                        dictionary[this.GetTagFormat(SysInput_SystemInputVolumeM3_D22)],
                        dictionary[this.GetTagFormat(SysInput_SystemInputVolumeM3_D23)],
                        dictionary[this.GetTagFormat(SysInput_SystemInputVolumeM3_D24)],
                        dictionary[this.GetTagFormat(SysInput_SystemInputVolumeM3_D25)],
                        dictionary[this.GetTagFormat(SysInput_SystemInputVolumeM3_D26)],
                    },
                SystemInputVolumeError_F6_F70 = new List<double>
                    {
                        dictionary[this.GetTagFormat(SysInput_SystemInputVolumeError_F6)],
                        dictionary[this.GetTagFormat(SysInput_SystemInputVolumeError_F7)],
                        dictionary[this.GetTagFormat(SysInput_SystemInputVolumeError_F8)],
                        dictionary[this.GetTagFormat(SysInput_SystemInputVolumeError_F9)],
                        dictionary[this.GetTagFormat(SysInput_SystemInputVolumeError_F10)],
                        dictionary[this.GetTagFormat(SysInput_SystemInputVolumeError_F11)],
                        dictionary[this.GetTagFormat(SysInput_SystemInputVolumeError_F12)],
                        dictionary[this.GetTagFormat(SysInput_SystemInputVolumeError_F13)],
                        dictionary[this.GetTagFormat(SysInput_SystemInputVolumeError_F14)],
                        dictionary[this.GetTagFormat(SysInput_SystemInputVolumeError_F15)],
                        dictionary[this.GetTagFormat(SysInput_SystemInputVolumeError_F16)],
                        dictionary[this.GetTagFormat(SysInput_SystemInputVolumeError_F17)],
                        dictionary[this.GetTagFormat(SysInput_SystemInputVolumeError_F18)],
                        dictionary[this.GetTagFormat(SysInput_SystemInputVolumeError_F19)],
                        dictionary[this.GetTagFormat(SysInput_SystemInputVolumeError_F20)],
                        dictionary[this.GetTagFormat(SysInput_SystemInputVolumeError_F21)],
                        dictionary[this.GetTagFormat(SysInput_SystemInputVolumeError_F22)],
                        dictionary[this.GetTagFormat(SysInput_SystemInputVolumeError_F23)],
                        dictionary[this.GetTagFormat(SysInput_SystemInputVolumeError_F24)],
                        dictionary[this.GetTagFormat(SysInput_SystemInputVolumeError_F25)],
                        dictionary[this.GetTagFormat(SysInput_SystemInputVolumeError_F26)],
                    },
            };
            model.BilledConsumptionSheet = new BilledConsumptionSheet
            {
                BilledMeteredConsumptionBulkWaterSupplyExportM3_D6 = dictionary[this.GetTagFormat(BilledCons_BilledMetConsBulkWatSupExpM3_D6)],
                BulledUnmeteredConsumptionBulkWaterSupplyExportM3_H6 = dictionary[this.GetTagFormat(BilledCons_BilledUnmetConsBulkWatSupExpM3_H6)],
            };
            model.UnbilledConsumptionSheet = new UnbilledConsumptionSheet
            {
                MeteredConsumptionBulkWaterSupplyExportM3_D6 = dictionary[this.GetTagFormat(UnbilledCons_MetConsBulkWatSupExpM3_D6)],
            };
            model.UnauthorizedConsumptionSheet = new UnauthorizedConsumptionSheet(model)
            {
                IllegalConnectionsDomesticEstimatedNumber_D6 = (int)dictionary[this.GetTagFormat(UnauthCons_IllegalConnDomEstNo_D6)],
                IllegalConnectionsDomesticPersonsPerHouse_H6 = (int)dictionary[this.GetTagFormat(UnauthCons_IllegalConnDomPersPerHouse_H6)],
                IllegalConnectionsDomesticConsumptionLitersPerPersonPerDay_J6 = dictionary[this.GetTagFormat(UnauthCons_IllegalConnDomConsLitPerPersDay_J6)],
                IllegalConnectionsDomesticErrorMargin_F6 = dictionary[this.GetTagFormat(UnauthCons_IllegalConnDomErrorMargin_F6)],
                IllegalConnectionsOthersErrorMargin_F10 = dictionary[this.GetTagFormat(UnauthCons_IllegalConnOthersErrorMargin_F10)],
                MeterTamperingBypassesEtcEstimatedNumber_D14 = dictionary[this.GetTagFormat(UnauthCons_MeterTampBypEtcEstNo_D14)],
                MeterTamperingBypassesEtcErrorMargin_F14 = dictionary[this.GetTagFormat(UnauthCons_MeterTampBypEtcErrorMargin_F14)],
                MeterTamperingBypassesEtcConsumptionLitersPerCustomerPerDay_J14 = dictionary[this.GetTagFormat(UnauthCons_MeterTampBypEtcConsLitPerCustDay_J14)],
            };
            model.MeterErrorsSheet = new MeterErrorsSheet(model)
            {
                DetailedManualSpec_J6 = dictionary[this.GetTagFormat(MetErrors_DetailedManualSpec_J6)] == 2,
                BilledMeteredConsumptionWithoutBulkSupplyMeterUnderregistration_H8 = dictionary[this.GetTagFormat(MetErrors_BilledMetConsWoBulkSupMetUndrreg_H8)],
                BilledMeteredConsumptionWithoutBulkSupplyErrorMargin_N8 = dictionary[this.GetTagFormat(MetErrors_BilledMetConsWoBulkSupErrorMargin_N8)],
                MeteredBulkSupplyExportMetereUnderregistration_H32 = dictionary[this.GetTagFormat(MetErrors_MetBulkSupExpMetUnderreg_H32)],
                UnbilledMeteredConsumptionWithoutBulkSupplyMeterUnderregistration_H34 = dictionary[this.GetTagFormat(MetErrors_UnbillMetConsWoBulkSupplMetUndrreg_H34)],
                CorruptMeterReadingPracticesMeterUnderregistration_H38 = dictionary[this.GetTagFormat(MetErrors_CorruptMetReadPractMetUndrreg_H38)],
            };
            model.NetworkSheet = new NetworkSheet(model)
            {
                DistributionAndTransmissionMainsEntries_D7_D26 = new List<double>
                {
                    dictionary[this.GetTagFormat(Network_DistributionAndTransmissionMains_D7)],
                    dictionary[this.GetTagFormat(Network_DistributionAndTransmissionMains_D8)],
                    dictionary[this.GetTagFormat(Network_DistributionAndTransmissionMains_D9)],
                    dictionary[this.GetTagFormat(Network_DistributionAndTransmissionMains_D10)],
                    dictionary[this.GetTagFormat(Network_DistributionAndTransmissionMains_D11)],
                    dictionary[this.GetTagFormat(Network_DistributionAndTransmissionMains_D12)],
                    dictionary[this.GetTagFormat(Network_DistributionAndTransmissionMains_D13)],
                    dictionary[this.GetTagFormat(Network_DistributionAndTransmissionMains_D14)],
                    dictionary[this.GetTagFormat(Network_DistributionAndTransmissionMains_D15)],
                    dictionary[this.GetTagFormat(Network_DistributionAndTransmissionMains_D16)],
                },
                NumberOfConnectionsOfRegsteredCustomers_H10 = dictionary[this.GetTagFormat(Network_NoOfConnOfRegCustomers_H10)],
                NumberOfInactiveAccountsWServiceConnections_H18 = dictionary[this.GetTagFormat(Network_NoOfInactAccountsWSvcConns_H18)],
                AvgLenOfServiceConnectionFromBoundaryToMeterM_H32 = dictionary[this.GetTagFormat(Network_AvgLenOfSvcConnFromBoundaryToMeterM_H32)],
            };
            model.PressureSheet = new PressureSheet()
            {
                ApproximateNumberOfConnections_D7_D24 = new List<double>
                {
                    dictionary[this.GetTagFormat(Prs_ApproxNoOfConn_D7)],
                    dictionary[this.GetTagFormat(Prs_ApproxNoOfConn_D8)],
                    dictionary[this.GetTagFormat(Prs_ApproxNoOfConn_D9)],
                    dictionary[this.GetTagFormat(Prs_ApproxNoOfConn_D10)],
                    dictionary[this.GetTagFormat(Prs_ApproxNoOfConn_D11)],
                    dictionary[this.GetTagFormat(Prs_ApproxNoOfConn_D12)],
                    dictionary[this.GetTagFormat(Prs_ApproxNoOfConn_D13)],
                    dictionary[this.GetTagFormat(Prs_ApproxNoOfConn_D14)],
                    dictionary[this.GetTagFormat(Prs_ApproxNoOfConn_D15)],
                    dictionary[this.GetTagFormat(Prs_ApproxNoOfConn_D16)],
                },
                DailyAveragePressureM_F7_F24 = new List<double>
                {
                    dictionary[this.GetTagFormat(Prs_DailyAvgPrsM_F7)],
                    dictionary[this.GetTagFormat(Prs_DailyAvgPrsM_F8)],
                    dictionary[this.GetTagFormat(Prs_DailyAvgPrsM_F9)],
                    dictionary[this.GetTagFormat(Prs_DailyAvgPrsM_F10)],
                    dictionary[this.GetTagFormat(Prs_DailyAvgPrsM_F11)],
                    dictionary[this.GetTagFormat(Prs_DailyAvgPrsM_F12)],
                    dictionary[this.GetTagFormat(Prs_DailyAvgPrsM_F13)],
                    dictionary[this.GetTagFormat(Prs_DailyAvgPrsM_F14)],
                    dictionary[this.GetTagFormat(Prs_DailyAvgPrsM_F15)],
                    dictionary[this.GetTagFormat(Prs_DailyAvgPrsM_F16)],
                },
            };
            model.IntermittentSupply = new IntermittentSupplySheet();
            model.PiSheet = new PiSheet(model);
            model.WaterBalanceSheet = new WaterBalanceSheet();
            return model;
        }

        private string GetTagFormat(string tag)
        {
            return string.Format(this.tagFormat, this.Zone, tag);
        }
    }
}
