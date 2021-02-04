using System;
using System.Collections.Generic;
using WbEasyCalc;
using WbEasyCalcRepository.Model;

namespace WbEasyCalcRepository
{
    public class WbEasyCalc : IWbEasyCalc
    {
        //public void PublishSheetData(EasyCalcSheetData data, string zoneName)
        //{
        //}

        public EasyCalcDataOutput Calculate(EasyCalcDataInput easyCalcDataInput)
        {
            EasyCalcSheetData easyCalcSheetData = ReadSheetData(easyCalcDataInput);

            EasyCalcRefactored.GetWaterLosses(easyCalcSheetData);
            EasyCalcRefactored.GetWaterLossesErrorMargin(easyCalcSheetData);

            EasyCalcDataOutput easyCalcDataOutput = new EasyCalcDataOutput()
            {
                SystemInputVolume_B19 = easyCalcSheetData.WaterBalanceSheet.SystemInputVolume_B19,
                SystemInputVolumeErrorMargin_B21 = easyCalcSheetData.WaterBalanceSheet.SystemInputVolumeErrorMargin_B21,

                AuthorizedConsumption_K12 = easyCalcSheetData.WaterBalanceSheet.AuthorizedConsumption_K12,
                AuthorizedConsumptionErrorMargin_K15 = easyCalcSheetData.WaterBalanceSheet.AuthorizedConsumptionErrorMargin_K15,
                WaterLosses_K29 = easyCalcSheetData.WaterBalanceSheet.WaterLosses_K29,
                WaterLossesErrorMargin_K31 = easyCalcSheetData.WaterBalanceSheet.WaterLossesErrorMargin_K31,

                BilledAuthorizedConsumption_T8 = easyCalcSheetData.WaterBalanceSheet.BilledAuthorizedConsumption_T8,
                UnbilledAuthorizedConsumption_T16 = easyCalcSheetData.WaterBalanceSheet.UnbilledAuthorizedConsumption_T16,
                UnbilledAuthorizedConsumptionErrorMargin_T20 = easyCalcSheetData.WaterBalanceSheet.UnbilledAuthorizedConsumptionErrorMargin_T20,
                CommercialLosses_T26 = easyCalcSheetData.WaterBalanceSheet.CommercialLosses_T26,
                CommercialLossesErrorMargin_T29 = easyCalcSheetData.WaterBalanceSheet.CommercialLossesErrorMargin_T29,
                PhysicalLossesM3_T34 = easyCalcSheetData.WaterBalanceSheet.PhysicalLossesM3_T34,
                PhyscialLossesErrorMargin_AH35 = easyCalcSheetData.WaterBalanceSheet.PhyscialLossesErrorMargin_AH35,

                BilledMeteredConsumption_AC4 = easyCalcSheetData.WaterBalanceSheet.BilledMeteredConsumption_AC4,
                BilledUnmeteredConsumption_AC9 = easyCalcSheetData.WaterBalanceSheet.BilledUnmeteredConsumption_AC9,
                UnbilledMeteredConsumption_AC14 = easyCalcSheetData.WaterBalanceSheet.UnbilledMeteredConsumption_AC14,

                UnbilledUnmeteredConsumption_AC19 = easyCalcSheetData.WaterBalanceSheet.UnbilledUnmeteredConsumption_AC19,
                UnbilledUnmeteredConsumptionErrorMargin_AO20 = easyCalcSheetData.WaterBalanceSheet.UnbilledUnmeteredConsumptionErrorMargin_AO20,
                UnauthorizedConsumption_AC24 = easyCalcSheetData.WaterBalanceSheet.UnauthorizedConsumption_AC24,
                UnauthorizedConsumptionErrorMargin_AO25 = easyCalcSheetData.WaterBalanceSheet.UnauthorizedConsumptionErrorMargin_AO25,
                CustomerMeterInaccuraciesAndErrorsM3_AC29 = easyCalcSheetData.WaterBalanceSheet.CustomerMeterInaccuraciesAndErrorsM3_AC29,
                CustomerMeterInaccuraciesAndErrorsErrorMargin_AO30 = easyCalcSheetData.WaterBalanceSheet.CustomerMeterInaccuraciesAndErrorsErrorMargin_AO30,

                RevenueWaterM3_AY8 = easyCalcSheetData.WaterBalanceSheet.RevenueWaterM3_AY8,
                NonRevenueWaterM3_AY24 = easyCalcSheetData.WaterBalanceSheet.NonRevenueWaterM3_AY24,
                NonRevenueWaterErrorMargin_AY26 = easyCalcSheetData.WaterBalanceSheet.NonRevenueWaterErrorMargin_AY26,

                AverageSupplyTimeHPerDayBestEstimate_F9 = easyCalcSheetData.PiSheet.AverageSupplyTimeHPerDayBestEstimate_F9,
                AveragePressureMBestEstimate_F11 = easyCalcSheetData.PiSheet.AveragePressureMBestEstimate_F11,


                Prs_BestEstimate_F33 = easyCalcSheetData.PressureSheet.AveragePressureBestEstimate_F33,
                Prs_Min_F29 = easyCalcSheetData.PressureSheet.Prs_Min_F29,
                Prs_Max_F31 = easyCalcSheetData.PressureSheet.Prs_Max_F31,

                SysInput_ErrorMarg_F72 = easyCalcSheetData.SystemInputSheet.ErrorMargin_F72,
                SysInput_Min_D75 = easyCalcSheetData.SystemInputSheet.Min_D75,
                SysInput_Max_D77 = easyCalcSheetData.SystemInputSheet.Max_D77,
                SysInput_BestEstimate_D79 = easyCalcSheetData.SystemInputSheet.SystemInputVolume_D79,

                BilledCons_Sum_D28 = easyCalcSheetData.BilledConsumptionSheet.BilledMeteredConsumption_D28,
                BilledCons_Sum_H28 = easyCalcSheetData.BilledConsumptionSheet.BilledUnmeteredConsumption_H28,
                UnbilledCons_Sum_D32 = easyCalcSheetData.UnbilledConsumptionSheet.UnbilledMeteredConsumption_D32,
                UnbilledCons_ErrorMarg_J25 = easyCalcSheetData.UnbilledConsumptionSheet.UnbilledUnmeteredConsumptionErrorMargin_J25,
                UnbilledCons_Min_H28 = easyCalcSheetData.UnbilledConsumptionSheet.Min_H28,
                UnbilledCons_Max_H30 = easyCalcSheetData.UnbilledConsumptionSheet.Max_H30,
                UnbilledCons_BestEstimate_H32 = easyCalcSheetData.UnbilledConsumptionSheet.UnbilledUnmeteredConsumption_H32,
                UnauthCons_Total_L6 = easyCalcSheetData.UnauthorizedConsumptionSheet.IllegalConnectionsDomesticConsumptionTotalM3_L6,
                UnauthCons_Total_L10 = easyCalcSheetData.UnauthorizedConsumptionSheet.IllegalConnectionsOthersConsumptionTotalM3_L10,
                UnauthCons_Total_L14 = easyCalcSheetData.UnauthorizedConsumptionSheet.MeterTamperingBypassesEtcConsumptionTotalM3_L14,
                UnauthCons_ErrorMarg_F24 = easyCalcSheetData.UnauthorizedConsumptionSheet.ErrorMargin_F24,
                UnauthCons_Min_L27 = easyCalcSheetData.UnauthorizedConsumptionSheet.Min_L27,
                UnauthCons_Max_L29 = easyCalcSheetData.UnauthorizedConsumptionSheet.Max_L29,
                UnauthCons_BestEstimate_L31 = easyCalcSheetData.UnauthorizedConsumptionSheet.BestEstimateTotal_L31,

                MetErrors_Total_F8 = easyCalcSheetData.MeterErrorsSheet.BilledMeteredConsumptionWithoutBulkSupplyTotalM3_F8,
                MetErrors_Total_F32 = easyCalcSheetData.MeterErrorsSheet.MeteredBulkSupplyExportTotalM3_F32,
                MetErrors_Total_F34 = easyCalcSheetData.MeterErrorsSheet.UnbilledMeteredConsumptionWithoutBulkSupplyM3_F34,
                MetErrors_Total_F38 = easyCalcSheetData.MeterErrorsSheet.CorruptMeterReadingPracticesTotalM3_F38,
                MetErrors_Total_L8 = easyCalcSheetData.MeterErrorsSheet.BilledMeteredConsumptionWithoutBulkSupplyTotalM3_L8,
                MetErrors_Total_L32 = easyCalcSheetData.MeterErrorsSheet.MeteredBulkSupplyExportTotalWithMeterUnderregistrationM3_L32,
                MetErrors_Total_L34 = easyCalcSheetData.MeterErrorsSheet.UnbilledMeteredConsumptionWithoutBulkSupplyWithMeterUnderregistration_L34,
                MetErrors_Total_L38 = easyCalcSheetData.MeterErrorsSheet.CorruptMeterReadingPracticesWithMeterUnderregistrationM3_L38,
                MetErrors_ErrorMarg_N42 = easyCalcSheetData.MeterErrorsSheet.ErrorMarginTotal_N42,
                MetErrors_Min_L45 = easyCalcSheetData.MeterErrorsSheet.Min_L45,
                MetErrors_Max_L47 = easyCalcSheetData.MeterErrorsSheet.Max_L47,
                MetErrors_BestEstimate_L49 = easyCalcSheetData.MeterErrorsSheet.BestEstimateTotalM3_L49,

                MetErrors_Total_L12 = easyCalcSheetData.MeterErrorsSheet.BilledMeteredConsumptionManuallyEnteredWithMeterUnderregistrationM3_L12_L28[0],
                MetErrors_Total_L13 = easyCalcSheetData.MeterErrorsSheet.BilledMeteredConsumptionManuallyEnteredWithMeterUnderregistrationM3_L12_L28[1],
                MetErrors_Total_L14 = easyCalcSheetData.MeterErrorsSheet.BilledMeteredConsumptionManuallyEnteredWithMeterUnderregistrationM3_L12_L28[2],
                MetErrors_Total_L15 = easyCalcSheetData.MeterErrorsSheet.BilledMeteredConsumptionManuallyEnteredWithMeterUnderregistrationM3_L12_L28[3],

                Network_Total_D28 = easyCalcSheetData.NetworkSheet.DistributionAndTransmissionMainsTotalKm_D28,
                //Network_Total_D32 = Network_Total_D32,
                Network_Min_D39 = easyCalcSheetData.NetworkSheet.DistributionAndTransmissionMainsMinimum_D33,
                Network_Max_D41 = easyCalcSheetData.NetworkSheet.DistributionAndTransmissionMainsMaximum_D35,
                Network_BestEstimate_D43 = easyCalcSheetData.NetworkSheet.DistributionAndTransmissionMainsBestEstimate_D37,
                Network_Number_H21 = easyCalcSheetData.NetworkSheet.EstimatedNumberOfIllegalConnections_H21,
                Network_ErrorMarg_J21 = easyCalcSheetData.NetworkSheet.Network_ErrorMarg_J21,
                Network_ErrorMarg_J24 = easyCalcSheetData.NetworkSheet.Network_ErrorMarg_J24,
                //Network_Min_H26 = easyCalcSheetData.NetworkSheet.,
                //Network_Max_H28 = easyCalcSheetData.NetworkSheet.,
                Network_BestEstimate_H30 = easyCalcSheetData.NetworkSheet.ServiceConnectionsBestEstimate_H30,
                Network_Number_H39 = easyCalcSheetData.NetworkSheet.LenOfServConnFromBoundToMeterKm_H39,
                Network_ErrorMarg_J39 = easyCalcSheetData.NetworkSheet.Network_ErrorMarg_J39,

                UnauthCons_Total_L18 = easyCalcSheetData.UnauthorizedConsumptionSheet.Total_L18_L22[0],
                UnauthCons_Total_L19 = easyCalcSheetData.UnauthorizedConsumptionSheet.Total_L18_L22[1],
                UnauthCons_Total_L20 = easyCalcSheetData.UnauthorizedConsumptionSheet.Total_L18_L22[2],
                UnauthCons_Total_L21 = easyCalcSheetData.UnauthorizedConsumptionSheet.Total_L18_L22[3],

                Interm_BestEstimate_H33 = easyCalcSheetData.IntermittentSupply.SupplyTimeBestEstimate_H33,
                Interm_Min_H29 = easyCalcSheetData.IntermittentSupply.Interm_Min_F29,
                Interm_Max_H31 = easyCalcSheetData.IntermittentSupply.Interm_Max_F31,

                Pis_AverageSupplyTime_F9 = easyCalcSheetData.PiSheet.AverageSupplyTimeHPerDayBestEstimate_F9,
                Pis_AverageSupplyTime_H9 = easyCalcSheetData.PiSheet.AverageSupplyTimeHPerDayBestEstimate_H9,
                Pis_AverageSupplyTime_J9 = easyCalcSheetData.PiSheet.AverageSupplyTimeHPerDayBestEstimate_J9,
                Pis_AverageSupplyTime_L9 = easyCalcSheetData.PiSheet.AverageSupplyTimeHPerDayBestEstimate_L9,
            };

            return easyCalcDataOutput;
        }

