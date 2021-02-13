﻿using System;
using WbEasyCalcModel;
using WbEasyCalcModel.WbEasyCalc;

namespace DataModel
{
    public class WbEasyCalcData : ICloneable
    {
        public int WbEasyCalcDataId { get; set; }

        public string CreateLogin { get; set; }
        public DateTime CreateDate { get; set; }
        public string ModifyLogin { get; set; }
        public DateTime ModifyDate { get; set; }

        public int YearNo { get; set; }
        public int MonthNo { get; set; }
        public int ZoneId { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool IsArchive { get; set; }
        public bool IsAccepted { get; set; }


        public EasyCalcModel EasyCalcModel { get; set; }



        // input
        //public int Start_PeriodDays_M21 { get; set; } 

        // SysInput
        public string SysInput_Desc_B6 { get; set; }
        public string SysInput_Desc_B7 { get; set; }
        public string SysInput_Desc_B8 { get; set; }
        public string SysInput_Desc_B9 { get; set; }
        public double SysInput_SystemInputVolumeM3_D6 { get; set; }
        public double SysInput_SystemInputVolumeError_F6 { get; set; }
        public double SysInput_SystemInputVolumeM3_D7 { get; set; }
        public double SysInput_SystemInputVolumeError_F7 { get; set; }
        public double SysInput_SystemInputVolumeM3_D8 { get; set; }
        public double SysInput_SystemInputVolumeError_F8 { get; set; }
        public double SysInput_SystemInputVolumeM3_D9 { get; set; }
        public double SysInput_SystemInputVolumeError_F9 { get; set; }


        public double BilledCons_BilledMetConsBulkWatSupExpM3_D6 { get; set; }
        public double BilledCons_BilledUnmetConsBulkWatSupExpM3_H6 { get; set; }


        public string BilledCons_Desc_B8 { get; set; }
        public string BilledCons_Desc_B9 { get; set; }
        public string BilledCons_Desc_B10 { get; set; }
        public string BilledCons_Desc_B11 { get; set; }
        public string BilledCons_Desc_F8 { get; set; }
        public string BilledCons_Desc_F9 { get; set; }
        public string BilledCons_Desc_F10 { get; set; }
        public string BilledCons_Desc_F11 { get; set; }

        public string UnbilledCons_Desc_D8 { get; set; }
        public string UnbilledCons_Desc_D9 { get; set; }
        public string UnbilledCons_Desc_D10 { get; set; }
        public string UnbilledCons_Desc_D11 { get; set; }
        public string UnbilledCons_Desc_F6 { get; set; }
        public string UnbilledCons_Desc_F7 { get; set; }
        public string UnbilledCons_Desc_F8 { get; set; }
        public string UnbilledCons_Desc_F9 { get; set; }
        public string UnbilledCons_Desc_F10 { get; set; }
        public string UnbilledCons_Desc_F11 { get; set; }
        
        public string UnauthCons_Desc_B18 { get; set; }
        public string UnauthCons_Desc_B19 { get; set; }
        public string UnauthCons_Desc_B20 { get; set; }
        public string UnauthCons_Desc_B21 { get; set; }
        
        public string MetErrors_Desc_D12 { get; set; }
        public string MetErrors_Desc_D13 { get; set; }
        public string MetErrors_Desc_D14 { get; set; }
        public string MetErrors_Desc_D15 { get; set; }
        
        public string Network_Desc_B7 { get; set; }
        public string Network_Desc_B8 { get; set; }
        public string Network_Desc_B9 { get; set; }
        public string Network_Desc_B10 { get; set; }
        
        public string Interm_Area_B7 { get; set; }
        public string Interm_Area_B8 { get; set; }
        public string Interm_Area_B9 { get; set; }
        public string Interm_Area_B10 { get; set; }
    

        public double BilledCons_UnbMetConsM3_D8 { get; set; }
        public double BilledCons_UnbMetConsM3_D9 { get; set; }
        public double BilledCons_UnbMetConsM3_D10 { get; set; }
        public double BilledCons_UnbMetConsM3_D11 { get; set; }
        public double BilledCons_UnbUnmetConsM3_H8 { get; set; }
        public double BilledCons_UnbUnmetConsM3_H9 { get; set; }
        public double BilledCons_UnbUnmetConsM3_H10 { get; set; }
        public double BilledCons_UnbUnmetConsM3_H11 { get; set; }


        public double UnbilledCons_MetConsBulkWatSupExpM3_D6 { get; set; }

        public double UnbilledCons_UnbMetConsM3_D8 { get; set; }
        public double UnbilledCons_UnbMetConsM3_D9 { get; set; }
        public double UnbilledCons_UnbMetConsM3_D10 { get; set; }
        public double UnbilledCons_UnbMetConsM3_D11 { get; set; }
        public double UnbilledCons_UnbUnmetConsM3_H6 { get; set; }
        public double UnbilledCons_UnbUnmetConsM3_H7 { get; set; }
        public double UnbilledCons_UnbUnmetConsM3_H8 { get; set; }
        public double UnbilledCons_UnbUnmetConsM3_H9 { get; set; }
        public double UnbilledCons_UnbUnmetConsM3_H10 { get; set; }
        public double UnbilledCons_UnbUnmetConsM3_H11 { get; set; }
        public double UnbilledCons_UnbUnmetConsError_J6 { get; set; }
        public double UnbilledCons_UnbUnmetConsError_J7 { get; set; }
        public double UnbilledCons_UnbUnmetConsError_J8 { get; set; }
        public double UnbilledCons_UnbUnmetConsError_J9 { get; set; }
        public double UnbilledCons_UnbUnmetConsError_J10 { get; set; }
        public double UnbilledCons_UnbUnmetConsError_J11 { get; set; }


        public int UnauthCons_IllegalConnDomEstNo_D6 { get; set; }                      //        
        public double UnauthCons_IllegalConnDomPersPerHouse_H6 { get; set; }
        public double UnauthCons_IllegalConnDomConsLitPerPersDay_J6 { get; set; }
        public double UnauthCons_IllegalConnDomErrorMargin_F6 { get; set; }
        public double UnauthCons_IllegalConnOthersErrorMargin_F10 { get; set; }
        public double IllegalConnectionsOthersEstimatedNumber_D10 { get; set; }
        public double IllegalConnectionsOthersConsumptionLitersPerConnectionPerDay_J10 { get; set; }
        public double UnauthCons_MeterTampBypEtcEstNo_D14 { get; set; }
        public double UnauthCons_MeterTampBypEtcErrorMargin_F14 { get; set; }
        public double UnauthCons_MeterTampBypEtcConsLitPerCustDay_J14 { get; set; }

        public double UnauthCons_OthersErrorMargin_F18 { get; set; }
        public double UnauthCons_OthersErrorMargin_F19 { get; set; }
        public double UnauthCons_OthersErrorMargin_F20 { get; set; }
        public double UnauthCons_OthersErrorMargin_F21 { get; set; }
        public double UnauthCons_OthersM3PerDay_J18 { get; set; }
        public double UnauthCons_OthersM3PerDay_J19 { get; set; }
        public double UnauthCons_OthersM3PerDay_J20 { get; set; }
        public double UnauthCons_OthersM3PerDay_J21 { get; set; }


        public int MetErrors_DetailedManualSpec_J6 { get; set; }                     //
        public double MetErrors_BilledMetConsWoBulkSupMetUndrreg_H8 { get; set; }
        public double MetErrors_BilledMetConsWoBulkSupErrorMargin_N8 { get; set; }

        public double MetErrors_Total_F12 { get; set; }
        public double MetErrors_Total_F13 { get; set; }
        public double MetErrors_Total_F14 { get; set; }
        public double MetErrors_Total_F15 { get; set; }

        public double MetErrors_Meter_H12 { get; set; }
        public double MetErrors_Meter_H13 { get; set; }
        public double MetErrors_Meter_H14 { get; set; }
        public double MetErrors_Meter_H15 { get; set; }

        public double MetErrors_Error_N12 { get; set; }
        public double MetErrors_Error_N13 { get; set; }
        public double MetErrors_Error_N14 { get; set; }
        public double MetErrors_Error_N15 { get; set; }

        public double MeteredBulkSupplyExportErrorMargin_N32 { get; set; }
        public double UnbilledMeteredConsumptionWithoutBulkSupplyErrorMargin_N34 { get; set; }
        public double CorruptMeterReadingPracticessErrorMargin_N38 { get; set; }
        public double DataHandlingErrorsOffice_L40 { get; set; }
        public double DataHandlingErrorsOfficeErrorMargin_N40 { get; set; }


        public double MetErrors_MetBulkSupExpMetUnderreg_H32 { get; set; }
        public double MetErrors_UnbillMetConsWoBulkSupplMetUndrreg_H34 { get; set; }
        public double MetErrors_CorruptMetReadPractMetUndrreg_H38 { get; set; }



        public double Network_DistributionAndTransmissionMains_D7 { get; set; }
        public double Network_DistributionAndTransmissionMains_D8 { get; set; }
        public double Network_DistributionAndTransmissionMains_D9 { get; set; }
        public double Network_DistributionAndTransmissionMains_D10 { get; set; }

        public double Network_NoOfConnOfRegCustomers_H10 { get; set; }
        public double Network_NoOfInactAccountsWSvcConns_H18 { get; set; }
        public double Network_AvgLenOfSvcConnFromBoundaryToMeterM_H32 { get; set; }

        public double Network_PossibleUnd_D30 { get; set; }
        public double Network_NoCustomers_H7 { get; set; }
        public double Network_ErrorMargin_J7 { get; set; }
        public double Network_ErrorMargin_J10 { get; set; }
        public double Network_ErrorMargin_J18 { get; set; }
        public double Network_ErrorMargin_J32 { get; set; }
        public double Network_ErrorMargin_D35 { get; set; }

        public double Network_Total_D28 { get; set; }
        public double Network_Total_D32 { get; set; }
        public double Network_Min_D39 { get; set; }
        public double Network_Max_D41 { get; set; }
        public double Network_BestEstimate_D43 { get; set; }
        public double Network_Number_H21 { get; set; }
        public double Network_ErrorMarg_J21 { get; set; }
        public double Network_ErrorMarg_J24 { get; set; }
        public double Network_Min_H26 { get; set; }
        public double Network_Max_H28 { get; set; }
        public double Network_BestEstimate_H30 { get; set; }
        public double Network_Number_H39 { get; set; }
        public double Network_ErrorMarg_J39 { get; set; }

        public double FinancData_G6 { get; set; }
        public string FinancData_K6 { get; set; }
        public double FinancData_G8 { get; set; }
        public double FinancData_D26 { get; set; }
        public double FinancData_G35 { get; set; }


        public string Prs_Area_B7 { get; set; }
        public double Prs_ApproxNoOfConn_D7 { get; set; }
        public double Prs_DailyAvgPrsM_F7 { get; set; }
        public string Prs_Area_B8 { get; set; }
        public double Prs_ApproxNoOfConn_D8 { get; set; }
        public double Prs_DailyAvgPrsM_F8 { get; set; }
        public string Prs_Area_B9 { get; set; }
        public double Prs_ApproxNoOfConn_D9 { get; set; }
        public double Prs_DailyAvgPrsM_F9 { get; set; }
        public string Prs_Area_B10 { get; set; }
        public double Prs_ApproxNoOfConn_D10 { get; set; }
        public double Prs_DailyAvgPrsM_F10 { get; set; }
        public double Prs_ErrorMarg_F26 { get; set; }
        public double Prs_Min_F29 { get; set; }
        public double Prs_Max_F31 { get; set; }
        public double Prs_BestEstimate_F33 { get; set; }






        public double SysInput_ErrorMarg_F72 { get; set; }
        public double SysInput_Min_D75 { get; set; }
        public double SysInput_Max_D77 { get; set; }
        public double SysInput_BestEstimate_D79 { get; set; }

        public double BilledCons_Sum_D28 { get; set; }
        public double BilledCons_Sum_H28 { get; set; }
        
        public double UnbilledCons_Sum_D32 { get; set; }
        public double UnbilledCons_ErrorMarg_J25 { get; set; }
        public double UnbilledCons_Min_H28 { get; set; }
        public double UnbilledCons_Max_H30 { get; set; }
        public double UnbilledCons_BestEstimate_H32 { get; set; }

        public double UnauthCons_Total_L6 { get; set; }
        public double UnauthCons_Total_L10 { get; set; }
        public double UnauthCons_Total_L14 { get; set; }
        public double UnauthCons_ErrorMarg_F24 { get; set; }
        public double UnauthCons_Min_L27 { get; set; }
        public double UnauthCons_Max_L29 { get; set; }
        public double UnauthCons_BestEstimate_L31 { get; set; }

        public double MetErrors_Total_F8 { get; set; }
        public double MetErrors_Total_F32 { get; set; }
        public double MetErrors_Total_F34 { get; set; }
        public double MetErrors_Total_F38 { get; set; }
        public double MetErrors_Total_L8 { get; set; }
        public double MetErrors_Total_L32 { get; set; }
        public double MetErrors_Total_L34 { get; set; }
        public double MetErrors_Total_L38 { get; set; }
        public double MetErrors_ErrorMarg_N42 { get; set; }
        public double MetErrors_Min_L45 { get; set; }
        public double MetErrors_Max_L47 { get; set; }
        public double MetErrors_BestEstimate_L49 { get; set; }

        public double MetErrors_Total_L12 { get; set; }
        public double MetErrors_Total_L13 { get; set; }
        public double MetErrors_Total_L14 { get; set; }
        public double MetErrors_Total_L15 { get; set; }

        public double UnauthCons_Total_L18 { get; set; }
        public double UnauthCons_Total_L19 { get; set; }
        public double UnauthCons_Total_L20 { get; set; }
        public double UnauthCons_Total_L21 { get; set; }

        public double Interm_ErrorMarg_H26 { get; set; }
        public double Interm_Min_H29 { get; set; }
        public double Interm_Max_H31 { get; set; }
        public double Interm_BestEstimate_H33 { get; set; }




        public double Interm_Conn_D7 { get; set; }
        public double Interm_Conn_D8 { get; set; }
        public double Interm_Conn_D9 { get; set; }
        public double Interm_Conn_D10 { get; set; }
        public double Interm_Days_F7 { get; set; }
        public double Interm_Days_F8 { get; set; }
        public double Interm_Days_F9 { get; set; }
        public double Interm_Days_F10 { get; set; }
        public double Interm_Hour_H7 { get; set; }
        public double Interm_Hour_H8 { get; set; }
        public double Interm_Hour_H9 { get; set; }
        public double Interm_Hour_H10 { get; set; }


        public double FinancData_G13 { get; set; }
        public double FinancData_G15 { get; set; }
        public double FinancData_G17 { get; set; }
        public double FinancData_G19 { get; set; }
        public double FinancData_G20 { get; set; }
        public double FinancData_G22 { get; set; }
        public double FinancData_D24 { get; set; }
        public double FinancData_G31 { get; set; }
        public string FinancData_K8  { get; set; }
        public string FinancData_K13 { get; set; }
        public string FinancData_K15 { get; set; }
        public string FinancData_K17 { get; set; }
        public string FinancData_K19 { get; set; }
        public string FinancData_K20 { get; set; }
        public string FinancData_K22 { get; set; }
        public string FinancData_K31 { get; set; }
        public string FinancData_K35 { get; set; }



        public WaterBalanceModel WaterBalanceDay { get; set; } 
        public WaterBalanceModel WaterBalancePeriod { get; set; } 
        public WaterBalanceModel WaterBalanceYear { get; set; } 

        public PisModel Pis { get; set; } 

        public object Clone()
        {
            return new WbEasyCalcData()
            {
                WbEasyCalcDataId = WbEasyCalcDataId,

                CreateLogin = CreateLogin,
                CreateDate = CreateDate,
                ModifyLogin = ModifyLogin,
                ModifyDate = ModifyDate,
                Description = Description,
                IsArchive = IsArchive,
                IsAccepted = IsAccepted,

                ZoneId = ZoneId,
                YearNo = YearNo,
                MonthNo = MonthNo,

                EasyCalcModel = (EasyCalcModel)EasyCalcModel.Clone(),

                // input
                //Start_PeriodDays_M21 = Start_PeriodDays_M21,

                SysInput_Desc_B6 = SysInput_Desc_B6,
                SysInput_Desc_B7 = SysInput_Desc_B7,
                SysInput_Desc_B8 = SysInput_Desc_B8,
                SysInput_Desc_B9 = SysInput_Desc_B9,
                SysInput_SystemInputVolumeM3_D6 = SysInput_SystemInputVolumeM3_D6,
                SysInput_SystemInputVolumeError_F6 = SysInput_SystemInputVolumeError_F6,
                SysInput_SystemInputVolumeM3_D7 = SysInput_SystemInputVolumeM3_D7,
                SysInput_SystemInputVolumeError_F7 = SysInput_SystemInputVolumeError_F7,
                SysInput_SystemInputVolumeM3_D8 = SysInput_SystemInputVolumeM3_D8,
                SysInput_SystemInputVolumeError_F8 = SysInput_SystemInputVolumeError_F8,
                SysInput_SystemInputVolumeM3_D9 = SysInput_SystemInputVolumeM3_D9,
                SysInput_SystemInputVolumeError_F9 = SysInput_SystemInputVolumeError_F9,

                BilledCons_Desc_B8 = BilledCons_Desc_B8,
                BilledCons_Desc_B9 = BilledCons_Desc_B9,
                BilledCons_Desc_B10 = BilledCons_Desc_B10,
                BilledCons_Desc_B11 = BilledCons_Desc_B11,
                BilledCons_Desc_F8 = BilledCons_Desc_F8,
                BilledCons_Desc_F9 = BilledCons_Desc_F9,
                BilledCons_Desc_F10 = BilledCons_Desc_F10,
                BilledCons_Desc_F11 = BilledCons_Desc_F11,
                UnbilledCons_Desc_D8 = UnbilledCons_Desc_D8,
                UnbilledCons_Desc_D9 = UnbilledCons_Desc_D9,
                UnbilledCons_Desc_D10 = UnbilledCons_Desc_D10,
                UnbilledCons_Desc_D11 = UnbilledCons_Desc_D11,
                UnbilledCons_Desc_F6 = UnbilledCons_Desc_F6,
                UnbilledCons_Desc_F7 = UnbilledCons_Desc_F7,
                UnbilledCons_Desc_F8 = UnbilledCons_Desc_F8,
                UnbilledCons_Desc_F9 = UnbilledCons_Desc_F9,
                UnbilledCons_Desc_F10 = UnbilledCons_Desc_F10,
                UnbilledCons_Desc_F11 = UnbilledCons_Desc_F11,
                UnauthCons_Desc_B18 = UnauthCons_Desc_B18,
                UnauthCons_Desc_B19 = UnauthCons_Desc_B19,
                UnauthCons_Desc_B20 = UnauthCons_Desc_B20,
                UnauthCons_Desc_B21 = UnauthCons_Desc_B21,
                MetErrors_Desc_D12 = MetErrors_Desc_D12,
                MetErrors_Desc_D13 = MetErrors_Desc_D13,
                MetErrors_Desc_D14 = MetErrors_Desc_D14,
                MetErrors_Desc_D15 = MetErrors_Desc_D15,
                Network_Desc_B7 = Network_Desc_B7,
                Network_Desc_B8 = Network_Desc_B8,
                Network_Desc_B9 = Network_Desc_B9,
                Network_Desc_B10 = Network_Desc_B10,
                Interm_Area_B7 = Interm_Area_B7,
                Interm_Area_B8 = Interm_Area_B8,
                Interm_Area_B9 = Interm_Area_B9,
                Interm_Area_B10 = Interm_Area_B10,




                BilledCons_BilledMetConsBulkWatSupExpM3_D6 = BilledCons_BilledMetConsBulkWatSupExpM3_D6,
                BilledCons_BilledUnmetConsBulkWatSupExpM3_H6 = BilledCons_BilledUnmetConsBulkWatSupExpM3_H6,

                BilledCons_UnbMetConsM3_D8 = BilledCons_UnbMetConsM3_D8,
                BilledCons_UnbMetConsM3_D9 = BilledCons_UnbMetConsM3_D9,
                BilledCons_UnbMetConsM3_D10 = BilledCons_UnbMetConsM3_D10,
                BilledCons_UnbMetConsM3_D11 = BilledCons_UnbMetConsM3_D11,
                BilledCons_UnbUnmetConsM3_H8 = BilledCons_UnbUnmetConsM3_H8,
                BilledCons_UnbUnmetConsM3_H9 = BilledCons_UnbUnmetConsM3_H9,
                BilledCons_UnbUnmetConsM3_H10 = BilledCons_UnbUnmetConsM3_H10,
                BilledCons_UnbUnmetConsM3_H11 = BilledCons_UnbUnmetConsM3_H11,

                UnbilledCons_MetConsBulkWatSupExpM3_D6 = UnbilledCons_MetConsBulkWatSupExpM3_D6,

                UnbilledCons_UnbMetConsM3_D8 = UnbilledCons_UnbMetConsM3_D8,
                UnbilledCons_UnbMetConsM3_D9 = UnbilledCons_UnbMetConsM3_D9,
                UnbilledCons_UnbMetConsM3_D10 = UnbilledCons_UnbMetConsM3_D10,
                UnbilledCons_UnbMetConsM3_D11 = UnbilledCons_UnbMetConsM3_D11,
                UnbilledCons_UnbUnmetConsM3_H6 = UnbilledCons_UnbUnmetConsM3_H6,
                UnbilledCons_UnbUnmetConsM3_H7 = UnbilledCons_UnbUnmetConsM3_H7,
                UnbilledCons_UnbUnmetConsM3_H8 = UnbilledCons_UnbUnmetConsM3_H8,
                UnbilledCons_UnbUnmetConsM3_H9 = UnbilledCons_UnbUnmetConsM3_H9,
                UnbilledCons_UnbUnmetConsM3_H10 = UnbilledCons_UnbUnmetConsM3_H10,
                UnbilledCons_UnbUnmetConsM3_H11 = UnbilledCons_UnbUnmetConsM3_H11,
                UnbilledCons_UnbUnmetConsError_J6 = UnbilledCons_UnbUnmetConsError_J6,
                UnbilledCons_UnbUnmetConsError_J7 = UnbilledCons_UnbUnmetConsError_J7,
                UnbilledCons_UnbUnmetConsError_J8 = UnbilledCons_UnbUnmetConsError_J8,
                UnbilledCons_UnbUnmetConsError_J9 = UnbilledCons_UnbUnmetConsError_J9,
                UnbilledCons_UnbUnmetConsError_J10 = UnbilledCons_UnbUnmetConsError_J10,
                UnbilledCons_UnbUnmetConsError_J11 = UnbilledCons_UnbUnmetConsError_J11,

                UnauthCons_OthersErrorMargin_F18 = UnauthCons_OthersErrorMargin_F18,
                UnauthCons_OthersErrorMargin_F19 = UnauthCons_OthersErrorMargin_F19,
                UnauthCons_OthersErrorMargin_F20 = UnauthCons_OthersErrorMargin_F20,
                UnauthCons_OthersErrorMargin_F21 = UnauthCons_OthersErrorMargin_F21,
                UnauthCons_OthersM3PerDay_J18 = UnauthCons_OthersM3PerDay_J18,
                UnauthCons_OthersM3PerDay_J19 = UnauthCons_OthersM3PerDay_J19,
                UnauthCons_OthersM3PerDay_J20 = UnauthCons_OthersM3PerDay_J20,
                UnauthCons_OthersM3PerDay_J21 = UnauthCons_OthersM3PerDay_J21,

                UnauthCons_IllegalConnDomEstNo_D6 = UnauthCons_IllegalConnDomEstNo_D6,
                UnauthCons_IllegalConnDomPersPerHouse_H6 = UnauthCons_IllegalConnDomPersPerHouse_H6,
                UnauthCons_IllegalConnDomConsLitPerPersDay_J6 = UnauthCons_IllegalConnDomConsLitPerPersDay_J6,
                UnauthCons_IllegalConnDomErrorMargin_F6 = UnauthCons_IllegalConnDomErrorMargin_F6,
                UnauthCons_IllegalConnOthersErrorMargin_F10 = UnauthCons_IllegalConnOthersErrorMargin_F10,

                IllegalConnectionsOthersEstimatedNumber_D10 = IllegalConnectionsOthersEstimatedNumber_D10,
                IllegalConnectionsOthersConsumptionLitersPerConnectionPerDay_J10 = IllegalConnectionsOthersConsumptionLitersPerConnectionPerDay_J10,

                UnauthCons_MeterTampBypEtcEstNo_D14 = UnauthCons_MeterTampBypEtcEstNo_D14,
                UnauthCons_MeterTampBypEtcErrorMargin_F14 = UnauthCons_MeterTampBypEtcErrorMargin_F14,
                UnauthCons_MeterTampBypEtcConsLitPerCustDay_J14 = UnauthCons_MeterTampBypEtcConsLitPerCustDay_J14,

                MetErrors_DetailedManualSpec_J6 = MetErrors_DetailedManualSpec_J6,
                MetErrors_BilledMetConsWoBulkSupMetUndrreg_H8 = MetErrors_BilledMetConsWoBulkSupMetUndrreg_H8,
                MetErrors_BilledMetConsWoBulkSupErrorMargin_N8 = MetErrors_BilledMetConsWoBulkSupErrorMargin_N8,

                MetErrors_Total_F12 = MetErrors_Total_F12,
                MetErrors_Total_F13 = MetErrors_Total_F13,
                MetErrors_Total_F14 = MetErrors_Total_F14,
                MetErrors_Total_F15 = MetErrors_Total_F15,
                MetErrors_Meter_H12 = MetErrors_Meter_H12,
                MetErrors_Meter_H13 = MetErrors_Meter_H13,
                MetErrors_Meter_H14 = MetErrors_Meter_H14,
                MetErrors_Meter_H15 = MetErrors_Meter_H15,
                MetErrors_Error_N12 = MetErrors_Error_N12,
                MetErrors_Error_N13 = MetErrors_Error_N13,
                MetErrors_Error_N14 = MetErrors_Error_N14,
                MetErrors_Error_N15 = MetErrors_Error_N15,

                MeteredBulkSupplyExportErrorMargin_N32 = MeteredBulkSupplyExportErrorMargin_N32,
                UnbilledMeteredConsumptionWithoutBulkSupplyErrorMargin_N34 = UnbilledMeteredConsumptionWithoutBulkSupplyErrorMargin_N34,
                CorruptMeterReadingPracticessErrorMargin_N38 = CorruptMeterReadingPracticessErrorMargin_N38,
                DataHandlingErrorsOffice_L40 = DataHandlingErrorsOffice_L40,
                DataHandlingErrorsOfficeErrorMargin_N40 = DataHandlingErrorsOfficeErrorMargin_N40,

                MetErrors_MetBulkSupExpMetUnderreg_H32 = MetErrors_MetBulkSupExpMetUnderreg_H32,
                MetErrors_UnbillMetConsWoBulkSupplMetUndrreg_H34 = MetErrors_UnbillMetConsWoBulkSupplMetUndrreg_H34,
                MetErrors_CorruptMetReadPractMetUndrreg_H38 = MetErrors_CorruptMetReadPractMetUndrreg_H38,

                Network_DistributionAndTransmissionMains_D7 = Network_DistributionAndTransmissionMains_D7,
                Network_DistributionAndTransmissionMains_D8 = Network_DistributionAndTransmissionMains_D8,
                Network_DistributionAndTransmissionMains_D9 = Network_DistributionAndTransmissionMains_D9,
                Network_DistributionAndTransmissionMains_D10 = Network_DistributionAndTransmissionMains_D10,

                Network_NoOfConnOfRegCustomers_H10 = Network_NoOfConnOfRegCustomers_H10,
                Network_NoOfInactAccountsWSvcConns_H18 = Network_NoOfInactAccountsWSvcConns_H18,
                Network_AvgLenOfSvcConnFromBoundaryToMeterM_H32 = Network_AvgLenOfSvcConnFromBoundaryToMeterM_H32,

                Network_PossibleUnd_D30 = Network_PossibleUnd_D30,
                Network_NoCustomers_H7 = Network_NoCustomers_H7,
                Network_ErrorMargin_J7 = Network_ErrorMargin_J7,
                Network_ErrorMargin_J10 = Network_ErrorMargin_J10,
                Network_ErrorMargin_J18 = Network_ErrorMargin_J18,
                Network_ErrorMargin_J32 = Network_ErrorMargin_J32,
                Network_ErrorMargin_D35 = Network_ErrorMargin_D35,

                Network_Total_D28 = Network_Total_D28,
                Network_Total_D32 = Network_Total_D32,
                Network_Min_D39 = Network_Min_D39,
                Network_Max_D41 = Network_Max_D41,
                Network_BestEstimate_D43 = Network_BestEstimate_D43,
                Network_Number_H21 = Network_Number_H21,
                Network_ErrorMarg_J21 = Network_ErrorMarg_J21,
                Network_ErrorMarg_J24 = Network_ErrorMarg_J24,
                Network_Min_H26 = Network_Min_H26,
                Network_Max_H28 = Network_Max_H28,
                Network_BestEstimate_H30 = Network_BestEstimate_H30,
                Network_Number_H39 = Network_Number_H39,
                Network_ErrorMarg_J39 = Network_ErrorMarg_J39,

                FinancData_G6 = FinancData_G6,
                FinancData_K6 = FinancData_K6,
                FinancData_G8 = FinancData_G8,
                FinancData_D26 = FinancData_D26,
                FinancData_G35 = FinancData_G35,

                Prs_Area_B7 = Prs_Area_B7,
                Prs_Area_B8 = Prs_Area_B8,
                Prs_Area_B9 = Prs_Area_B9,
                Prs_Area_B10 = Prs_Area_B10,
                Prs_ApproxNoOfConn_D7 = Prs_ApproxNoOfConn_D7,
                Prs_DailyAvgPrsM_F7 = Prs_DailyAvgPrsM_F7,
                Prs_ApproxNoOfConn_D8 = Prs_ApproxNoOfConn_D8,
                Prs_DailyAvgPrsM_F8 = Prs_DailyAvgPrsM_F8,
                Prs_ApproxNoOfConn_D9 = Prs_ApproxNoOfConn_D9,
                Prs_DailyAvgPrsM_F9 = Prs_DailyAvgPrsM_F9,
                Prs_ApproxNoOfConn_D10 = Prs_ApproxNoOfConn_D10,
                Prs_DailyAvgPrsM_F10 = Prs_DailyAvgPrsM_F10,
                Prs_ErrorMarg_F26 = Prs_ErrorMarg_F26,

                Interm_Conn_D7 = Interm_Conn_D7,
                Interm_Conn_D8 = Interm_Conn_D8,
                Interm_Conn_D9 = Interm_Conn_D9,
                Interm_Conn_D10 = Interm_Conn_D10,
                Interm_Days_F7 = Interm_Days_F7,
                Interm_Days_F8 = Interm_Days_F8,
                Interm_Days_F9 = Interm_Days_F9,
                Interm_Days_F10 = Interm_Days_F10,
                Interm_Hour_H7 = Interm_Hour_H7,
                Interm_Hour_H8 = Interm_Hour_H8,
                Interm_Hour_H9 = Interm_Hour_H9,
                Interm_Hour_H10 = Interm_Hour_H10,
                Interm_ErrorMarg_H26 = Interm_ErrorMarg_H26,


                Prs_Min_F29 = Prs_Min_F29,
                Prs_Max_F31 = Prs_Max_F31,
                Prs_BestEstimate_F33 = Prs_BestEstimate_F33,

                SysInput_ErrorMarg_F72 = SysInput_ErrorMarg_F72,
                SysInput_Min_D75 = SysInput_Min_D75,
                SysInput_Max_D77 = SysInput_Max_D77,
                SysInput_BestEstimate_D79 = SysInput_BestEstimate_D79,

                BilledCons_Sum_D28 = BilledCons_Sum_D28,
                BilledCons_Sum_H28 = BilledCons_Sum_H28,
                UnbilledCons_Sum_D32 = UnbilledCons_Sum_D32,
                UnbilledCons_ErrorMarg_J25 = UnbilledCons_ErrorMarg_J25,
                UnbilledCons_Min_H28 = UnbilledCons_Min_H28,
                UnbilledCons_Max_H30 = UnbilledCons_Max_H30,
                UnbilledCons_BestEstimate_H32 = UnbilledCons_BestEstimate_H32,
                UnauthCons_Total_L6 = UnauthCons_Total_L6,
                UnauthCons_Total_L10 = UnauthCons_Total_L10,
                UnauthCons_Total_L14 = UnauthCons_Total_L14,
                UnauthCons_ErrorMarg_F24 = UnauthCons_ErrorMarg_F24,
                UnauthCons_Min_L27 = UnauthCons_Min_L27,
                UnauthCons_Max_L29 = UnauthCons_Max_L29,
                UnauthCons_BestEstimate_L31 = UnauthCons_BestEstimate_L31,

                MetErrors_Total_F8 = MetErrors_Total_F8,
                MetErrors_Total_F32 = MetErrors_Total_F32,
                MetErrors_Total_F34 = MetErrors_Total_F34,
                MetErrors_Total_F38 = MetErrors_Total_F38,
                MetErrors_Total_L8 = MetErrors_Total_L8,
                MetErrors_Total_L32 = MetErrors_Total_L32,
                MetErrors_Total_L34 = MetErrors_Total_L34,
                MetErrors_Total_L38 = MetErrors_Total_L38,
                MetErrors_ErrorMarg_N42 = MetErrors_ErrorMarg_N42,
                MetErrors_Min_L45 = MetErrors_Min_L45,
                MetErrors_Max_L47 = MetErrors_Max_L47,
                MetErrors_BestEstimate_L49 = MetErrors_BestEstimate_L49,

                MetErrors_Total_L12 = MetErrors_Total_L12,
                MetErrors_Total_L13 = MetErrors_Total_L13,
                MetErrors_Total_L14 = MetErrors_Total_L14,
                MetErrors_Total_L15 = MetErrors_Total_L15,

                UnauthCons_Total_L18 = UnauthCons_Total_L18,
                UnauthCons_Total_L19 = UnauthCons_Total_L19,
                UnauthCons_Total_L20 = UnauthCons_Total_L20,
                UnauthCons_Total_L21 = UnauthCons_Total_L21,

                Interm_Min_H29 = Interm_Min_H29,
                Interm_Max_H31 = Interm_Max_H31,
                Interm_BestEstimate_H33 = Interm_BestEstimate_H33,

                FinancData_G13 = FinancData_G13,
                FinancData_G15 = FinancData_G15,
                FinancData_G17 = FinancData_G17,
                FinancData_G19 = FinancData_G19,
                FinancData_G20 = FinancData_G20,
                FinancData_G22 = FinancData_G22,
                FinancData_D24 = FinancData_D24,
                FinancData_G31 = FinancData_G31,
                FinancData_K8 = FinancData_K8,
                FinancData_K13 = FinancData_K13,
                FinancData_K15 = FinancData_K15,
                FinancData_K17 = FinancData_K17,
                FinancData_K19 = FinancData_K19,
                FinancData_K20 = FinancData_K20,
                FinancData_K22 = FinancData_K22,
                FinancData_K31 = FinancData_K31,
                FinancData_K35 = FinancData_K35,

                WaterBalanceDay = (WaterBalanceModel)WaterBalanceDay.Clone(),
                WaterBalancePeriod = (WaterBalanceModel)WaterBalancePeriod.Clone(),
                WaterBalanceYear = (WaterBalanceModel)WaterBalanceYear.Clone(),

                Pis = (PisModel)Pis.Clone(),
            };
        }

        private EasyCalcDataInput _easyCalcDataInput;
        public EasyCalcDataInput EasyCalcDataInput
        {
            get
            {
                _easyCalcDataInput = new EasyCalcDataInput()
                {
                    //Start_PeriodDays_M21 = Start_PeriodDays_M21,

                    SysInput_Desc_B6 = SysInput_Desc_B6,
                    SysInput_Desc_B7 = SysInput_Desc_B7,
                    SysInput_Desc_B8 = SysInput_Desc_B8,
                    SysInput_Desc_B9 = SysInput_Desc_B9,
                    SysInput_SystemInputVolumeM3_D6 = SysInput_SystemInputVolumeM3_D6,
                    SysInput_SystemInputVolumeError_F6 = SysInput_SystemInputVolumeError_F6,
                    SysInput_SystemInputVolumeM3_D7 = SysInput_SystemInputVolumeM3_D7,
                    SysInput_SystemInputVolumeError_F7 = SysInput_SystemInputVolumeError_F7,
                    SysInput_SystemInputVolumeM3_D8 = SysInput_SystemInputVolumeM3_D8,
                    SysInput_SystemInputVolumeError_F8 = SysInput_SystemInputVolumeError_F8,
                    SysInput_SystemInputVolumeM3_D9 = SysInput_SystemInputVolumeM3_D9,
                    SysInput_SystemInputVolumeError_F9 = SysInput_SystemInputVolumeError_F9,

                    BilledCons_Desc_B8 = BilledCons_Desc_B8,
                    BilledCons_Desc_B9 = BilledCons_Desc_B9,
                    BilledCons_Desc_B10 = BilledCons_Desc_B10,
                    BilledCons_Desc_B11 = BilledCons_Desc_B11,
                    BilledCons_Desc_F8 = BilledCons_Desc_F8,
                    BilledCons_Desc_F9 = BilledCons_Desc_F9,
                    BilledCons_Desc_F10 = BilledCons_Desc_F10,
                    BilledCons_Desc_F11 = BilledCons_Desc_F11,
                    UnbilledCons_Desc_D8 = UnbilledCons_Desc_D8,
                    UnbilledCons_Desc_D9 = UnbilledCons_Desc_D9,
                    UnbilledCons_Desc_D10 = UnbilledCons_Desc_D10,
                    UnbilledCons_Desc_D11 = UnbilledCons_Desc_D11,
                    UnbilledCons_Desc_F6 = UnbilledCons_Desc_F6,
                    UnbilledCons_Desc_F7 = UnbilledCons_Desc_F7,
                    UnbilledCons_Desc_F8 = UnbilledCons_Desc_F8,
                    UnbilledCons_Desc_F9 = UnbilledCons_Desc_F9,
                    UnbilledCons_Desc_F10 = UnbilledCons_Desc_F10,
                    UnbilledCons_Desc_F11 = UnbilledCons_Desc_F11,
                    UnauthCons_Desc_B18 = UnauthCons_Desc_B18,
                    UnauthCons_Desc_B19 = UnauthCons_Desc_B19,
                    UnauthCons_Desc_B20 = UnauthCons_Desc_B20,
                    UnauthCons_Desc_B21 = UnauthCons_Desc_B21,
                    MetErrors_Desc_D12 = MetErrors_Desc_D12,
                    MetErrors_Desc_D13 = MetErrors_Desc_D13,
                    MetErrors_Desc_D14 = MetErrors_Desc_D14,
                    MetErrors_Desc_D15 = MetErrors_Desc_D15,
                    Network_Desc_B7 = Network_Desc_B7,
                    Network_Desc_B8 = Network_Desc_B8,
                    Network_Desc_B9 = Network_Desc_B9,
                    Network_Desc_B10 = Network_Desc_B10,
                    Interm_Area_B7 = Interm_Area_B7,
                    Interm_Area_B8 = Interm_Area_B8,
                    Interm_Area_B9 = Interm_Area_B9,
                    Interm_Area_B10 = Interm_Area_B10,

                    BilledCons_BilledMetConsBulkWatSupExpM3_D6 = BilledCons_BilledMetConsBulkWatSupExpM3_D6,
                    BilledCons_BilledUnmetConsBulkWatSupExpM3_H6 = BilledCons_BilledUnmetConsBulkWatSupExpM3_H6,

                    BilledCons_UnbMetConsM3_D8 = BilledCons_UnbMetConsM3_D8,
                    BilledCons_UnbMetConsM3_D9 = BilledCons_UnbMetConsM3_D9,
                    BilledCons_UnbMetConsM3_D10 = BilledCons_UnbMetConsM3_D10,
                    BilledCons_UnbMetConsM3_D11 = BilledCons_UnbMetConsM3_D11,
                    BilledCons_UnbUnmetConsM3_H8 = BilledCons_UnbUnmetConsM3_H8,
                    BilledCons_UnbUnmetConsM3_H9 = BilledCons_UnbUnmetConsM3_H9,
                    BilledCons_UnbUnmetConsM3_H10 = BilledCons_UnbUnmetConsM3_H10,
                    BilledCons_UnbUnmetConsM3_H11 = BilledCons_UnbUnmetConsM3_H11,

                    UnbilledCons_MetConsBulkWatSupExpM3_D6 = UnbilledCons_MetConsBulkWatSupExpM3_D6,

                    UnbilledCons_UnbMetConsM3_D8 = UnbilledCons_UnbMetConsM3_D8,
                    UnbilledCons_UnbMetConsM3_D9 = UnbilledCons_UnbMetConsM3_D9,
                    UnbilledCons_UnbMetConsM3_D10 = UnbilledCons_UnbMetConsM3_D10,
                    UnbilledCons_UnbMetConsM3_D11 = UnbilledCons_UnbMetConsM3_D11,
                    UnbilledCons_UnbUnmetConsM3_H6 = UnbilledCons_UnbUnmetConsM3_H6,
                    UnbilledCons_UnbUnmetConsM3_H7 = UnbilledCons_UnbUnmetConsM3_H7,
                    UnbilledCons_UnbUnmetConsM3_H8 = UnbilledCons_UnbUnmetConsM3_H8,
                    UnbilledCons_UnbUnmetConsM3_H9 = UnbilledCons_UnbUnmetConsM3_H9,
                    UnbilledCons_UnbUnmetConsM3_H10 = UnbilledCons_UnbUnmetConsM3_H10,
                    UnbilledCons_UnbUnmetConsM3_H11 = UnbilledCons_UnbUnmetConsM3_H11,
                    UnbilledCons_UnbUnmetConsError_J6 = UnbilledCons_UnbUnmetConsError_J6,
                    UnbilledCons_UnbUnmetConsError_J7 = UnbilledCons_UnbUnmetConsError_J7,
                    UnbilledCons_UnbUnmetConsError_J8 = UnbilledCons_UnbUnmetConsError_J8,
                    UnbilledCons_UnbUnmetConsError_J9 = UnbilledCons_UnbUnmetConsError_J9,
                    UnbilledCons_UnbUnmetConsError_J10 = UnbilledCons_UnbUnmetConsError_J10,
                    UnbilledCons_UnbUnmetConsError_J11 = UnbilledCons_UnbUnmetConsError_J11,

                    UnauthCons_OthersErrorMargin_F18 = UnauthCons_OthersErrorMargin_F18,
                    UnauthCons_OthersErrorMargin_F19 = UnauthCons_OthersErrorMargin_F19,
                    UnauthCons_OthersErrorMargin_F20 = UnauthCons_OthersErrorMargin_F20,
                    UnauthCons_OthersErrorMargin_F21 = UnauthCons_OthersErrorMargin_F21,
                    UnauthCons_OthersM3PerDay_J18 = UnauthCons_OthersM3PerDay_J18,
                    UnauthCons_OthersM3PerDay_J19 = UnauthCons_OthersM3PerDay_J19,
                    UnauthCons_OthersM3PerDay_J20 = UnauthCons_OthersM3PerDay_J20,
                    UnauthCons_OthersM3PerDay_J21 = UnauthCons_OthersM3PerDay_J21,

                    UnauthCons_IllegalConnDomEstNo_D6 = UnauthCons_IllegalConnDomEstNo_D6,
                    UnauthCons_IllegalConnDomErrorMargin_F6 = UnauthCons_IllegalConnDomErrorMargin_F6,
                    UnauthCons_IllegalConnDomPersPerHouse_H6 = UnauthCons_IllegalConnDomPersPerHouse_H6,
                    UnauthCons_IllegalConnDomConsLitPerPersDay_J6 = UnauthCons_IllegalConnDomConsLitPerPersDay_J6,
                    UnauthCons_IllegalConnOthersErrorMargin_F10 = UnauthCons_IllegalConnOthersErrorMargin_F10,

                    IllegalConnectionsOthersEstimatedNumber_D10 = IllegalConnectionsOthersEstimatedNumber_D10,
                    IllegalConnectionsOthersConsumptionLitersPerConnectionPerDay_J10 = IllegalConnectionsOthersConsumptionLitersPerConnectionPerDay_J10,

                    UnauthCons_MeterTampBypEtcEstNo_D14 = UnauthCons_MeterTampBypEtcEstNo_D14,
                    UnauthCons_MeterTampBypEtcErrorMargin_F14 = UnauthCons_MeterTampBypEtcErrorMargin_F14,
                    UnauthCons_MeterTampBypEtcConsLitPerCustDay_J14 = UnauthCons_MeterTampBypEtcConsLitPerCustDay_J14,
                    MetErrors_DetailedManualSpec_J6 = Math.Abs(MetErrors_DetailedManualSpec_J6-2)<0.01,
                    MetErrors_BilledMetConsWoBulkSupMetUndrreg_H8 = MetErrors_BilledMetConsWoBulkSupMetUndrreg_H8,
                    MetErrors_BilledMetConsWoBulkSupErrorMargin_N8 = MetErrors_BilledMetConsWoBulkSupErrorMargin_N8,

                    MetErrors_Total_F12 = MetErrors_Total_F12,
                    MetErrors_Total_F13 = MetErrors_Total_F13,
                    MetErrors_Total_F14 = MetErrors_Total_F14,
                    MetErrors_Total_F15 = MetErrors_Total_F15,
                    MetErrors_Meter_H12 = MetErrors_Meter_H12,
                    MetErrors_Meter_H13 = MetErrors_Meter_H13,
                    MetErrors_Meter_H14 = MetErrors_Meter_H14,
                    MetErrors_Meter_H15 = MetErrors_Meter_H15,
                    MetErrors_Error_N12 = MetErrors_Error_N12,
                    MetErrors_Error_N13 = MetErrors_Error_N13,
                    MetErrors_Error_N14 = MetErrors_Error_N14,
                    MetErrors_Error_N15 = MetErrors_Error_N15,

                    MeteredBulkSupplyExportErrorMargin_N32 = MeteredBulkSupplyExportErrorMargin_N32,
                    UnbilledMeteredConsumptionWithoutBulkSupplyErrorMargin_N34 = UnbilledMeteredConsumptionWithoutBulkSupplyErrorMargin_N34,
                    CorruptMeterReadingPracticessErrorMargin_N38 = CorruptMeterReadingPracticessErrorMargin_N38,
                    DataHandlingErrorsOffice_L40 = DataHandlingErrorsOffice_L40,
                    DataHandlingErrorsOfficeErrorMargin_N40 = DataHandlingErrorsOfficeErrorMargin_N40,


                    MetErrors_MetBulkSupExpMetUnderreg_H32 = MetErrors_MetBulkSupExpMetUnderreg_H32,
                    MetErrors_UnbillMetConsWoBulkSupplMetUndrreg_H34 = MetErrors_UnbillMetConsWoBulkSupplMetUndrreg_H34,
                    MetErrors_CorruptMetReadPractMetUndrreg_H38 = MetErrors_CorruptMetReadPractMetUndrreg_H38,
                    Network_DistributionAndTransmissionMains_D7 = Network_DistributionAndTransmissionMains_D7,
                    Network_DistributionAndTransmissionMains_D8 = Network_DistributionAndTransmissionMains_D8,
                    Network_DistributionAndTransmissionMains_D9 = Network_DistributionAndTransmissionMains_D9,
                    Network_DistributionAndTransmissionMains_D10 = Network_DistributionAndTransmissionMains_D10,
                    Network_NoOfConnOfRegCustomers_H10 = Network_NoOfConnOfRegCustomers_H10,
                    Network_NoOfInactAccountsWSvcConns_H18 = Network_NoOfInactAccountsWSvcConns_H18,
                    Network_AvgLenOfSvcConnFromBoundaryToMeterM_H32 = Network_AvgLenOfSvcConnFromBoundaryToMeterM_H32,

                    Network_PossibleUnd_D30 = Network_PossibleUnd_D30,
                    Network_NoCustomers_H7 = Network_NoCustomers_H7,
                    Network_ErrorMargin_J7 = Network_ErrorMargin_J7,
                    Network_ErrorMargin_J10 = Network_ErrorMargin_J10,
                    Network_ErrorMargin_J18 = Network_ErrorMargin_J18,
                    Network_ErrorMargin_J32 = Network_ErrorMargin_J32,
                    Network_ErrorMargin_D35 = Network_ErrorMargin_D35,

                    FinancData_G6 = FinancData_G6,
                    FinancData_K6 = FinancData_K6,
                    FinancData_G8 = FinancData_G8,
                    FinancData_D26 = FinancData_D26,
                    FinancData_G35 = FinancData_G35,
                    Prs_Area_B7 = Prs_Area_B7,
                    Prs_Area_B8 = Prs_Area_B8,
                    Prs_Area_B9 = Prs_Area_B9,
                    Prs_Area_B10 = Prs_Area_B10,
                    Prs_ApproxNoOfConn_D7 = Prs_ApproxNoOfConn_D7,
                    Prs_DailyAvgPrsM_F7 = Prs_DailyAvgPrsM_F7,
                    Prs_ApproxNoOfConn_D8 = Prs_ApproxNoOfConn_D8,
                    Prs_DailyAvgPrsM_F8 = Prs_DailyAvgPrsM_F8,
                    Prs_ApproxNoOfConn_D9 = Prs_ApproxNoOfConn_D9,
                    Prs_DailyAvgPrsM_F9 = Prs_DailyAvgPrsM_F9,
                    Prs_ApproxNoOfConn_D10 = Prs_ApproxNoOfConn_D10,
                    Prs_DailyAvgPrsM_F10 = Prs_DailyAvgPrsM_F10,
                    Prs_ErrorMarg_F26 = Prs_ErrorMarg_F26,

                    Interm_Conn_D7 = Interm_Conn_D7,
                    Interm_Conn_D8 = Interm_Conn_D8,
                    Interm_Conn_D9 = Interm_Conn_D9,
                    Interm_Conn_D10 = Interm_Conn_D10,
                    Interm_Days_F7 = Interm_Days_F7,
                    Interm_Days_F8 = Interm_Days_F8,
                    Interm_Days_F9 = Interm_Days_F9,
                    Interm_Days_F10 = Interm_Days_F10,
                    Interm_Hour_H7 = Interm_Hour_H7,
                    Interm_Hour_H8 = Interm_Hour_H8,
                    Interm_Hour_H9 = Interm_Hour_H9,
                    Interm_Hour_H10 = Interm_Hour_H10,
                    Interm_ErrorMarg_H26 = Interm_ErrorMarg_H26,

                };
                return _easyCalcDataInput;
            }
        }
    }
}
