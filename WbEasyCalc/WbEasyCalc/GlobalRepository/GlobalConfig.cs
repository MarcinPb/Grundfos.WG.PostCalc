using System;
using System.Configuration;
using DataModel;
using DataRepository;
using WbEasyCalc;

namespace GlobalRepository
{
    public static class GlobalConfig
    {
        // ex: GlobalConfig.Opc.RunOpcPublish(zoneRomanNo, easyCalcDataInput, easyCalcDataOutput); 
        public static IOpcServer OpcServer { get; set; }

        public static IWbEasyCalcExcel WbEasyCalcExcel { get; set; }

        public static IDataRepository DataRepository { get; private set; }


        public static void InitializeConnection(DatabaseType db)
        {
            OpcServer = new OpcServer(ConfigurationManager.AppSettings["opcAddress"]);
            WbEasyCalcExcel = new WbEasyCalcExcel(ConfigurationManager.AppSettings["ExcelTemplateFileName"]);

            if (db == DatabaseType.Sql)
            {
                DataRepository = new DataRepository.DataRepository(CnnString("TWDB"));
            }
            else if (db == DatabaseType.TextFile)
            {
                // todo - Set up the text Connector properly
                //TextConnector text = new TextConnector();
                DataRepository = null;
            }
        }

        public static string CnnString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        /*
        public static EasyCalcDataInput MapEasyCalcDataInput(WbEasyCalcData wbEasyCalcData)
        {
            EasyCalcDataInput easyCalcDataInput = new EasyCalcDataInput
            {
                Start_PeriodDays_M21 = wbEasyCalcData.Start_PeriodDays_M21,
                SysInput_SystemInputVolumeM3_D6 = wbEasyCalcData.SysInput_SystemInputVolumeM3_D6,
                SysInput_SystemInputVolumeError_F6 = wbEasyCalcData.SysInput_SystemInputVolumeError_F6,
                BilledCons_BilledMetConsBulkWatSupExpM3_D6 = wbEasyCalcData.BilledCons_BilledMetConsBulkWatSupExpM3_D6,
                BilledCons_BilledUnmetConsBulkWatSupExpM3_H6 = wbEasyCalcData.BilledCons_BilledUnmetConsBulkWatSupExpM3_H6,
                UnbilledCons_MetConsBulkWatSupExpM3_D6 = wbEasyCalcData.UnbilledCons_MetConsBulkWatSupExpM3_D6,
                UnauthCons_IllegalConnDomEstNo_D6 = wbEasyCalcData.UnauthCons_IllegalConnDomEstNo_D6,
                UnauthCons_IllegalConnDomErrorMargin_F6 = wbEasyCalcData.UnauthCons_IllegalConnDomErrorMargin_F6,
                UnauthCons_IllegalConnDomPersPerHouse_H6 = wbEasyCalcData.UnauthCons_IllegalConnDomPersPerHouse_H6,
                UnauthCons_IllegalConnDomConsLitPerPersDay_J6 = wbEasyCalcData.UnauthCons_IllegalConnDomConsLitPerPersDay_J6,
                UnauthCons_IllegalConnOthersErrorMargin_F10 = wbEasyCalcData.UnauthCons_IllegalConnOthersErrorMargin_F10,
                UnauthCons_MeterTampBypEtcEstNo_D14 = wbEasyCalcData.UnauthCons_MeterTampBypEtcEstNo_D14,
                UnauthCons_MeterTampBypEtcErrorMargin_F14 = wbEasyCalcData.UnauthCons_MeterTampBypEtcErrorMargin_F14,
                UnauthCons_MeterTampBypEtcConsLitPerCustDay_J14 = wbEasyCalcData.UnauthCons_MeterTampBypEtcConsLitPerCustDay_J14,
                MetErrors_DetailedManualSpec_J6 = Math.Abs(wbEasyCalcData.MetErrors_DetailedManualSpec_J6 - 2) < 0.1,
                MetErrors_BilledMetConsWoBulkSupMetUndrreg_H8 = wbEasyCalcData.MetErrors_BilledMetConsWoBulkSupMetUndrreg_H8,
                MetErrors_BilledMetConsWoBulkSupErrorMargin_N8 = wbEasyCalcData.MetErrors_BilledMetConsWoBulkSupErrorMargin_N8,
                MetErrors_MetBulkSupExpMetUnderreg_H32 = wbEasyCalcData.MetErrors_MetBulkSupExpMetUnderreg_H32,
                MetErrors_UnbillMetConsWoBulkSupplMetUndrreg_H34 = wbEasyCalcData.MetErrors_UnbillMetConsWoBulkSupplMetUndrreg_H34,
                MetErrors_CorruptMetReadPractMetUndrreg_H38 = wbEasyCalcData.MetErrors_CorruptMetReadPractMetUndrreg_H38,
                Network_DistributionAndTransmissionMains_D7 = wbEasyCalcData.Network_DistributionAndTransmissionMains_D7,
                Network_NoOfConnOfRegCustomers_H10 = wbEasyCalcData.Network_NoOfConnOfRegCustomers_H10,
                Network_NoOfInactAccountsWSvcConns_H18 = wbEasyCalcData.Network_NoOfInactAccountsWSvcConns_H18,
                Network_AvgLenOfSvcConnFromBoundaryToMeterM_H32 = wbEasyCalcData.Network_AvgLenOfSvcConnFromBoundaryToMeterM_H32,
                Prs_ApproxNoOfConn_D7 = wbEasyCalcData.Prs_ApproxNoOfConn_D7,
                Prs_DailyAvgPrsM_F7 = wbEasyCalcData.Prs_DailyAvgPrsM_F7
            };
            return easyCalcDataInput;
        }

        private static void MapEasyCalcDataOutput(WbEasyCalcData wbEasyCalcData, EasyCalcDataOutput easyCalcDataOutput)
        {
            wbEasyCalcData.SystemInputVolume_B19 = easyCalcDataOutput.SystemInputVolume_B19;
            wbEasyCalcData.SystemInputVolumeErrorMargin_B21 = easyCalcDataOutput.SystemInputVolumeErrorMargin_B21;

            wbEasyCalcData.AuthorizedConsumption_K12 = easyCalcDataOutput.AuthorizedConsumption_K12;
            wbEasyCalcData.AuthorizedConsumptionErrorMargin_K15 = easyCalcDataOutput.AuthorizedConsumptionErrorMargin_K15;
            wbEasyCalcData.WaterLosses_K29 = easyCalcDataOutput.WaterLosses_K29;
            wbEasyCalcData.WaterLossesErrorMargin_K31 = easyCalcDataOutput.WaterLossesErrorMargin_K31;

            wbEasyCalcData.BilledAuthorizedConsumption_T8 = easyCalcDataOutput.BilledAuthorizedConsumption_T8;
            wbEasyCalcData.UnbilledAuthorizedConsumption_T16 = easyCalcDataOutput.UnbilledAuthorizedConsumption_T16;
            wbEasyCalcData.UnbilledAuthorizedConsumptionErrorMargin_T20 = easyCalcDataOutput.UnbilledAuthorizedConsumptionErrorMargin_T20;
            wbEasyCalcData.CommercialLosses_T26 = easyCalcDataOutput.CommercialLosses_T26;
            wbEasyCalcData.CommercialLossesErrorMargin_T29 = easyCalcDataOutput.CommercialLossesErrorMargin_T29;
            wbEasyCalcData.PhysicalLossesM3_T34 = easyCalcDataOutput.PhysicalLossesM3_T34;
            wbEasyCalcData.PhyscialLossesErrorMargin_AH35 = easyCalcDataOutput.PhyscialLossesErrorMargin_AH35;

            wbEasyCalcData.BilledMeteredConsumption_AC4 = easyCalcDataOutput.BilledMeteredConsumption_AC4;
            wbEasyCalcData.BilledUnmeteredConsumption_AC9 = easyCalcDataOutput.BilledUnmeteredConsumption_AC9;
            wbEasyCalcData.UnbilledMeteredConsumption_AC14 = easyCalcDataOutput.UnbilledMeteredConsumption_AC14;

            wbEasyCalcData.UnbilledUnmeteredConsumption_AC19 = easyCalcDataOutput.UnbilledUnmeteredConsumption_AC19;
            wbEasyCalcData.UnbilledUnmeteredConsumptionErrorMargin_AO20 = easyCalcDataOutput.UnbilledUnmeteredConsumptionErrorMargin_AO20;
            wbEasyCalcData.UnauthorizedConsumption_AC24 = easyCalcDataOutput.UnauthorizedConsumption_AC24;
            wbEasyCalcData.UnauthorizedConsumptionErrorMargin_AO25 = easyCalcDataOutput.UnauthorizedConsumptionErrorMargin_AO25;
            wbEasyCalcData.CustomerMeterInaccuraciesAndErrorsM3_AC29 = easyCalcDataOutput.CustomerMeterInaccuraciesAndErrorsM3_AC29;
            wbEasyCalcData.CustomerMeterInaccuraciesAndErrorsErrorMargin_AO30 = easyCalcDataOutput.CustomerMeterInaccuraciesAndErrorsErrorMargin_AO30;

            wbEasyCalcData.RevenueWaterM3_AY8 = easyCalcDataOutput.RevenueWaterM3_AY8;
            wbEasyCalcData.NonRevenueWaterM3_AY24 = easyCalcDataOutput.NonRevenueWaterM3_AY24;
            wbEasyCalcData.NonRevenueWaterErrorMargin_AY26 = easyCalcDataOutput.NonRevenueWaterErrorMargin_AY26;
        }

        public static void CalculateExcel(WbEasyCalcData wbEasyCalcData)
        {
            try
            {
                EasyCalcDataInput easyCalcDataInput = MapEasyCalcDataInput(wbEasyCalcData);
                IWbEasyCalc easyCalcDataReaderMoq = new WbEasyCalc.WbEasyCalc();
                EasyCalcDataOutput easyCalcDataOutput = easyCalcDataReaderMoq.Calculate(easyCalcDataInput);
                MapEasyCalcDataOutput(wbEasyCalcData, easyCalcDataOutput);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        */
    }
}
