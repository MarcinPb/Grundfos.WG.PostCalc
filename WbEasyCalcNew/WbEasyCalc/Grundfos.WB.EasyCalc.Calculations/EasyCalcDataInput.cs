using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grundfos.WB.EasyCalc.Calculations
{
    public class EasyCalcDataInput
    {
        public int Start_PeriodDays_M21 { get; set; }
        public double SysInput_SystemInputVolumeM3_D6 { get; set; }
        //public double SysInput_SystemInputVolumeM3_D7 { get; set; }
        //public double SysInput_SystemInputVolumeM3_D8 { get; set; }
        //public double SysInput_SystemInputVolumeM3_D9 { get; set; }
        //public double SysInput_SystemInputVolumeM3_D10 { get; set; }
        //public double SysInput_SystemInputVolumeM3_D11 { get; set; }
        //public double SysInput_SystemInputVolumeM3_D12 { get; set; }
        //public double SysInput_SystemInputVolumeM3_D13 { get; set; }
        //public double SysInput_SystemInputVolumeM3_D14 { get; set; }
        //public double SysInput_SystemInputVolumeM3_D15 { get; set; }
        //public double SysInput_SystemInputVolumeM3_D16 { get; set; }
        //public double SysInput_SystemInputVolumeM3_D17 { get; set; }
        //public double SysInput_SystemInputVolumeM3_D18 { get; set; }
        //public double SysInput_SystemInputVolumeM3_D19 { get; set; }
        //public double SysInput_SystemInputVolumeM3_D20 { get; set; }
        //public double SysInput_SystemInputVolumeM3_D21 { get; set; }
        //public double SysInput_SystemInputVolumeM3_D22 { get; set; }
        //public double SysInput_SystemInputVolumeM3_D23 { get; set; }
        //public double SysInput_SystemInputVolumeM3_D24 { get; set; }
        //public double SysInput_SystemInputVolumeM3_D25 { get; set; }
        //public double SysInput_SystemInputVolumeM3_D26 { get; set; }
        public double SysInput_SystemInputVolumeError_F6 { get; set; }
        //public double SysInput_SystemInputVolumeError_F7 { get; set; }
        //public double SysInput_SystemInputVolumeError_F8 { get; set; }
        //public double SysInput_SystemInputVolumeError_F9 { get; set; }
        //public double SysInput_SystemInputVolumeError_F10 { get; set; }
        //public double SysInput_SystemInputVolumeError_F11 { get; set; }
        //public double SysInput_SystemInputVolumeError_F12 { get; set; }
        //public double SysInput_SystemInputVolumeError_F13 { get; set; }
        //public double SysInput_SystemInputVolumeError_F14 { get; set; }
        //public double SysInput_SystemInputVolumeError_F15 { get; set; }
        //public double SysInput_SystemInputVolumeError_F16 { get; set; }
        //public double SysInput_SystemInputVolumeError_F17 { get; set; }
        //public double SysInput_SystemInputVolumeError_F18 { get; set; }
        //public double SysInput_SystemInputVolumeError_F19 { get; set; }
        //public double SysInput_SystemInputVolumeError_F20 { get; set; }
        //public double SysInput_SystemInputVolumeError_F21 { get; set; }
        //public double SysInput_SystemInputVolumeError_F22 { get; set; }
        //public double SysInput_SystemInputVolumeError_F23 { get; set; }
        //public double SysInput_SystemInputVolumeError_F24 { get; set; }
        //public double SysInput_SystemInputVolumeError_F25 { get; set; }
        //public double SysInput_SystemInputVolumeError_F26 { get; set; }
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
        public bool   MetErrors_DetailedManualSpec_J6 { get; set; }
        public double MetErrors_BilledMetConsWoBulkSupMetUndrreg_H8 { get; set; }
        public double MetErrors_BilledMetConsWoBulkSupErrorMargin_N8 { get; set; }
        public double MetErrors_MetBulkSupExpMetUnderreg_H32 { get; set; }
        public double MetErrors_UnbillMetConsWoBulkSupplMetUndrreg_H34 { get; set; }
        public double MetErrors_CorruptMetReadPractMetUndrreg_H38 { get; set; }
        public double Network_DistributionAndTransmissionMains_D7 { get; set; }
        //public double Network_DistributionAndTransmissionMains_D8 { get; set; }
        //public double Network_DistributionAndTransmissionMains_D9 { get; set; }
        //public double Network_DistributionAndTransmissionMains_D10 { get; set; }
        //public double Network_DistributionAndTransmissionMains_D11 { get; set; }
        //public double Network_DistributionAndTransmissionMains_D12 { get; set; }
        //public double Network_DistributionAndTransmissionMains_D13 { get; set; }
        //public double Network_DistributionAndTransmissionMains_D14 { get; set; }
        //public double Network_DistributionAndTransmissionMains_D15 { get; set; }
        //public double Network_DistributionAndTransmissionMains_D16 { get; set; }
        public double Network_NoOfConnOfRegCustomers_H10 { get; set; }
        public double Network_NoOfInactAccountsWSvcConns_H18 { get; set; }
        public double Network_AvgLenOfSvcConnFromBoundaryToMeterM_H32 { get; set; }
        public double Prs_ApproxNoOfConn_D7 { get; set; }
        //public double Prs_ApproxNoOfConn_D8 { get; set; }
        //public double Prs_ApproxNoOfConn_D9 { get; set; }
        //public double Prs_ApproxNoOfConn_D10 { get; set; }
        //public double Prs_ApproxNoOfConn_D11 { get; set; }
        //public double Prs_ApproxNoOfConn_D12 { get; set; }
        //public double Prs_ApproxNoOfConn_D13 { get; set; }
        //public double Prs_ApproxNoOfConn_D14 { get; set; }
        //public double Prs_ApproxNoOfConn_D15 { get; set; }
        //public double Prs_ApproxNoOfConn_D16 { get; set; }
        public double Prs_DailyAvgPrsM_F7 { get; set; }
        //public double Prs_DailyAvgPrsM_F8 { get; set; }
        //public double Prs_DailyAvgPrsM_F9 { get; set; }
        //public double Prs_DailyAvgPrsM_F10 { get; set; }
        //public double Prs_DailyAvgPrsM_F11 { get; set; }
        //public double Prs_DailyAvgPrsM_F12 { get; set; }
        //public double Prs_DailyAvgPrsM_F13 { get; set; }
        //public double Prs_DailyAvgPrsM_F14 { get; set; }
        //public double Prs_DailyAvgPrsM_F15 { get; set; }
        //public double Prs_DailyAvgPrsM_F16 { get; set; }
        public double PIs_IliBestEstimate_F25 { get; set; }

    }
}
