using System;
using System.Collections.Generic;
using WbEasyCalcModel;
using WbEasyCalcModel.WbEasyCalc;
using WbEasyCalcRepository.Model;

namespace WbEasyCalcRepository
{
    public class WbEasyCalc : IWbEasyCalc
    {
        public EasyCalcDataOutput Calculate(EasyCalcDataInput easyCalcDataInput)
        {
            EasyCalcSheetData easyCalcSheetData = ReadSheetData(easyCalcDataInput);

            EasyCalcDataOutput easyCalcDataOutput = new EasyCalcDataOutput()
            {
                WaterBalanceDay = GetWaterBalanceSheet(easyCalcSheetData.WaterBalanceDaySheet),
                WaterBalancePeriod = GetWaterBalanceSheet(easyCalcSheetData.WaterBalanceSheet),
                WaterBalanceYear = GetWaterBalanceSheet(easyCalcSheetData.WaterBalanceYearSheet),

                Pis = GetPisSheet(easyCalcSheetData.PiSheet),




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
                UnauthCons_Total_L18 = easyCalcSheetData.UnauthorizedConsumptionSheet.Total_L18_L22[0],
                UnauthCons_Total_L19 = easyCalcSheetData.UnauthorizedConsumptionSheet.Total_L18_L22[1],
                UnauthCons_Total_L20 = easyCalcSheetData.UnauthorizedConsumptionSheet.Total_L18_L22[2],
                UnauthCons_Total_L21 = easyCalcSheetData.UnauthorizedConsumptionSheet.Total_L18_L22[3],

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
                Network_Min_H26 = easyCalcSheetData.NetworkSheet.Network_Min_H26,
                Network_Max_H28 = easyCalcSheetData.NetworkSheet.Network_Max_H28,
                Network_BestEstimate_H30 = easyCalcSheetData.NetworkSheet.ServiceConnectionsBestEstimate_H30,
                Network_Number_H39 = easyCalcSheetData.NetworkSheet.LenOfServConnFromBoundToMeterKm_H39,
                Network_ErrorMarg_J39 = easyCalcSheetData.NetworkSheet.Network_ErrorMarg_J39,

                Prs_Min_F29 = easyCalcSheetData.PressureSheet.Prs_Min_F29,
                Prs_Max_F31 = easyCalcSheetData.PressureSheet.Prs_Max_F31,
                Prs_BestEstimate_F33 = easyCalcSheetData.PressureSheet.AveragePressureBestEstimate_F33,

                Interm_BestEstimate_H33 = easyCalcSheetData.IntermittentSupply.SupplyTimeBestEstimate_H33,
                Interm_Min_H29 = easyCalcSheetData.IntermittentSupply.Interm_Min_F29,
                Interm_Max_H31 = easyCalcSheetData.IntermittentSupply.Interm_Max_F31,


                FinancData_G13 = easyCalcSheetData.FinancialDataSheet.FinancData_G13,
                FinancData_G15 = easyCalcSheetData.FinancialDataSheet.FinancData_G15,
                FinancData_G17 = easyCalcSheetData.FinancialDataSheet.FinancData_G17,
                FinancData_G19 = easyCalcSheetData.FinancialDataSheet.FinancData_G19,
                FinancData_G20 = easyCalcSheetData.FinancialDataSheet.FinancData_G20,
                FinancData_G22 = easyCalcSheetData.FinancialDataSheet.FinancData_G22,
                FinancData_D24 = easyCalcSheetData.FinancialDataSheet.FinancData_D24,
                FinancData_G31 = easyCalcSheetData.FinancialDataSheet.FinancData_G31,
                FinancData_K8  = easyCalcSheetData.FinancialDataSheet.FinancData_K8 ,
                FinancData_K13 = easyCalcSheetData.FinancialDataSheet.FinancData_K13,
                FinancData_K15 = easyCalcSheetData.FinancialDataSheet.FinancData_K15,
                FinancData_K17 = easyCalcSheetData.FinancialDataSheet.FinancData_K17,
                FinancData_K19 = easyCalcSheetData.FinancialDataSheet.FinancData_K19,
                FinancData_K20 = easyCalcSheetData.FinancialDataSheet.FinancData_K20,
                FinancData_K22 = easyCalcSheetData.FinancialDataSheet.FinancData_K22,
                FinancData_K31 = easyCalcSheetData.FinancialDataSheet.FinancData_K31,
                FinancData_K35 = easyCalcSheetData.FinancialDataSheet.FinancData_K35,


            };

            return easyCalcDataOutput;
        }