        // Only in test
        public EasyCalcSheetData ReadSheetData(EasyCalcDataInput easyCalcDataInput)
        {
            var data = new EasyCalcSheetData();
            data.StartSheet = new StartSheet
            {
                PeriodDays_M21 = easyCalcDataInput.Start_PeriodDays_M21,
            };
            data.SystemInputSheet = new SystemInputSheet
            {
                SystemInputVolumeM3_D6_D70 = new List<double> 
                { 
                    easyCalcDataInput.SysInput_SystemInputVolumeM3_D6,  
                    easyCalcDataInput.SysInput_SystemInputVolumeM3_D7,
                    easyCalcDataInput.SysInput_SystemInputVolumeM3_D8,  
                    easyCalcDataInput.SysInput_SystemInputVolumeM3_D9,
                },
                SystemInputVolumeError_F6_F70 = new List<double> 
                { 
                    easyCalcDataInput.SysInput_SystemInputVolumeError_F6, 
                    easyCalcDataInput.SysInput_SystemInputVolumeError_F7,
                    easyCalcDataInput.SysInput_SystemInputVolumeError_F8, 
                    easyCalcDataInput.SysInput_SystemInputVolumeError_F9,
                }
            };
            data.BilledConsumptionSheet = new BilledConsumptionSheet
            {
                BilledMeteredConsumptionBulkWaterSupplyExportM3_D6 = easyCalcDataInput.BilledCons_BilledMetConsBulkWatSupExpM3_D6,
                BulledUnmeteredConsumptionBulkWaterSupplyExportM3_H6 = easyCalcDataInput.BilledCons_BilledUnmetConsBulkWatSupExpM3_H6,
                BilledMeteredConsumptionWithoutBulkSupply_D8_D25 = new List<double> 
                { 
                    easyCalcDataInput.BilledCons_UnbMetConsM3_D8, 
                    easyCalcDataInput.BilledCons_UnbMetConsM3_D9, 
                    easyCalcDataInput.BilledCons_UnbMetConsM3_D10, 
                    easyCalcDataInput.BilledCons_UnbMetConsM3_D11, 
                },
                BilledUnmeteredConsumptionWithoutBulkSupply_H8_H25 = new List<double> 
                { 
                    easyCalcDataInput.BilledCons_UnbUnmetConsM3_H8, 
                    easyCalcDataInput.BilledCons_UnbUnmetConsM3_H9, 
                    easyCalcDataInput.BilledCons_UnbUnmetConsM3_H10, 
                    easyCalcDataInput.BilledCons_UnbUnmetConsM3_H11, 
                }, 
            };
            data.UnbilledConsumptionSheet = new UnbilledConsumptionSheet
            {
                MeteredConsumptionBulkWaterSupplyExportM3_D6 = easyCalcDataInput.UnbilledCons_MetConsBulkWatSupExpM3_D6,

                UnbilledMeteredConsumptionWithoutBulkSupply_D8_D23 = new List<double> 
                { 
                    easyCalcDataInput.UnbilledCons_UnbMetConsM3_D8, 
                    easyCalcDataInput.UnbilledCons_UnbMetConsM3_D9, 
                    easyCalcDataInput.UnbilledCons_UnbMetConsM3_D10, 
                    easyCalcDataInput.UnbilledCons_UnbMetConsM3_D11, 
                },
                UnbilledUnmeteredConsumptionM3_H6_H23 = new List<double> 
                { 
                    easyCalcDataInput.UnbilledCons_UnbUnmetConsM3_H6, 
                    easyCalcDataInput.UnbilledCons_UnbUnmetConsM3_H7, 
                    easyCalcDataInput.UnbilledCons_UnbUnmetConsM3_H8, 
                    easyCalcDataInput.UnbilledCons_UnbUnmetConsM3_H9, 
                    easyCalcDataInput.UnbilledCons_UnbUnmetConsM3_H10, 
                    easyCalcDataInput.UnbilledCons_UnbUnmetConsM3_H11, 
                },
                UnbilledUnmeteredConsumptionError_J6_J23 = new List<double> 
                { 
                    easyCalcDataInput.UnbilledCons_UnbUnmetConsError_J6, 
                    easyCalcDataInput.UnbilledCons_UnbUnmetConsError_J7, 
                    easyCalcDataInput.UnbilledCons_UnbUnmetConsError_J8, 
                    easyCalcDataInput.UnbilledCons_UnbUnmetConsError_J9, 
                    easyCalcDataInput.UnbilledCons_UnbUnmetConsError_J10, 
                    easyCalcDataInput.UnbilledCons_UnbUnmetConsError_J11, 
                },
            };
            data.UnauthorizedConsumptionSheet = new UnauthorizedConsumptionSheet(data)
            {
                OthersErrorMargin_F18_F22 = new List<double>
                {
                    easyCalcDataInput.UnauthCons_OthersErrorMargin_F18, 
                    easyCalcDataInput.UnauthCons_OthersErrorMargin_F19, 
                    easyCalcDataInput.UnauthCons_OthersErrorMargin_F20, 
                    easyCalcDataInput.UnauthCons_OthersErrorMargin_F21, 
                },
                OthersM3PerDay_J18_J22 = new List<double>
                {
                    easyCalcDataInput.UnauthCons_OthersM3PerDay_J18, 
                    easyCalcDataInput.UnauthCons_OthersM3PerDay_J19, 
                    easyCalcDataInput.UnauthCons_OthersM3PerDay_J20, 
                    easyCalcDataInput.UnauthCons_OthersM3PerDay_J21, 
                },
                IllegalConnectionsDomesticEstimatedNumber_D6 = easyCalcDataInput.UnauthCons_IllegalConnDomEstNo_D6,
                IllegalConnectionsDomesticPersonsPerHouse_H6 = easyCalcDataInput.UnauthCons_IllegalConnDomPersPerHouse_H6,
                IllegalConnectionsDomesticConsumptionLitersPerPersonPerDay_J6 = easyCalcDataInput.UnauthCons_IllegalConnDomConsLitPerPersDay_J6,
                IllegalConnectionsDomesticErrorMargin_F6 = easyCalcDataInput.UnauthCons_IllegalConnDomErrorMargin_F6,

                IllegalConnectionsOthersErrorMargin_F10 = easyCalcDataInput.UnauthCons_IllegalConnOthersErrorMargin_F10,

                IllegalConnectionsOthersEstimatedNumber_D10 = easyCalcDataInput.IllegalConnectionsOthersEstimatedNumber_D10,
                IllegalConnectionsOthersConsumptionLitersPerConnectionPerDay_J10 = easyCalcDataInput.IllegalConnectionsOthersConsumptionLitersPerConnectionPerDay_J10,

                MeterTamperingBypassesEtcEstimatedNumber_D14 = easyCalcDataInput.UnauthCons_MeterTampBypEtcEstNo_D14,
                MeterTamperingBypassesEtcErrorMargin_F14 = easyCalcDataInput.UnauthCons_MeterTampBypEtcErrorMargin_F14,
                MeterTamperingBypassesEtcConsumptionLitersPerCustomerPerDay_J14 = easyCalcDataInput.UnauthCons_MeterTampBypEtcConsLitPerCustDay_J14,



            };
            data.MeterErrorsSheet = new MeterErrorsSheet(data)
            {
                DetailedManualSpec_J6 = easyCalcDataInput.MetErrors_DetailedManualSpec_J6,
                BilledMeteredConsumptionWithoutBulkSupplyMeterUnderregistration_H8 = easyCalcDataInput.MetErrors_BilledMetConsWoBulkSupMetUndrreg_H8,
                BilledMeteredConsumptionWithoutBulkSupplyErrorMargin_N8 = easyCalcDataInput.MetErrors_BilledMetConsWoBulkSupErrorMargin_N8,

                BilledMeteredConsumptionManuallyEnteredM3_F12_F28 = new List<double>
                {
                    easyCalcDataInput.MetErrors_Total_F12,
                    easyCalcDataInput.MetErrors_Total_F13,
                    easyCalcDataInput.MetErrors_Total_F14,
                    easyCalcDataInput.MetErrors_Total_F15,
                },
                BilledMeteredConsumptionManuallyEnteredMeterUnderregistration_H12_H28 = new List<double>
                {
                    easyCalcDataInput.MetErrors_Meter_H12,
                    easyCalcDataInput.MetErrors_Meter_H13,
                    easyCalcDataInput.MetErrors_Meter_H14,
                    easyCalcDataInput.MetErrors_Meter_H15,
                },
                BilledMeteredConsumptionManuallyEnteredErrorMargin_N12_N28 = new List<double>
                {
                    easyCalcDataInput.MetErrors_Error_N12,
                    easyCalcDataInput.MetErrors_Error_N13,
                    easyCalcDataInput.MetErrors_Error_N14,
                    easyCalcDataInput.MetErrors_Error_N15,
                },

                MeteredBulkSupplyExportErrorMargin_N32 = easyCalcDataInput.MeteredBulkSupplyExportErrorMargin_N32,
                UnbilledMeteredConsumptionWithoutBulkSupplyErrorMargin_N34 = easyCalcDataInput.UnbilledMeteredConsumptionWithoutBulkSupplyErrorMargin_N34,
                CorruptMeterReadingPracticessErrorMargin_N38 = easyCalcDataInput.CorruptMeterReadingPracticessErrorMargin_N38,
                DataHandlingErrorsOffice_L40 = easyCalcDataInput.DataHandlingErrorsOffice_L40,
                DataHandlingErrorsOfficeErrorMargin_N40 = easyCalcDataInput.DataHandlingErrorsOfficeErrorMargin_N40,

                MeteredBulkSupplyExportMetereUnderregistration_H32 = easyCalcDataInput.MetErrors_MetBulkSupExpMetUnderreg_H32,
                UnbilledMeteredConsumptionWithoutBulkSupplyMeterUnderregistration_H34 = easyCalcDataInput.MetErrors_UnbillMetConsWoBulkSupplMetUndrreg_H34,
                CorruptMeterReadingPracticesMeterUnderregistration_H38 = easyCalcDataInput.MetErrors_CorruptMetReadPractMetUndrreg_H38,
            };

            data.NetworkSheet = new NetworkSheet(data)
            {
                DistributionAndTransmissionMainsEntries_D7_D26 = new List<double> 
                { 
                    easyCalcDataInput.Network_DistributionAndTransmissionMains_D7, 
                    easyCalcDataInput.Network_DistributionAndTransmissionMains_D8, 
                    easyCalcDataInput.Network_DistributionAndTransmissionMains_D9, 
                    easyCalcDataInput.Network_DistributionAndTransmissionMains_D10, 
                },
                NumberOfConnectionsOfRegsteredCustomers_H10 = easyCalcDataInput.Network_NoOfConnOfRegCustomers_H10,
                NumberOfInactiveAccountsWServiceConnections_H18 = easyCalcDataInput.Network_NoOfInactAccountsWSvcConns_H18,
                AvgLenOfServiceConnectionFromBoundaryToMeterM_H32 = easyCalcDataInput.Network_AvgLenOfSvcConnFromBoundaryToMeterM_H32,

                DistributionAndTransmissionMainsPossibleUnderestimation_D30 = easyCalcDataInput.Network_PossibleUnd_D30,
                Network_NoCustomers_H7 = easyCalcDataInput.Network_NoCustomers_H7,
                Network_ErrorMargin_J7 = easyCalcDataInput.Network_ErrorMargin_J7,
                Network_ErrorMargin_J10 = easyCalcDataInput.Network_ErrorMargin_J10,
                Network_ErrorMargin_J18 = easyCalcDataInput.Network_ErrorMargin_J18,
                Network_ErrorMargin_J32 = easyCalcDataInput.Network_ErrorMargin_J32,
                //Network_ErrorMargin_D35 = easyCalcDataInput.Network_ErrorMargin_D35,
            };
            data.PressureSheet = new PressureSheet()
            {
                ApproximateNumberOfConnections_D7_D24 = new List<double> 
                { 
                    easyCalcDataInput.Prs_ApproxNoOfConn_D7, 
                    easyCalcDataInput.Prs_ApproxNoOfConn_D8, 
                    easyCalcDataInput.Prs_ApproxNoOfConn_D9, 
                    easyCalcDataInput.Prs_ApproxNoOfConn_D10, 
                },
                DailyAveragePressureM_F7_F24 = new List<double> 
                { 
                    easyCalcDataInput.Prs_DailyAvgPrsM_F7, 
                    easyCalcDataInput.Prs_DailyAvgPrsM_F8, 
                    easyCalcDataInput.Prs_DailyAvgPrsM_F9, 
                    easyCalcDataInput.Prs_DailyAvgPrsM_F10, 
                },
                Prs_ErrorMarg_F26 = easyCalcDataInput.Prs_ErrorMarg_F26,
            };
            data.IntermittentSupply = new IntermittentSupplySheet() 
            { 
                Interm_Conn_D7_24_List = new List<double> 
                {
                    easyCalcDataInput.Interm_Conn_D7,
                    easyCalcDataInput.Interm_Conn_D8,
                    easyCalcDataInput.Interm_Conn_D9,
                    easyCalcDataInput.Interm_Conn_D10,
                },
                Interm_Days_F7_24_List = new List<double> 
                { 
                    easyCalcDataInput.Interm_Days_F7,
                    easyCalcDataInput.Interm_Days_F8,
                    easyCalcDataInput.Interm_Days_F9,
                    easyCalcDataInput.Interm_Days_F10,
                },
                Interm_Hour_H7_24_List = new List<double> 
                { 
                    easyCalcDataInput.Interm_Hour_H7,
                    easyCalcDataInput.Interm_Hour_H8,
                    easyCalcDataInput.Interm_Hour_H9,
                    easyCalcDataInput.Interm_Hour_H10,
                },
                ErrorMargin_H26 = easyCalcDataInput.Interm_ErrorMarg_H26,
            };

            data.PiSheet = new PiSheet(data);
            data.WaterBalanceSheet = new WaterBalanceSheet();

            return data;
        }

