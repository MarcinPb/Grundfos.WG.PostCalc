using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grundfos.OPC;
using Grundfos.OPC.Model;
using Grundfos.WB.EasyCalc.Calculations;

namespace WpfApp1.DataAccess
{
    public class Opc
    {
        //private ICollection<string> _opcTagList = new List<string>()
        //{
        //    "SysInput_SystemInputVolumeM3_D6",
        //    "SysInput_SystemInputVolumeError_F6",
        //    "BilledCons_BilledMetConsBulkWatSupExpM3_D6",
        //    "BilledCons_BilledUnmetConsBulkWatSupExpM3_H6",
        //    "UnbilledCons_MetConsBulkWatSupExpM3_D6",
        //    "UnauthCons_IllegalConnDomEstNo_D6",
        //    "UnauthCons_IllegalConnDomPersPerHouse_H6",
        //    "UnauthCons_IllegalConnDomConsLitPerPersDay_J6",
        //    "UnauthCons_IllegalConnDomErrorMargin_F6",
        //    "UnauthCons_IllegalConnOthersErrorMargin_F10",
        //    "UnauthCons_MeterTampBypEtcEstNo_D14",
        //    "UnauthCons_MeterTampBypEtcErrorMargin_F14",
        //    "UnauthCons_MeterTampBypEtcConsLitPerCustDay_J14",
        //    "MetErrors_DetailedManualSpec_J6",
        //    "MetErrors_BilledMetConsWoBulkSupMetUndrreg_H8",
        //    "MetErrors_BilledMetConsWoBulkSupErrorMargin_N8",
        //    "MetErrors_MetBulkSupExpMetUnderreg_H32",
        //    "MetErrors_UnbillMetConsWoBulkSupplMetUndrreg_H34",
        //    "MetErrors_CorruptMetReadPractMetUndrreg_H38",
        //    "Network_DistributionAndTransmissionMains_D7",
        //    "Network_NoOfConnOfRegCustomers_H10",
        //    "Network_NoOfInactAccountsWSvcConns_H18",
        //    "Network_AvgLenOfSvcConnFromBoundaryToMeterM_H32",
        //    "Prs_ApproxNoOfConn_D7",
        //    "Prs_DailyAvgPrsM_F7",
        //};

