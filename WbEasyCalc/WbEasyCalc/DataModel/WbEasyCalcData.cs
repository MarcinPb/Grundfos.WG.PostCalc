using System;
using WbEasyCalc;

namespace DataModel
{
    public class WbEasyCalcData : ICloneable
    {
        public int WbEasyCalcDataId { get; set; }
        public int YearNo { get; set; }
        public int MonthNo { get; set; }
        public int ZoneId { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool IsArchive { get; set; }
        public bool IsAccepted { get; set; }

        // input
        //public EasyCalcDataInput EasyCalcDataInput { get; set; }
        public int Start_PeriodDays_M21 { get; set; }
        public double SysInput_SystemInputVolumeM3_D6 { get; set; }
        public double SysInput_SystemInputVolumeError_F6 { get; set; }
        public double BilledCons_BilledMetConsBulkWatSupExpM3_D6 { get; set; }
        public double BilledCons_BilledUnmetConsBulkWatSupExpM3_H6 { get; set; }
        public double UnbilledCons_MetConsBulkWatSupExpM3_D6 { get; set; }
        public int UnauthCons_IllegalConnDomEstNo_D6 { get; set; }
        public double UnauthCons_IllegalConnDomPersPerHouse_H6 { get; set; }
        public double UnauthCons_IllegalConnDomConsLitPerPersDay_J6 { get; set; }
        public double UnauthCons_IllegalConnDomErrorMargin_F6 { get; set; }
        public double UnauthCons_IllegalConnOthersErrorMargin_F10 { get; set; }
        public double UnauthCons_MeterTampBypEtcEstNo_D14 { get; set; }
        public double UnauthCons_MeterTampBypEtcErrorMargin_F14 { get; set; }
        public double UnauthCons_MeterTampBypEtcConsLitPerCustDay_J14 { get; set; }
        public double MetErrors_DetailedManualSpec_J6 { get; set; }
        public double MetErrors_BilledMetConsWoBulkSupMetUndrreg_H8 { get; set; }
        public double MetErrors_BilledMetConsWoBulkSupErrorMargin_N8 { get; set; }
        public double MetErrors_MetBulkSupExpMetUnderreg_H32 { get; set; }
        public double MetErrors_UnbillMetConsWoBulkSupplMetUndrreg_H34 { get; set; }
        public double MetErrors_CorruptMetReadPractMetUndrreg_H38 { get; set; }
        public double Network_DistributionAndTransmissionMains_D7 { get; set; }
        public double Network_NoOfConnOfRegCustomers_H10 { get; set; }
        public double Network_NoOfInactAccountsWSvcConns_H18 { get; set; }
        public double Network_AvgLenOfSvcConnFromBoundaryToMeterM_H32 { get; set; }
        public double Prs_ApproxNoOfConn_D7 { get; set; }
        public double Prs_DailyAvgPrsM_F7 { get; set; }
        public double PIs_IliBestEstimate_F25 { get; set; }

        // output
        //public EasyCalcDataOutput EasyCalcDataOutput { get; set; }
        public double SystemInputVolume_B19 { get; set; }
        public double SystemInputVolumeErrorMargin_B21 { get; set; }
        public double AuthorizedConsumption_K12 { get; set; }
        public double AuthorizedConsumptionErrorMargin_K15 { get; set; }
        public double WaterLosses_K29 { get; set; }
        public double WaterLossesErrorMargin_K31 { get; set; }
        public double BilledAuthorizedConsumption_T8 { get; set; }
        public double UnbilledAuthorizedConsumption_T16 { get; set; }
        public double UnbilledAuthorizedConsumptionErrorMargin_T20 { get; set; }
        public double CommercialLosses_T26 { get; set; }
        public double CommercialLossesErrorMargin_T29 { get; set; }
        public double PhysicalLossesM3_T34 { get; set; }
        public double PhyscialLossesErrorMargin_AH35 { get; set; }
        public double BilledMeteredConsumption_AC4 { get; set; }
        public double BilledUnmeteredConsumption_AC9 { get; set; }
        public double UnbilledMeteredConsumption_AC14 { get; set; }
        public double UnbilledUnmeteredConsumption_AC19 { get; set; }
        public double UnbilledUnmeteredConsumptionErrorMargin_AO20 { get; set; }
        public double UnauthorizedConsumption_AC24 { get; set; }
        public double UnauthorizedConsumptionErrorMargin_AO25 { get; set; }
        public double CustomerMeterInaccuraciesAndErrorsM3_AC29 { get; set; }
        public double CustomerMeterInaccuraciesAndErrorsErrorMargin_AO30 { get; set; }
        public double RevenueWaterM3_AY8 { get; set; }
        public double NonRevenueWaterM3_AY24 { get; set; }
        public double NonRevenueWaterErrorMargin_AY26 { get; set; }

        public object Clone()
        {
            return new WbEasyCalcData()
            {
                WbEasyCalcDataId  = WbEasyCalcDataId,
                ZoneId  = ZoneId,
                YearNo  = YearNo,
                MonthNo  = MonthNo, 
                Description  = Description, 
                IsArchive  = IsArchive,
                IsAccepted = IsAccepted,

                // input
                //EasyCalcDataInput = (EasyCalcDataInput)EasyCalcDataInput.Clone(),
                Start_PeriodDays_M21 = Start_PeriodDays_M21,
                SysInput_SystemInputVolumeM3_D6 = SysInput_SystemInputVolumeM3_D6,
                SysInput_SystemInputVolumeError_F6 = SysInput_SystemInputVolumeError_F6,
                BilledCons_BilledMetConsBulkWatSupExpM3_D6 = BilledCons_BilledMetConsBulkWatSupExpM3_D6,
                BilledCons_BilledUnmetConsBulkWatSupExpM3_H6 = BilledCons_BilledUnmetConsBulkWatSupExpM3_H6,
                UnbilledCons_MetConsBulkWatSupExpM3_D6 = UnbilledCons_MetConsBulkWatSupExpM3_D6,
                UnauthCons_IllegalConnDomEstNo_D6 = UnauthCons_IllegalConnDomEstNo_D6,
                UnauthCons_IllegalConnDomPersPerHouse_H6 = UnauthCons_IllegalConnDomPersPerHouse_H6,
                UnauthCons_IllegalConnDomConsLitPerPersDay_J6 = UnauthCons_IllegalConnDomConsLitPerPersDay_J6,
                UnauthCons_IllegalConnDomErrorMargin_F6 = UnauthCons_IllegalConnDomErrorMargin_F6,
                UnauthCons_IllegalConnOthersErrorMargin_F10 = UnauthCons_IllegalConnOthersErrorMargin_F10,
                UnauthCons_MeterTampBypEtcEstNo_D14 = UnauthCons_MeterTampBypEtcEstNo_D14,
                UnauthCons_MeterTampBypEtcErrorMargin_F14 = UnauthCons_MeterTampBypEtcErrorMargin_F14,
                UnauthCons_MeterTampBypEtcConsLitPerCustDay_J14 = UnauthCons_MeterTampBypEtcConsLitPerCustDay_J14,
                MetErrors_DetailedManualSpec_J6 = MetErrors_DetailedManualSpec_J6,
                MetErrors_BilledMetConsWoBulkSupMetUndrreg_H8 = MetErrors_BilledMetConsWoBulkSupMetUndrreg_H8,
                MetErrors_BilledMetConsWoBulkSupErrorMargin_N8 = MetErrors_BilledMetConsWoBulkSupErrorMargin_N8,
                MetErrors_MetBulkSupExpMetUnderreg_H32 = MetErrors_MetBulkSupExpMetUnderreg_H32,
                MetErrors_UnbillMetConsWoBulkSupplMetUndrreg_H34 = MetErrors_UnbillMetConsWoBulkSupplMetUndrreg_H34,
                MetErrors_CorruptMetReadPractMetUndrreg_H38 = MetErrors_CorruptMetReadPractMetUndrreg_H38,
                Network_DistributionAndTransmissionMains_D7 = Network_DistributionAndTransmissionMains_D7,
                Network_NoOfConnOfRegCustomers_H10 = Network_NoOfConnOfRegCustomers_H10,
                Network_NoOfInactAccountsWSvcConns_H18 = Network_NoOfInactAccountsWSvcConns_H18,
                Network_AvgLenOfSvcConnFromBoundaryToMeterM_H32 = Network_AvgLenOfSvcConnFromBoundaryToMeterM_H32,
                Prs_ApproxNoOfConn_D7 = Prs_ApproxNoOfConn_D7,
                Prs_DailyAvgPrsM_F7 = Prs_DailyAvgPrsM_F7,
                PIs_IliBestEstimate_F25 = PIs_IliBestEstimate_F25,

                // output
                //EasyCalcDataOutput = (EasyCalcDataOutput)EasyCalcDataOutput.Clone(),
                SystemInputVolume_B19  = SystemInputVolume_B19,
                SystemInputVolumeErrorMargin_B21  = SystemInputVolumeErrorMargin_B21,
                AuthorizedConsumption_K12  = AuthorizedConsumption_K12,
                AuthorizedConsumptionErrorMargin_K15  = AuthorizedConsumptionErrorMargin_K15,
                WaterLosses_K29  = WaterLosses_K29,
                WaterLossesErrorMargin_K31  = WaterLossesErrorMargin_K31,
                BilledAuthorizedConsumption_T8  = BilledAuthorizedConsumption_T8,
                UnbilledAuthorizedConsumption_T16  = UnbilledAuthorizedConsumption_T16,
                UnbilledAuthorizedConsumptionErrorMargin_T20  = UnbilledAuthorizedConsumptionErrorMargin_T20,
                CommercialLosses_T26  = CommercialLosses_T26,
                CommercialLossesErrorMargin_T29  = CommercialLossesErrorMargin_T29,
                PhysicalLossesM3_T34  = PhysicalLossesM3_T34,
                PhyscialLossesErrorMargin_AH35  = PhyscialLossesErrorMargin_AH35,
                BilledMeteredConsumption_AC4  = BilledMeteredConsumption_AC4,
                BilledUnmeteredConsumption_AC9  = BilledUnmeteredConsumption_AC9,
                UnbilledMeteredConsumption_AC14  = UnbilledMeteredConsumption_AC14,
                UnbilledUnmeteredConsumption_AC19  = UnbilledUnmeteredConsumption_AC19,
                UnbilledUnmeteredConsumptionErrorMargin_AO20  = UnbilledUnmeteredConsumptionErrorMargin_AO20,
                UnauthorizedConsumption_AC24  = UnauthorizedConsumption_AC24,
                UnauthorizedConsumptionErrorMargin_AO25  = UnauthorizedConsumptionErrorMargin_AO25,
                CustomerMeterInaccuraciesAndErrorsM3_AC29  = CustomerMeterInaccuraciesAndErrorsM3_AC29,
                CustomerMeterInaccuraciesAndErrorsErrorMargin_AO30  = CustomerMeterInaccuraciesAndErrorsErrorMargin_AO30,
                RevenueWaterM3_AY8  = RevenueWaterM3_AY8,
                NonRevenueWaterM3_AY24  = NonRevenueWaterM3_AY24,
                NonRevenueWaterErrorMargin_AY26  = NonRevenueWaterErrorMargin_AY26,
            };
        }

        private EasyCalcDataInput _easyCalcDataInput;
        public EasyCalcDataInput EasyCalcDataInput
        {
            get
            {
                _easyCalcDataInput = new EasyCalcDataInput()
                {
                    Start_PeriodDays_M21 = Start_PeriodDays_M21,
                    SysInput_SystemInputVolumeM3_D6 = SysInput_SystemInputVolumeM3_D6,
                    SysInput_SystemInputVolumeError_F6 = SysInput_SystemInputVolumeError_F6,
                    BilledCons_BilledMetConsBulkWatSupExpM3_D6 = BilledCons_BilledMetConsBulkWatSupExpM3_D6,
                    BilledCons_BilledUnmetConsBulkWatSupExpM3_H6 = BilledCons_BilledUnmetConsBulkWatSupExpM3_H6,
                    UnbilledCons_MetConsBulkWatSupExpM3_D6 = UnbilledCons_MetConsBulkWatSupExpM3_D6,
                    UnauthCons_IllegalConnDomEstNo_D6 = UnauthCons_IllegalConnDomEstNo_D6,
                    UnauthCons_IllegalConnDomErrorMargin_F6 = UnauthCons_IllegalConnDomErrorMargin_F6,
                    UnauthCons_IllegalConnDomPersPerHouse_H6 = UnauthCons_IllegalConnDomPersPerHouse_H6,
                    UnauthCons_IllegalConnDomConsLitPerPersDay_J6 = UnauthCons_IllegalConnDomConsLitPerPersDay_J6,
                    UnauthCons_IllegalConnOthersErrorMargin_F10 = UnauthCons_IllegalConnOthersErrorMargin_F10,
                    UnauthCons_MeterTampBypEtcEstNo_D14 = UnauthCons_MeterTampBypEtcEstNo_D14,
                    UnauthCons_MeterTampBypEtcErrorMargin_F14 = UnauthCons_MeterTampBypEtcErrorMargin_F14,
                    UnauthCons_MeterTampBypEtcConsLitPerCustDay_J14 = UnauthCons_MeterTampBypEtcConsLitPerCustDay_J14,
                    MetErrors_DetailedManualSpec_J6 = Math.Abs(MetErrors_DetailedManualSpec_J6-2)<0.01,
                    MetErrors_BilledMetConsWoBulkSupMetUndrreg_H8 = MetErrors_BilledMetConsWoBulkSupMetUndrreg_H8,
                    MetErrors_BilledMetConsWoBulkSupErrorMargin_N8 = MetErrors_BilledMetConsWoBulkSupErrorMargin_N8,
                    MetErrors_MetBulkSupExpMetUnderreg_H32 = MetErrors_MetBulkSupExpMetUnderreg_H32,
                    MetErrors_UnbillMetConsWoBulkSupplMetUndrreg_H34 = MetErrors_UnbillMetConsWoBulkSupplMetUndrreg_H34,
                    MetErrors_CorruptMetReadPractMetUndrreg_H38 = MetErrors_CorruptMetReadPractMetUndrreg_H38,
                    Network_DistributionAndTransmissionMains_D7 = Network_DistributionAndTransmissionMains_D7,
                    Network_NoOfConnOfRegCustomers_H10 = Network_NoOfConnOfRegCustomers_H10,
                    Network_NoOfInactAccountsWSvcConns_H18 = Network_NoOfInactAccountsWSvcConns_H18,
                    Network_AvgLenOfSvcConnFromBoundaryToMeterM_H32 = Network_AvgLenOfSvcConnFromBoundaryToMeterM_H32,
                    Prs_ApproxNoOfConn_D7 = Prs_ApproxNoOfConn_D7,
                    Prs_DailyAvgPrsM_F7 = Prs_DailyAvgPrsM_F7
                };
                return _easyCalcDataInput;
            }
        }
    }
}