        // Only in test
        public EasyCalcSheetData ReadSheetData(string zone, DateTime yearMonth)
        {
            var data = new EasyCalcSheetData();
            data.StartSheet = new StartSheet
            {
                PeriodDays_M21 = 30,
            };
            data.SystemInputSheet = new SystemInputSheet
            {
                SystemInputVolumeM3_D6_D70 = new List<double> { 6593339 },
                SystemInputVolumeError_F6_F70 = new List<double> { 0.05 }
            };
            data.BilledConsumptionSheet = new BilledConsumptionSheet
            {
                BilledMeteredConsumptionBulkWaterSupplyExportM3_D6 = 5332026,
                BulledUnmeteredConsumptionBulkWaterSupplyExportM3_H6 = 1000,
            };
            data.UnbilledConsumptionSheet = new UnbilledConsumptionSheet
            {
                MeteredConsumptionBulkWaterSupplyExportM3_D6 = 309349,
            };
            data.UnauthorizedConsumptionSheet = new UnauthorizedConsumptionSheet(data)
            {
                IllegalConnectionsDomesticEstimatedNumber_D6 = 100,
                IllegalConnectionsDomesticPersonsPerHouse_H6 = 3,
                IllegalConnectionsDomesticConsumptionLitersPerPersonPerDay_J6 = 120,
                IllegalConnectionsDomesticErrorMargin_F6 = 0.05,
                IllegalConnectionsOthersErrorMargin_F10 = 0.0,
                MeterTamperingBypassesEtcEstimatedNumber_D14 = 1000,
                MeterTamperingBypassesEtcErrorMargin_F14 = 0.10,
                MeterTamperingBypassesEtcConsumptionLitersPerCustomerPerDay_J14 = 160,
            };
            data.MeterErrorsSheet = new MeterErrorsSheet(data)
            {
                DetailedManualSpec_J6 = false,
                BilledMeteredConsumptionWithoutBulkSupplyMeterUnderregistration_H8 = 0.03,
                BilledMeteredConsumptionWithoutBulkSupplyErrorMargin_N8 = 0.02,
                MeteredBulkSupplyExportMetereUnderregistration_H32 = 0.03,
                UnbilledMeteredConsumptionWithoutBulkSupplyMeterUnderregistration_H34 = 0.03,
                CorruptMeterReadingPracticesMeterUnderregistration_H38 = 0.03,
            };

            data.NetworkSheet = new NetworkSheet(data)
            {
                DistributionAndTransmissionMainsEntries_D7_D26 = new List<double> { 260.0 },
                NumberOfConnectionsOfRegsteredCustomers_H10 = 8000,
                NumberOfInactiveAccountsWServiceConnections_H18 = 1500,
                AvgLenOfServiceConnectionFromBoundaryToMeterM_H32 = 7,
            };
            data.PressureSheet = new PressureSheet()
            {
                ApproximateNumberOfConnections_D7_D24 = new List<double> { 9500, },
                DailyAveragePressureM_F7_F24 = new List<double> { 30 },
            };

            data.IntermittentSupply = new IntermittentSupplySheet();

            data.PiSheet = new PiSheet(data);
            data.WaterBalanceSheet = new WaterBalanceSheet();

            return data;
        }
    }
}