        private PisModel GetPisSheet(PiSheet pisSheet)
        {
            PisModel model = new PisModel
            {
                Pis_F9 =  pisSheet.Pis_F9,
                Pis_H9 =  pisSheet.Pis_H9,
                Pis_J9 =  pisSheet.Pis_J9,
                Pis_L9 =  pisSheet.Pis_L9,
                Pis_F11 = pisSheet.Pis_F11,
                Pis_H11 = pisSheet.Pis_H11,
                Pis_J11 = pisSheet.Pis_J11,
                Pis_L11 = pisSheet.Pis_L11,
                Pis_F17 = pisSheet.Pis_F17,
                Pis_H17 = pisSheet.Pis_H17,
                Pis_J17 = pisSheet.Pis_J17,
                Pis_L17 = pisSheet.Pis_L17,
                Pis_F19 = pisSheet.Pis_F19,
                Pis_H19 = pisSheet.Pis_H19,
                Pis_J19 = pisSheet.Pis_J19,
                Pis_L19 = pisSheet.Pis_L19,
                Pis_F25 = pisSheet.Pis_F25,
                Pis_H25 = pisSheet.Pis_H25,
                Pis_J25 = pisSheet.Pis_J25,
                Pis_L25 = pisSheet.Pis_L25,
                Pis_F27 = pisSheet.Pis_F27,
                Pis_H27 = pisSheet.Pis_H27,
                Pis_J27 = pisSheet.Pis_J27,
                Pis_L27 = pisSheet.Pis_L27,
                Pis_F29 = pisSheet.Pis_F29,
                Pis_H29 = pisSheet.Pis_H29,
                Pis_J29 = pisSheet.Pis_J29,
                Pis_L29 = pisSheet.Pis_L29,
                Pis_F31 = pisSheet.Pis_F31,
                Pis_H31 = pisSheet.Pis_H31,
                Pis_J31 = pisSheet.Pis_J31,
                Pis_L31 = pisSheet.Pis_L31,
                Pis_F37 = pisSheet.Pis_F37,
                Pis_H37 = pisSheet.Pis_H37,
                Pis_J37 = pisSheet.Pis_J37,
                Pis_L37 = pisSheet.Pis_L37,
                Pis_F39 = pisSheet.Pis_F39,
                Pis_H39 = pisSheet.Pis_H39,
                Pis_J39 = pisSheet.Pis_J39,
                Pis_L39 = pisSheet.Pis_L39,
                Pis_F41 = pisSheet.Pis_F41,
                Pis_H41 = pisSheet.Pis_H41,
                Pis_J41 = pisSheet.Pis_J41,
                Pis_L41 = pisSheet.Pis_L41,
                Pis_F47 = pisSheet.Pis_F47,
                Pis_H47 = pisSheet.Pis_H47,
                Pis_J47 = pisSheet.Pis_J47,
                Pis_L47 = pisSheet.Pis_L47,
                Pis_F49 = pisSheet.Pis_F49,
                Pis_H49 = pisSheet.Pis_H49,
                Pis_J49 = pisSheet.Pis_J49,
                Pis_L49 = pisSheet.Pis_L49,
                Pis_F51 = pisSheet.Pis_F51,
                Pis_H51 = pisSheet.Pis_H51,
                Pis_J51 = pisSheet.Pis_J51,
                Pis_L51 = pisSheet.Pis_L51,

                Pis_N27 = pisSheet.Pis_N27,
                Pis_P27 = pisSheet.Pis_P27,
                Pis_N47 = pisSheet.Pis_N47,
                Pis_P47 = pisSheet.Pis_P47,

            };
            return model;
        }

