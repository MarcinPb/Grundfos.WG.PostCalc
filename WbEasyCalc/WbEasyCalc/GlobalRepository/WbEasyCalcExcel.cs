using System.Configuration;
using ExcelNpoi;
using WbEasyCalc;

namespace GlobalRepository
{
    public class WbEasyCalcExcel : IWbEasyCalcExcel
    {
        public void SaveToExcelFile(string excelFileName, EasyCalcDataInput easyCalcDataInput)
        {
            var excelReader = new ExcelReader(_excelTemplateFileName);

            excelReader.WriteToCell("Start", "M21", easyCalcDataInput.Start_PeriodDays_M21);

            excelReader.WriteToCell("Sys. Input", "B6", easyCalcDataInput.SysInput_Desc_B6);
            excelReader.WriteToCell("Sys. Input", "B7", easyCalcDataInput.SysInput_Desc_B7);
            excelReader.WriteToCell("Sys. Input", "B8", easyCalcDataInput.SysInput_Desc_B8);
            excelReader.WriteToCell("Sys. Input", "B9", easyCalcDataInput.SysInput_Desc_B9);
            excelReader.WriteToCell("Sys. Input", "D6", easyCalcDataInput.SysInput_SystemInputVolumeM3_D6);
            excelReader.WriteToCell("Sys. Input", "F6", easyCalcDataInput.SysInput_SystemInputVolumeError_F6);
            excelReader.WriteToCell("Sys. Input", "D7", easyCalcDataInput.SysInput_SystemInputVolumeM3_D7);
            excelReader.WriteToCell("Sys. Input", "F7", easyCalcDataInput.SysInput_SystemInputVolumeError_F7);
            excelReader.WriteToCell("Sys. Input", "D8", easyCalcDataInput.SysInput_SystemInputVolumeM3_D8);
            excelReader.WriteToCell("Sys. Input", "F8", easyCalcDataInput.SysInput_SystemInputVolumeError_F8);
            excelReader.WriteToCell("Sys. Input", "D9", easyCalcDataInput.SysInput_SystemInputVolumeM3_D9);
            excelReader.WriteToCell("Sys. Input", "F9", easyCalcDataInput.SysInput_SystemInputVolumeError_F9);

            excelReader.WriteToCell("Billed Cons", "D6", easyCalcDataInput.BilledCons_BilledMetConsBulkWatSupExpM3_D6);
            excelReader.WriteToCell("Billed Cons", "H6", easyCalcDataInput.BilledCons_BilledUnmetConsBulkWatSupExpM3_H6);

            excelReader.WriteToCell("Billed Cons", "D8", easyCalcDataInput.BilledCons_UnbMetConsM3_D8);
            excelReader.WriteToCell("Billed Cons", "D9", easyCalcDataInput.BilledCons_UnbMetConsM3_D9);
            excelReader.WriteToCell("Billed Cons", "D10", easyCalcDataInput.BilledCons_UnbMetConsM3_D10);
            excelReader.WriteToCell("Billed Cons", "D11", easyCalcDataInput.BilledCons_UnbMetConsM3_D11);
            excelReader.WriteToCell("Billed Cons", "H8", easyCalcDataInput.BilledCons_UnbUnmetConsM3_H8);
            excelReader.WriteToCell("Billed Cons", "H9", easyCalcDataInput.BilledCons_UnbUnmetConsM3_H9);
            excelReader.WriteToCell("Billed Cons", "H10", easyCalcDataInput.BilledCons_UnbUnmetConsM3_H10);
            excelReader.WriteToCell("Billed Cons", "H11", easyCalcDataInput.BilledCons_UnbUnmetConsM3_H11);

            excelReader.WriteToCell("Unb. Cons.", "D6", easyCalcDataInput.UnbilledCons_MetConsBulkWatSupExpM3_D6);

            excelReader.WriteToCell("Unb. Cons.", "D8", easyCalcDataInput.UnbilledCons_UnbMetConsM3_D8);
            excelReader.WriteToCell("Unb. Cons.", "D9", easyCalcDataInput.UnbilledCons_UnbMetConsM3_D9);
            excelReader.WriteToCell("Unb. Cons.", "D10", easyCalcDataInput.UnbilledCons_UnbMetConsM3_D10);
            excelReader.WriteToCell("Unb. Cons.", "D11", easyCalcDataInput.UnbilledCons_UnbMetConsM3_D11);
            excelReader.WriteToCell("Unb. Cons.", "H6", easyCalcDataInput.UnbilledCons_UnbUnmetConsM3_H6);
            excelReader.WriteToCell("Unb. Cons.", "H7", easyCalcDataInput.UnbilledCons_UnbUnmetConsM3_H7);
            excelReader.WriteToCell("Unb. Cons.", "H8", easyCalcDataInput.UnbilledCons_UnbUnmetConsM3_H8);
            excelReader.WriteToCell("Unb. Cons.", "H9", easyCalcDataInput.UnbilledCons_UnbUnmetConsM3_H9);
            excelReader.WriteToCell("Unb. Cons.", "H10", easyCalcDataInput.UnbilledCons_UnbUnmetConsM3_H10);
            excelReader.WriteToCell("Unb. Cons.", "H11", easyCalcDataInput.UnbilledCons_UnbUnmetConsM3_H11);
            excelReader.WriteToCell("Unb. Cons.", "J6", easyCalcDataInput.UnbilledCons_UnbUnmetConsError_J6);
            excelReader.WriteToCell("Unb. Cons.", "J7", easyCalcDataInput.UnbilledCons_UnbUnmetConsError_J7);
            excelReader.WriteToCell("Unb. Cons.", "J8", easyCalcDataInput.UnbilledCons_UnbUnmetConsError_J8);
            excelReader.WriteToCell("Unb. Cons.", "J9", easyCalcDataInput.UnbilledCons_UnbUnmetConsError_J9);
            excelReader.WriteToCell("Unb. Cons.", "J10", easyCalcDataInput.UnbilledCons_UnbUnmetConsError_J10);
            excelReader.WriteToCell("Unb. Cons.", "J11", easyCalcDataInput.UnbilledCons_UnbUnmetConsError_J11);

            excelReader.WriteToCell("Unauth.Cons.", "F18", easyCalcDataInput.UnauthCons_OthersErrorMargin_F18);
            excelReader.WriteToCell("Unauth.Cons.", "F19", easyCalcDataInput.UnauthCons_OthersErrorMargin_F19);
            excelReader.WriteToCell("Unauth.Cons.", "F20", easyCalcDataInput.UnauthCons_OthersErrorMargin_F20);
            excelReader.WriteToCell("Unauth.Cons.", "F21", easyCalcDataInput.UnauthCons_OthersErrorMargin_F21);
            excelReader.WriteToCell("Unauth.Cons.", "J18", easyCalcDataInput.UnauthCons_OthersM3PerDay_J18);
            excelReader.WriteToCell("Unauth.Cons.", "J19", easyCalcDataInput.UnauthCons_OthersM3PerDay_J19);
            excelReader.WriteToCell("Unauth.Cons.", "J20", easyCalcDataInput.UnauthCons_OthersM3PerDay_J20);
            excelReader.WriteToCell("Unauth.Cons.", "J21", easyCalcDataInput.UnauthCons_OthersM3PerDay_J21);

            excelReader.WriteToCell("Unauth. Cons.", "D6", easyCalcDataInput.UnauthCons_IllegalConnDomEstNo_D6);
            excelReader.WriteToCell("Unauth. Cons.", "H6", easyCalcDataInput.UnauthCons_IllegalConnDomPersPerHouse_H6);
            excelReader.WriteToCell("Unauth. Cons.", "J6", easyCalcDataInput.UnauthCons_IllegalConnDomConsLitPerPersDay_J6);
            excelReader.WriteToCell("Unauth. Cons.", "F6", easyCalcDataInput.UnauthCons_IllegalConnDomErrorMargin_F6);
            excelReader.WriteToCell("Unauth. Cons.", "F10", easyCalcDataInput.UnauthCons_IllegalConnOthersErrorMargin_F10);

            excelReader.WriteToCell("Unauth. Cons.", "D10", easyCalcDataInput.IllegalConnectionsOthersEstimatedNumber_D10);
            excelReader.WriteToCell("Unauth. Cons.", "J10", easyCalcDataInput.IllegalConnectionsOthersConsumptionLitersPerConnectionPerDay_J10);

            excelReader.WriteToCell("Unauth. Cons.", "D14", easyCalcDataInput.UnauthCons_MeterTampBypEtcEstNo_D14);
            excelReader.WriteToCell("Unauth. Cons.", "F14", easyCalcDataInput.UnauthCons_MeterTampBypEtcErrorMargin_F14);
            excelReader.WriteToCell("Unauth. Cons.", "J14", easyCalcDataInput.UnauthCons_MeterTampBypEtcConsLitPerCustDay_J14);
            excelReader.WriteToCell("Meter Errors", "J6", easyCalcDataInput.MetErrors_DetailedManualSpec_J6 ? 2 : 1);
            excelReader.WriteToCell("Meter Errors", "H8", easyCalcDataInput.MetErrors_BilledMetConsWoBulkSupMetUndrreg_H8);
            excelReader.WriteToCell("Meter Errors", "N8", easyCalcDataInput.MetErrors_BilledMetConsWoBulkSupErrorMargin_N8);

            excelReader.WriteToCell("Meter Errors", "F12", easyCalcDataInput.MetErrors_Total_F12);
            excelReader.WriteToCell("Meter Errors", "F13", easyCalcDataInput.MetErrors_Total_F13);
            excelReader.WriteToCell("Meter Errors", "F14", easyCalcDataInput.MetErrors_Total_F14);
            excelReader.WriteToCell("Meter Errors", "F15", easyCalcDataInput.MetErrors_Total_F15);
            excelReader.WriteToCell("Meter Errors", "H12", easyCalcDataInput.MetErrors_Meter_H12);
            excelReader.WriteToCell("Meter Errors", "H13", easyCalcDataInput.MetErrors_Meter_H13);
            excelReader.WriteToCell("Meter Errors", "H14", easyCalcDataInput.MetErrors_Meter_H14);
            excelReader.WriteToCell("Meter Errors", "H15", easyCalcDataInput.MetErrors_Meter_H15);
            excelReader.WriteToCell("Meter Errors", "N12", easyCalcDataInput.MetErrors_Error_N12);
            excelReader.WriteToCell("Meter Errors", "N13", easyCalcDataInput.MetErrors_Error_N13);
            excelReader.WriteToCell("Meter Errors", "N14", easyCalcDataInput.MetErrors_Error_N14);
            excelReader.WriteToCell("Meter Errors", "N15", easyCalcDataInput.MetErrors_Error_N15);

            excelReader.WriteToCell("Meter Errors", "N32", easyCalcDataInput.MeteredBulkSupplyExportErrorMargin_N32);
            excelReader.WriteToCell("Meter Errors", "N34", easyCalcDataInput.UnbilledMeteredConsumptionWithoutBulkSupplyErrorMargin_N34);
            excelReader.WriteToCell("Meter Errors", "N38", easyCalcDataInput.CorruptMeterReadingPracticessErrorMargin_N38);
            excelReader.WriteToCell("Meter Errors", "L40", easyCalcDataInput.DataHandlingErrorsOffice_L40);
            excelReader.WriteToCell("Meter Errors", "N40", easyCalcDataInput.DataHandlingErrorsOfficeErrorMargin_N40);

            excelReader.WriteToCell("Meter Errors", "H32", easyCalcDataInput.MetErrors_MetBulkSupExpMetUnderreg_H32);
            excelReader.WriteToCell("Meter Errors", "H34", easyCalcDataInput.MetErrors_UnbillMetConsWoBulkSupplMetUndrreg_H34);
            excelReader.WriteToCell("Meter Errors", "H38", easyCalcDataInput.MetErrors_CorruptMetReadPractMetUndrreg_H38);
            excelReader.WriteToCell("Network", "D7", easyCalcDataInput.Network_DistributionAndTransmissionMains_D7);
            excelReader.WriteToCell("Network", "D8", easyCalcDataInput.Network_DistributionAndTransmissionMains_D8);
            excelReader.WriteToCell("Network", "D9", easyCalcDataInput.Network_DistributionAndTransmissionMains_D9);
            excelReader.WriteToCell("Network", "D10", easyCalcDataInput.Network_DistributionAndTransmissionMains_D10);
            excelReader.WriteToCell("Network", "H10", easyCalcDataInput.Network_NoOfConnOfRegCustomers_H10);
            excelReader.WriteToCell("Network", "H18", easyCalcDataInput.Network_NoOfInactAccountsWSvcConns_H18);
            excelReader.WriteToCell("Network", "H32", easyCalcDataInput.Network_AvgLenOfSvcConnFromBoundaryToMeterM_H32);

            excelReader.WriteToCell("Pressure", "B7", easyCalcDataInput.Prs_Area_B7);
            excelReader.WriteToCell("Pressure", "B8", easyCalcDataInput.Prs_Area_B8);
            excelReader.WriteToCell("Pressure", "B9", easyCalcDataInput.Prs_Area_B9);
            excelReader.WriteToCell("Pressure", "B10", easyCalcDataInput.Prs_Area_B10);
            excelReader.WriteToCell("Pressure", "D7", easyCalcDataInput.Prs_ApproxNoOfConn_D7);
            excelReader.WriteToCell("Pressure", "F7", easyCalcDataInput.Prs_DailyAvgPrsM_F7);
            excelReader.WriteToCell("Pressure", "D8", easyCalcDataInput.Prs_ApproxNoOfConn_D8);
            excelReader.WriteToCell("Pressure", "F8", easyCalcDataInput.Prs_DailyAvgPrsM_F8);
            excelReader.WriteToCell("Pressure", "D9", easyCalcDataInput.Prs_ApproxNoOfConn_D9);
            excelReader.WriteToCell("Pressure", "F9", easyCalcDataInput.Prs_DailyAvgPrsM_F9);
            excelReader.WriteToCell("Pressure", "D10", easyCalcDataInput.Prs_ApproxNoOfConn_D10);
            excelReader.WriteToCell("Pressure", "F10", easyCalcDataInput.Prs_DailyAvgPrsM_F10);

            excelReader.WriteToFile(excelFileName);
        }

