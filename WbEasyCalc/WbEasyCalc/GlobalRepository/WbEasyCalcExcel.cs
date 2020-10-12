﻿using System.Configuration;
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
            excelReader.WriteToCell("Sys. Input", "D6", easyCalcDataInput.SysInput_SystemInputVolumeM3_D6);
            excelReader.WriteToCell("Sys. Input", "F6", easyCalcDataInput.SysInput_SystemInputVolumeError_F6);
            excelReader.WriteToCell("Billed Cons", "D6", easyCalcDataInput.BilledCons_BilledMetConsBulkWatSupExpM3_D6);
            excelReader.WriteToCell("Billed Cons", "H6", easyCalcDataInput.BilledCons_BilledUnmetConsBulkWatSupExpM3_H6);
            excelReader.WriteToCell("Unb. Cons.", "D6", easyCalcDataInput.UnbilledCons_MetConsBulkWatSupExpM3_D6);
            excelReader.WriteToCell("Unauth. Cons.", "D6", easyCalcDataInput.UnauthCons_IllegalConnDomEstNo_D6);
            excelReader.WriteToCell("Unauth. Cons.", "H6", easyCalcDataInput.UnauthCons_IllegalConnDomPersPerHouse_H6);
            excelReader.WriteToCell("Unauth. Cons.", "J6", easyCalcDataInput.UnauthCons_IllegalConnDomConsLitPerPersDay_J6);
            excelReader.WriteToCell("Unauth. Cons.", "F6", easyCalcDataInput.UnauthCons_IllegalConnDomErrorMargin_F6);
            excelReader.WriteToCell("Unauth. Cons.", "F10", easyCalcDataInput.UnauthCons_IllegalConnOthersErrorMargin_F10);
            excelReader.WriteToCell("Unauth. Cons.", "D14", easyCalcDataInput.UnauthCons_MeterTampBypEtcEstNo_D14);
            excelReader.WriteToCell("Unauth. Cons.", "F14", easyCalcDataInput.UnauthCons_MeterTampBypEtcErrorMargin_F14);
            excelReader.WriteToCell("Unauth. Cons.", "J14", easyCalcDataInput.UnauthCons_MeterTampBypEtcConsLitPerCustDay_J14);
            excelReader.WriteToCell("Meter Errors", "J6", easyCalcDataInput.MetErrors_DetailedManualSpec_J6);
            excelReader.WriteToCell("Meter Errors", "H8", easyCalcDataInput.MetErrors_BilledMetConsWoBulkSupMetUndrreg_H8);
            excelReader.WriteToCell("Meter Errors", "N8", easyCalcDataInput.MetErrors_BilledMetConsWoBulkSupErrorMargin_N8);
            excelReader.WriteToCell("Meter Errors", "H32", easyCalcDataInput.MetErrors_MetBulkSupExpMetUnderreg_H32);
            excelReader.WriteToCell("Meter Errors", "H34", easyCalcDataInput.MetErrors_UnbillMetConsWoBulkSupplMetUndrreg_H34);
            excelReader.WriteToCell("Meter Errors", "H38", easyCalcDataInput.MetErrors_CorruptMetReadPractMetUndrreg_H38);
            excelReader.WriteToCell("Network", "D7", easyCalcDataInput.Network_DistributionAndTransmissionMains_D7);
            excelReader.WriteToCell("Network", "H10", easyCalcDataInput.Network_NoOfConnOfRegCustomers_H10);
            excelReader.WriteToCell("Network", "H18", easyCalcDataInput.Network_NoOfInactAccountsWSvcConns_H18);
            excelReader.WriteToCell("Network", "H32", easyCalcDataInput.Network_AvgLenOfSvcConnFromBoundaryToMeterM_H32);
            excelReader.WriteToCell("Pressure", "D7", easyCalcDataInput.Prs_ApproxNoOfConn_D7);
            excelReader.WriteToCell("Pressure", "F7", easyCalcDataInput.Prs_DailyAvgPrsM_F7);

            excelReader.WriteToFile(excelFileName);
        }

        public EasyCalcDataInput LoadFromExcelFile(string excelFileName)
        {
            var excelReader = new ExcelReader(excelFileName);

            EasyCalcDataInput easyCalcDataInput = new EasyCalcDataInput
            {
                Start_PeriodDays_M21 = excelReader.ReadCell<int>("Start", "M21"),
                SysInput_SystemInputVolumeM3_D6 = excelReader.ReadCell<double>("Sys. Input", "D6"),
                SysInput_SystemInputVolumeError_F6 = excelReader.ReadCell<double>("Sys. Input", "F6"),
                BilledCons_BilledMetConsBulkWatSupExpM3_D6 = excelReader.ReadCell<double>("Billed Cons", "D6"),
                BilledCons_BilledUnmetConsBulkWatSupExpM3_H6 = excelReader.ReadCell<double>("Billed Cons", "H6"),
                UnbilledCons_MetConsBulkWatSupExpM3_D6 = excelReader.ReadCell<double>("Unb. Cons.", "D6"),
                UnauthCons_IllegalConnDomEstNo_D6 = excelReader.ReadCell<int>("Unauth. Cons.", "D6"),
                UnauthCons_IllegalConnDomPersPerHouse_H6 = excelReader.ReadCell<double>("Unauth. Cons.", "H6"),
                UnauthCons_IllegalConnDomConsLitPerPersDay_J6 = excelReader.ReadCell<double>("Unauth. Cons.", "J6"),
                UnauthCons_IllegalConnDomErrorMargin_F6 = excelReader.ReadCell<double>("Unauth. Cons.", "F6"),
                UnauthCons_IllegalConnOthersErrorMargin_F10 = excelReader.ReadCell<double>("Unauth. Cons.", "F10"),
                UnauthCons_MeterTampBypEtcEstNo_D14 = excelReader.ReadCell<double>("Unauth. Cons.", "D14"),
                UnauthCons_MeterTampBypEtcErrorMargin_F14 = excelReader.ReadCell<double>("Unauth. Cons.", "F14"),
                UnauthCons_MeterTampBypEtcConsLitPerCustDay_J14 = excelReader.ReadCell<double>("Unauth. Cons.", "J14"),
                MetErrors_DetailedManualSpec_J6 = excelReader.ReadCell<int>("Meter Errors", "J6") == 2,
                MetErrors_BilledMetConsWoBulkSupMetUndrreg_H8 = excelReader.ReadCell<double>("Meter Errors", "H8"),
                MetErrors_BilledMetConsWoBulkSupErrorMargin_N8 = excelReader.ReadCell<double>("Meter Errors", "N8"),
                MetErrors_MetBulkSupExpMetUnderreg_H32 = excelReader.ReadCell<double>("Meter Errors", "H32"),
                MetErrors_UnbillMetConsWoBulkSupplMetUndrreg_H34 = excelReader.ReadCell<double>("Meter Errors", "H34"),
                MetErrors_CorruptMetReadPractMetUndrreg_H38 = excelReader.ReadCell<double>("Meter Errors", "H38"),
                Network_DistributionAndTransmissionMains_D7 = excelReader.ReadCell<double>("Network", "D7"),
                Network_NoOfConnOfRegCustomers_H10 = excelReader.ReadCell<double>("Network", "H10"),
                Network_NoOfInactAccountsWSvcConns_H18 = excelReader.ReadCell<double>("Network", "H18"),
                Network_AvgLenOfSvcConnFromBoundaryToMeterM_H32 = excelReader.ReadCell<double>("Network", "H32"),
                Prs_ApproxNoOfConn_D7 = excelReader.ReadCell<double>("Pressure", "D7"),
                Prs_DailyAvgPrsM_F7 = excelReader.ReadCell<double>("Pressure", "F7")
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