        private WaterBalanceModel GetWaterBalanceSheet(WaterBalanceSheet sheet)
        {
            WaterBalanceModel model = new WaterBalanceModel
            {
                SystemInputVolume_B19 = sheet.SystemInputVolume_B19,
                SystemInputVolumeErrorMargin_B21 = sheet.SystemInputVolumeErrorMargin_B21,

                AuthorizedConsumption_K12 = sheet.AuthorizedConsumption_K12,
                AuthorizedConsumptionErrorMargin_K15 = sheet.AuthorizedConsumptionErrorMargin_K15,
                WaterLosses_K29 = sheet.WaterLosses_K29,
                WaterLossesErrorMargin_K31 = sheet.WaterLossesErrorMargin_K31,

                BilledAuthorizedConsumption_T8 = sheet.BilledAuthorizedConsumption_T8,
                UnbilledAuthorizedConsumption_T16 = sheet.UnbilledAuthorizedConsumption_T16,
                UnbilledAuthorizedConsumptionErrorMargin_T20 = sheet.UnbilledAuthorizedConsumptionErrorMargin_T20,
                CommercialLosses_T26 = sheet.CommercialLosses_T26,
                CommercialLossesErrorMargin_T29 = sheet.CommercialLossesErrorMargin_T29,
                PhysicalLossesM3_T34 = sheet.PhysicalLossesM3_T34,
                PhyscialLossesErrorMargin_AH35 = sheet.PhyscialLossesErrorMargin_AH35,

                BilledMeteredConsumption_AC4 = sheet.BilledMeteredConsumption_AC4,
                BilledUnmeteredConsumption_AC9 = sheet.BilledUnmeteredConsumption_AC9,
                UnbilledMeteredConsumption_AC14 = sheet.UnbilledMeteredConsumption_AC14,

                UnbilledUnmeteredConsumption_AC19 = sheet.UnbilledUnmeteredConsumption_AC19,
                UnbilledUnmeteredConsumptionErrorMargin_AO20 = sheet.UnbilledUnmeteredConsumptionErrorMargin_AO20,
                UnauthorizedConsumption_AC24 = sheet.UnauthorizedConsumption_AC24,
                UnauthorizedConsumptionErrorMargin_AO25 = sheet.UnauthorizedConsumptionErrorMargin_AO25,
                CustomerMeterInaccuraciesAndErrorsM3_AC29 = sheet.CustomerMeterInaccuraciesAndErrorsM3_AC29,
                CustomerMeterInaccuraciesAndErrorsErrorMargin_AO30 = sheet.CustomerMeterInaccuraciesAndErrorsErrorMargin_AO30,

                RevenueWaterM3_AY8 = sheet.RevenueWaterM3_AY8,
                NonRevenueWaterM3_AY24 = sheet.NonRevenueWaterM3_AY24,
                NonRevenueWaterErrorMargin_AY26 = sheet.NonRevenueWaterErrorMargin_AY26,

            };
            return model;
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

            data.FinancialDataSheet = new FinancialDataSheet(data)
            {
                FinancData_G6 = easyCalcDataInput.FinancData_G6,
                FinancData_K6 = easyCalcDataInput.FinancData_K6,
                FinancData_G8 = easyCalcDataInput.FinancData_G8,
                FinancData_D26 = easyCalcDataInput.FinancData_D26,
                FinancData_G35 = easyCalcDataInput.FinancData_G35,
            };

            data.WaterBalanceSheet = new WaterBalanceSheet(data);
            data.WaterBalanceDaySheet = new WaterBalanceDaySheet(data);
            data.WaterBalanceYearSheet = new WaterBalanceYearSheet(data);

            data.PiSheet = new PiSheet(data);

            return data;
        }
    }
}