        public EasyCalcDataInput LoadFromExcelFile(string excelFileName)
        {
            var excelReader = new ExcelReader(excelFileName);

            EasyCalcDataInput easyCalcDataInput = new EasyCalcDataInput
            {
                Start_PeriodDays_M21 = excelReader.ReadCell<int>("Start", "M21"),

                SysInput_Desc_B6 = excelReader.ReadCell<string>("Sys. Input", "B6"),
                SysInput_Desc_B7 = excelReader.ReadCell<string>("Sys. Input", "B7"),
                SysInput_Desc_B8 = excelReader.ReadCell<string>("Sys. Input", "B8"),
                SysInput_Desc_B9 = excelReader.ReadCell<string>("Sys. Input", "B9"),
                SysInput_SystemInputVolumeM3_D6 = excelReader.ReadCell<double>("Sys. Input", "D6"),
                SysInput_SystemInputVolumeError_F6 = excelReader.ReadCell<double>("Sys. Input", "F6"),
                SysInput_SystemInputVolumeM3_D7 = excelReader.ReadCell<double>("Sys. Input", "D7"),
                SysInput_SystemInputVolumeError_F7 = excelReader.ReadCell<double>("Sys. Input", "F7"),
                SysInput_SystemInputVolumeM3_D8 = excelReader.ReadCell<double>("Sys. Input", "D8"),
                SysInput_SystemInputVolumeError_F8 = excelReader.ReadCell<double>("Sys. Input", "F8"),
                SysInput_SystemInputVolumeM3_D9 = excelReader.ReadCell<double>("Sys. Input", "D9"),
                SysInput_SystemInputVolumeError_F9 = excelReader.ReadCell<double>("Sys. Input", "F9"),


                BilledCons_BilledMetConsBulkWatSupExpM3_D6 = excelReader.ReadCell<double>("Billed Cons", "D6"),
                BilledCons_BilledUnmetConsBulkWatSupExpM3_H6 = excelReader.ReadCell<double>("Billed Cons", "H6"),

                BilledCons_UnbMetConsM3_D8 = excelReader.ReadCell<double>("Billed Cons", "D8"),
                BilledCons_UnbMetConsM3_D9 = excelReader.ReadCell<double>("Billed Cons", "D9"),
                BilledCons_UnbMetConsM3_D10 = excelReader.ReadCell<double>("Billed Cons", "D10"),
                BilledCons_UnbMetConsM3_D11 = excelReader.ReadCell<double>("Billed Cons", "D11"),
                BilledCons_UnbUnmetConsM3_H8 = excelReader.ReadCell<double>("Billed Cons", "H8"),
                BilledCons_UnbUnmetConsM3_H9 = excelReader.ReadCell<double>("Billed Cons", "H9"),
                BilledCons_UnbUnmetConsM3_H10 = excelReader.ReadCell<double>("Billed Cons", "H10"),
                BilledCons_UnbUnmetConsM3_H11 = excelReader.ReadCell<double>("Billed Cons", "H11"),

                UnbilledCons_MetConsBulkWatSupExpM3_D6 = excelReader.ReadCell<double>("Unb. Cons.", "D6"),

                UnbilledCons_UnbMetConsM3_D8 = excelReader.ReadCell<double>("Unb. Cons.", "D8"),
                UnbilledCons_UnbMetConsM3_D9 = excelReader.ReadCell<double>("Unb. Cons.", "D9"),
                UnbilledCons_UnbMetConsM3_D10 = excelReader.ReadCell<double>("Unb. Cons.", "D10"),
                UnbilledCons_UnbMetConsM3_D11 = excelReader.ReadCell<double>("Unb. Cons.", "D11"),
                UnbilledCons_UnbUnmetConsM3_H6 = excelReader.ReadCell<double>("Unb. Cons.", "H6"),
                UnbilledCons_UnbUnmetConsM3_H7 = excelReader.ReadCell<double>("Unb. Cons.", "H7"),
                UnbilledCons_UnbUnmetConsM3_H8 = excelReader.ReadCell<double>("Unb. Cons.", "H8"),
                UnbilledCons_UnbUnmetConsM3_H9 = excelReader.ReadCell<double>("Unb. Cons.", "H9"),
                UnbilledCons_UnbUnmetConsM3_H10 = excelReader.ReadCell<double>("Unb. Cons.", "H10"),
                UnbilledCons_UnbUnmetConsM3_H11 = excelReader.ReadCell<double>("Unb. Cons.", "H11"),
                UnbilledCons_UnbUnmetConsError_J6 = excelReader.ReadCell<double>("Unb. Cons.", "J6"),
                UnbilledCons_UnbUnmetConsError_J7 = excelReader.ReadCell<double>("Unb. Cons.", "J7"),
                UnbilledCons_UnbUnmetConsError_J8 = excelReader.ReadCell<double>("Unb. Cons.", "J8"),
                UnbilledCons_UnbUnmetConsError_J9 = excelReader.ReadCell<double>("Unb. Cons.", "J9"),
                UnbilledCons_UnbUnmetConsError_J10 = excelReader.ReadCell<double>("Unb. Cons.", "J10"),
                UnbilledCons_UnbUnmetConsError_J11 = excelReader.ReadCell<double>("Unb. Cons.", "J11"),

                UnauthCons_OthersErrorMargin_F18 = excelReader.ReadCell<double>("Unb. Cons.", "F18"),
                UnauthCons_OthersErrorMargin_F19 = excelReader.ReadCell<double>("Unb. Cons.", "F19"),
                UnauthCons_OthersErrorMargin_F20 = excelReader.ReadCell<double>("Unb. Cons.", "F20"),
                UnauthCons_OthersErrorMargin_F21 = excelReader.ReadCell<double>("Unb. Cons.", "F21"),
                UnauthCons_OthersM3PerDay_J18 = excelReader.ReadCell<double>("Unb. Cons.", "J18"),
                UnauthCons_OthersM3PerDay_J19 = excelReader.ReadCell<double>("Unb. Cons.", "J19"),
                UnauthCons_OthersM3PerDay_J20 = excelReader.ReadCell<double>("Unb. Cons.", "J20"),
                UnauthCons_OthersM3PerDay_J21 = excelReader.ReadCell<double>("Unb. Cons.", "J21"),

                UnauthCons_IllegalConnDomEstNo_D6 = excelReader.ReadCell<int>("Unauth. Cons.", "D6"),
                UnauthCons_IllegalConnDomPersPerHouse_H6 = excelReader.ReadCell<double>("Unauth. Cons.", "H6"),
                UnauthCons_IllegalConnDomConsLitPerPersDay_J6 = excelReader.ReadCell<double>("Unauth. Cons.", "J6"),
                UnauthCons_IllegalConnDomErrorMargin_F6 = excelReader.ReadCell<double>("Unauth. Cons.", "F6"),
                UnauthCons_IllegalConnOthersErrorMargin_F10 = excelReader.ReadCell<double>("Unauth. Cons.", "F10"),

                IllegalConnectionsOthersEstimatedNumber_D10 = excelReader.ReadCell<double>("Unauth. Cons.", "D10"),
                IllegalConnectionsOthersConsumptionLitersPerConnectionPerDay_J10 = excelReader.ReadCell<double>("Unauth. Cons.", "J10"),

                UnauthCons_MeterTampBypEtcEstNo_D14 = excelReader.ReadCell<double>("Unauth. Cons.", "D14"),
                UnauthCons_MeterTampBypEtcErrorMargin_F14 = excelReader.ReadCell<double>("Unauth. Cons.", "F14"),
                UnauthCons_MeterTampBypEtcConsLitPerCustDay_J14 = excelReader.ReadCell<double>("Unauth. Cons.", "J14"),
                MetErrors_DetailedManualSpec_J6 = excelReader.ReadCell<int>("Meter Errors", "J6") == 2,
                MetErrors_BilledMetConsWoBulkSupMetUndrreg_H8 = excelReader.ReadCell<double>("Meter Errors", "H8"),
                MetErrors_BilledMetConsWoBulkSupErrorMargin_N8 = excelReader.ReadCell<double>("Meter Errors", "N8"),

                MetErrors_Total_F12 = excelReader.ReadCell<double>("Meter Errors", "F12"),
                MetErrors_Total_F13 = excelReader.ReadCell<double>("Meter Errors", "F13"),
                MetErrors_Total_F14 = excelReader.ReadCell<double>("Meter Errors", "F14"),
                MetErrors_Total_F15 = excelReader.ReadCell<double>("Meter Errors", "F15"),
                MetErrors_Meter_H12 = excelReader.ReadCell<double>("Meter Errors", "H12"),
                MetErrors_Meter_H13 = excelReader.ReadCell<double>("Meter Errors", "H13"),
                MetErrors_Meter_H14 = excelReader.ReadCell<double>("Meter Errors", "H14"),
                MetErrors_Meter_H15 = excelReader.ReadCell<double>("Meter Errors", "H15"),
                MetErrors_Error_N12 = excelReader.ReadCell<double>("Meter Errors", "N12"),
                MetErrors_Error_N13 = excelReader.ReadCell<double>("Meter Errors", "N13"),
                MetErrors_Error_N14 = excelReader.ReadCell<double>("Meter Errors", "N14"),
                MetErrors_Error_N15 = excelReader.ReadCell<double>("Meter Errors", "N15"),

                MeteredBulkSupplyExportErrorMargin_N32 = excelReader.ReadCell<double>("Meter Errors", "N32"),
                UnbilledMeteredConsumptionWithoutBulkSupplyErrorMargin_N34 = excelReader.ReadCell<double>("Meter Errors", "N34"),
                CorruptMeterReadingPracticessErrorMargin_N38 = excelReader.ReadCell<double>("Meter Errors", "N38"),
                DataHandlingErrorsOffice_L40 = excelReader.ReadCell<double>("Meter Errors", "L40"),
                DataHandlingErrorsOfficeErrorMargin_N40 = excelReader.ReadCell<double>("Meter Errors", "N40"),

                MetErrors_MetBulkSupExpMetUnderreg_H32 = excelReader.ReadCell<double>("Meter Errors", "H32"),
                MetErrors_UnbillMetConsWoBulkSupplMetUndrreg_H34 = excelReader.ReadCell<double>("Meter Errors", "H34"),
                MetErrors_CorruptMetReadPractMetUndrreg_H38 = excelReader.ReadCell<double>("Meter Errors", "H38"),
                Network_DistributionAndTransmissionMains_D7 = excelReader.ReadCell<double>("Network", "D7"),
                Network_NoOfConnOfRegCustomers_H10 = excelReader.ReadCell<double>("Network", "H10"),
                Network_NoOfInactAccountsWSvcConns_H18 = excelReader.ReadCell<double>("Network", "H18"),
                Network_AvgLenOfSvcConnFromBoundaryToMeterM_H32 = excelReader.ReadCell<double>("Network", "H32"),

                Prs_Area_B7 = excelReader.ReadCell<string>("Pressure", "B7"),
                Prs_Area_B8 = excelReader.ReadCell<string>("Pressure", "B8"),
                Prs_Area_B9 = excelReader.ReadCell<string>("Pressure", "B9"),
                Prs_Area_B10 = excelReader.ReadCell<string>("Pressure", "B10"),

                Prs_ApproxNoOfConn_D7 = excelReader.ReadCell<double>("Pressure", "D7"),
                Prs_DailyAvgPrsM_F7 = excelReader.ReadCell<double>("Pressure", "F7"),
                Prs_ApproxNoOfConn_D8 = excelReader.ReadCell<double>("Pressure", "D8"),
                Prs_DailyAvgPrsM_F8 = excelReader.ReadCell<double>("Pressure", "F8"),
                Prs_ApproxNoOfConn_D9 = excelReader.ReadCell<double>("Pressure", "D9"),
                Prs_DailyAvgPrsM_F9 = excelReader.ReadCell<double>("Pressure", "F9"),
                Prs_ApproxNoOfConn_D10 = excelReader.ReadCell<double>("Pressure", "D10"),
                Prs_DailyAvgPrsM_F10 = excelReader.ReadCell<double>("Pressure", "F10"),
            };

            return easyCalcDataInput;
        }

        private readonly string _excelTemplateFileName;
        public WbEasyCalcExcel(string excelTemplateFileName)
        {
            _excelTemplateFileName = excelTemplateFileName;
        }

    }
}