        public static void RunOpcPublish(string zoneRomanNo, EasyCalcDataInput easyCalcDataInput, EasyCalcDataOutput easyCalcDataOutput)
        {
            ICollection<OpcValue> values = new List<OpcValue>()
            {
                new OpcValue(){Tag=$"WbEasyCalc_{zoneRomanNo}.DEV.SysInput_SystemInputVolumeM3_D6", Value = easyCalcDataInput.SysInput_SystemInputVolumeM3_D6},
                new OpcValue(){Tag=$"WbEasyCalc_{zoneRomanNo}.DEV.SysInput_SystemInputVolumeError_F6", Value = easyCalcDataInput.SysInput_SystemInputVolumeError_F6},
                new OpcValue(){Tag=$"WbEasyCalc_{zoneRomanNo}.DEV.BilledCons_BilledMetConsBulkWatSupExpM3_D6", Value = easyCalcDataInput.BilledCons_BilledMetConsBulkWatSupExpM3_D6},
                new OpcValue(){Tag=$"WbEasyCalc_{zoneRomanNo}.DEV.BilledCons_BilledUnmetConsBulkWatSupExpM3_H6", Value = easyCalcDataInput.BilledCons_BilledUnmetConsBulkWatSupExpM3_H6},
                new OpcValue(){Tag=$"WbEasyCalc_{zoneRomanNo}.DEV.UnbilledCons_MetConsBulkWatSupExpM3_D6", Value = easyCalcDataInput.UnbilledCons_MetConsBulkWatSupExpM3_D6},
                new OpcValue(){Tag=$"WbEasyCalc_{zoneRomanNo}.DEV.UnauthCons_IllegalConnDomEstNo_D6", Value = easyCalcDataInput.UnauthCons_IllegalConnDomEstNo_D6},
                new OpcValue(){Tag=$"WbEasyCalc_{zoneRomanNo}.DEV.UnauthCons_IllegalConnDomPersPerHouse_H6", Value = easyCalcDataInput.UnauthCons_IllegalConnDomPersPerHouse_H6},
                new OpcValue(){Tag=$"WbEasyCalc_{zoneRomanNo}.DEV.UnauthCons_IllegalConnDomConsLitPerPersDay_J6", Value = easyCalcDataInput.UnauthCons_IllegalConnDomConsLitPerPersDay_J6},
                new OpcValue(){Tag=$"WbEasyCalc_{zoneRomanNo}.DEV.UnauthCons_IllegalConnDomErrorMargin_F6", Value = easyCalcDataInput.UnauthCons_IllegalConnDomErrorMargin_F6},
                new OpcValue(){Tag=$"WbEasyCalc_{zoneRomanNo}.DEV.UnauthCons_IllegalConnOthersErrorMargin_F10", Value = easyCalcDataInput.UnauthCons_IllegalConnOthersErrorMargin_F10},
                new OpcValue(){Tag=$"WbEasyCalc_{zoneRomanNo}.DEV.UnauthCons_MeterTampBypEtcEstNo_D14", Value = easyCalcDataInput.UnauthCons_MeterTampBypEtcEstNo_D14},
                new OpcValue(){Tag=$"WbEasyCalc_{zoneRomanNo}.DEV.UnauthCons_MeterTampBypEtcErrorMargin_F14", Value = easyCalcDataInput.UnauthCons_MeterTampBypEtcErrorMargin_F14},
                new OpcValue(){Tag=$"WbEasyCalc_{zoneRomanNo}.DEV.UnauthCons_MeterTampBypEtcConsLitPerCustDay_J14", Value = easyCalcDataInput.UnauthCons_MeterTampBypEtcConsLitPerCustDay_J14},
                new OpcValue(){Tag=$"WbEasyCalc_{zoneRomanNo}.DEV.MetErrors_DetailedManualSpec_J6", Value = easyCalcDataInput.MetErrors_DetailedManualSpec_J6},
                new OpcValue(){Tag=$"WbEasyCalc_{zoneRomanNo}.DEV.MetErrors_BilledMetConsWoBulkSupMetUndrreg_H8", Value = easyCalcDataInput.MetErrors_BilledMetConsWoBulkSupMetUndrreg_H8},
                new OpcValue(){Tag=$"WbEasyCalc_{zoneRomanNo}.DEV.MetErrors_BilledMetConsWoBulkSupErrorMargin_N8", Value = easyCalcDataInput.MetErrors_BilledMetConsWoBulkSupErrorMargin_N8},
                new OpcValue(){Tag=$"WbEasyCalc_{zoneRomanNo}.DEV.MetErrors_MetBulkSupExpMetUnderreg_H32", Value = easyCalcDataInput.MetErrors_MetBulkSupExpMetUnderreg_H32},
                new OpcValue(){Tag=$"WbEasyCalc_{zoneRomanNo}.DEV.MetErrors_UnbillMetConsWoBulkSupplMetUndrreg_H34", Value = easyCalcDataInput.MetErrors_UnbillMetConsWoBulkSupplMetUndrreg_H34},
                new OpcValue(){Tag=$"WbEasyCalc_{zoneRomanNo}.DEV.MetErrors_CorruptMetReadPractMetUndrreg_H38", Value = easyCalcDataInput.MetErrors_CorruptMetReadPractMetUndrreg_H38},
                new OpcValue(){Tag=$"WbEasyCalc_{zoneRomanNo}.DEV.Network_DistributionAndTransmissionMains_D7", Value = easyCalcDataInput.Network_DistributionAndTransmissionMains_D7},
                new OpcValue(){Tag=$"WbEasyCalc_{zoneRomanNo}.DEV.Network_NoOfConnOfRegCustomers_H10", Value = easyCalcDataInput.Network_NoOfConnOfRegCustomers_H10},
                new OpcValue(){Tag=$"WbEasyCalc_{zoneRomanNo}.DEV.Network_NoOfInactAccountsWSvcConns_H18", Value = easyCalcDataInput.Network_NoOfInactAccountsWSvcConns_H18},
                new OpcValue(){Tag=$"WbEasyCalc_{zoneRomanNo}.DEV.Network_AvgLenOfSvcConnFromBoundaryToMeterM_H32", Value = easyCalcDataInput.Network_AvgLenOfSvcConnFromBoundaryToMeterM_H32},
                new OpcValue(){Tag=$"WbEasyCalc_{zoneRomanNo}.DEV.Prs_ApproxNoOfConn_D7", Value = easyCalcDataInput.Prs_ApproxNoOfConn_D7},
                new OpcValue(){Tag=$"WbEasyCalc_{zoneRomanNo}.DEV.Prs_DailyAvgPrsM_F7", Value = easyCalcDataInput.Prs_DailyAvgPrsM_F7},

                new OpcValue(){Tag=$"WbEasyCalc_{zoneRomanNo}.DEV.WatBal_SystemInputVolume_B19", Value = easyCalcDataOutput.SystemInputVolume_B19},
                new OpcValue(){Tag=$"WbEasyCalc_{zoneRomanNo}.DEV.WatBal_SystemInputVolumeErrorMargin_B21", Value = easyCalcDataOutput.SystemInputVolumeErrorMargin_B21},
                new OpcValue(){Tag=$"WbEasyCalc_{zoneRomanNo}.DEV.WatBal_AuthorizedConsumption_K12", Value = easyCalcDataOutput.AuthorizedConsumption_K12},
                new OpcValue(){Tag=$"WbEasyCalc_{zoneRomanNo}.DEV.WatBal_AuthorizedConsumptionErrorMargin_K15", Value = easyCalcDataOutput.AuthorizedConsumptionErrorMargin_K15},
                new OpcValue(){Tag=$"WbEasyCalc_{zoneRomanNo}.DEV.WatBal_WaterLosses_K29", Value = easyCalcDataOutput.WaterLosses_K29},
                new OpcValue(){Tag=$"WbEasyCalc_{zoneRomanNo}.DEV.WatBal_WaterLossesErrorMargin_K31", Value = easyCalcDataOutput.WaterLossesErrorMargin_K31},
                new OpcValue(){Tag=$"WbEasyCalc_{zoneRomanNo}.DEV.WatBal_BilledAuthorizedConsumption_T8", Value = easyCalcDataOutput.BilledAuthorizedConsumption_T8},
                new OpcValue(){Tag=$"WbEasyCalc_{zoneRomanNo}.DEV.WatBal_UnbilledAuthorizedConsumption_T16", Value = easyCalcDataOutput.UnbilledAuthorizedConsumption_T16},
                new OpcValue(){Tag=$"WbEasyCalc_{zoneRomanNo}.DEV.WatBal_CommercialLosses_T26", Value = easyCalcDataOutput.CommercialLosses_T26},
                new OpcValue(){Tag=$"WbEasyCalc_{zoneRomanNo}.DEV.WatBal_CommercialLossesErrorMargin_T29", Value = easyCalcDataOutput.CommercialLossesErrorMargin_T29},
                new OpcValue(){Tag=$"WbEasyCalc_{zoneRomanNo}.DEV.WatBal_PhysicalLossesM3_T34", Value = easyCalcDataOutput.PhysicalLossesM3_T34},
                new OpcValue(){Tag=$"WbEasyCalc_{zoneRomanNo}.DEV.WatBal_PhyscialLossesErrorMargin_AH35", Value = easyCalcDataOutput.PhyscialLossesErrorMargin_AH35},
                new OpcValue(){Tag=$"WbEasyCalc_{zoneRomanNo}.DEV.WatBal_BilledMeteredConsumption_AC4", Value = easyCalcDataOutput.BilledMeteredConsumption_AC4},
                new OpcValue(){Tag=$"WbEasyCalc_{zoneRomanNo}.DEV.WatBal_BilledUnmeteredConsumption_AC9", Value = easyCalcDataOutput.BilledUnmeteredConsumption_AC9},
                new OpcValue(){Tag=$"WbEasyCalc_{zoneRomanNo}.DEV.WatBal_UnbilledMeteredConsumption_AC14", Value = easyCalcDataOutput.UnbilledMeteredConsumption_AC14},
                new OpcValue(){Tag=$"WbEasyCalc_{zoneRomanNo}.DEV.WatBal_UnbilledUnmeteredConsumption_AC19", Value = easyCalcDataOutput.UnbilledUnmeteredConsumption_AC19},
                new OpcValue(){Tag=$"WbEasyCalc_{zoneRomanNo}.DEV.WatBal_UnbilledUnmeteredConsumptionErrorMargin_AO20", Value = easyCalcDataOutput.UnbilledUnmeteredConsumptionErrorMargin_AO20},
                new OpcValue(){Tag=$"WbEasyCalc_{zoneRomanNo}.DEV.WatBal_UnauthorizedConsumption_AC24", Value = easyCalcDataOutput.UnauthorizedConsumption_AC24},
                new OpcValue(){Tag=$"WbEasyCalc_{zoneRomanNo}.DEV.WatBal_UnauthorizedConsumptionErrorMargin_AO25", Value = easyCalcDataOutput.UnauthorizedConsumptionErrorMargin_AO25},
                new OpcValue(){Tag=$"WbEasyCalc_{zoneRomanNo}.DEV.WatBal_CustomerMeterInaccuraciesAndErrorsM3_AC29", Value = easyCalcDataOutput.CustomerMeterInaccuraciesAndErrorsM3_AC29},
                new OpcValue(){Tag=$"WbEasyCalc_{zoneRomanNo}.DEV.WatBal_CustomerMeterInaccuraciesAndErrorsErrorMargin_AO30", Value = easyCalcDataOutput.CustomerMeterInaccuraciesAndErrorsErrorMargin_AO30},
                new OpcValue(){Tag=$"WbEasyCalc_{zoneRomanNo}.DEV.WatBal_RevenueWaterM3_AY8", Value = easyCalcDataOutput.RevenueWaterM3_AY8},
                new OpcValue(){Tag=$"WbEasyCalc_{zoneRomanNo}.DEV.WatBal_NonRevenueWaterM3_AY24", Value = easyCalcDataOutput.NonRevenueWaterM3_AY24},
                new OpcValue(){Tag=$"WbEasyCalc_{zoneRomanNo}.DEV.WatBal_NonRevenueWaterErrorMargin_AY26", Value = easyCalcDataOutput.NonRevenueWaterErrorMargin_AY26},
            };

            string opcAddress = ConfigurationManager.AppSettings["opcAddress"];
            using (var client = new OpcReader(opcAddress))
            {
                //log.Log(LogLevel.Info, "OpcValue start ---------------------------");
                //foreach (var opcValue in values)
                //{
                //    log.Log(LogLevel.Info, opcValue);
                //}
                //log.Log(LogLevel.Info, "OpcValue end -----------------------------");
                ////client.WriteValues(values);

                //foreach (var opcValue in values)
                //{
                //    opcValue.Value = 0;
                //}
                client.WriteValues(values);
            }
        }
    }
}
