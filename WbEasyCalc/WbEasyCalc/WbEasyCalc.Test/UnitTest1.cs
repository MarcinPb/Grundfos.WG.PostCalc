using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WbEasyCalc.Model;

namespace WbEasyCalc.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ReadSheetData_Test()
        {
            Dictionary<string, double> expectedDict = new Dictionary<string, double>()
            {
                {"SystemInputVolume_B19", 6593339},
                {"SystemInputVolumeErrorMargin_B21", 0.05},

                {"AuthorizedConsumption_K12", 5642375},
                {"AuthorizedConsumptionErrorMargin_K15", 0.0},
                {"WaterLosses_K29", 950964},
                {"WaterLossesErrorMargin_K31", 0.347},

                {"BilledAuthorizedConsumption_T8", 5333026},
                {"UnbilledAuthorizedConsumption_T16", 309349},
                {"UnbilledAuthorizedConsumptionErrorMargin_T20", 0.0},
                {"CommercialLosses_T26", 345294.464},
                {"CommercialLossesErrorMargin_T29", 0.001},
                {"PhysicalLossesM3_T34", 605669.536},
                {"PhyscialLossesErrorMargin_AH35", 0.544},

                {"BilledMeteredConsumption_AC4", 5332026},
                {"BilledUnmeteredConsumption_AC9", 1000},
                {"UnbilledMeteredConsumption_AC14", 309349},

                {"UnbilledUnmeteredConsumption_AC19", 0},
                {"UnbilledUnmeteredConsumptionErrorMargin_AO20", 0.0},
                {"UnauthorizedConsumption_AC24", 5880},
                {"UnauthorizedConsumptionErrorMargin_AO25", 0.082},
                {"CustomerMeterInaccuraciesAndErrorsM3_AC29", 339414.464},
                {"CustomerMeterInaccuraciesAndErrorsErrorMargin_AO30", 0.0},

                {"RevenueWaterM3_AY8", 5333026},
                {"NonRevenueWaterM3_AY24", 1260313},
                {"NonRevenueWaterErrorMargin_AY26", 0.262},
            };



            WbEasyCalc easyCalcDataReaderMoq = new WbEasyCalc();
            EasyCalcSheetData easyCalcSheetData = easyCalcDataReaderMoq.ReadSheetData("", DateTime.Now);
            //EasyCalcRefactored.GetWaterLosses(easyCalcSheetData);
            //EasyCalcRefactored.GetWaterLossesErrorMargin(easyCalcSheetData);

            double actual;
            double expected;

            foreach (var keyValuePair in expectedDict)
            {
                actual = Math.Round((double)easyCalcSheetData.WaterBalanceSheet.GetType().GetProperty(keyValuePair.Key).GetValue(easyCalcSheetData.WaterBalanceSheet, null), 3);
                expected = keyValuePair.Value;
                Assert.AreEqual(expected, actual, $"{keyValuePair.Key}: actual = {actual}, expected = {expected}");
            }



            //actual = easyCalcSheetData.WaterBalanceSheet.SystemInputVolume_B19;
            //expected = 6593339;
            //Assert.AreEqual(expected, actual, $"actual = {actual}, expected = {expected}");

            //actual = Math.Round(easyCalcSheetData.WaterBalanceSheet.SystemInputVolumeErrorMargin_B21, 3);
            //expected = 0.05;
            //Assert.AreEqual(expected, actual, $"actual = {actual}, expected = {expected}");



            //actual = easyCalcSheetData.WaterBalanceSheet.WaterLosses_K29;
            //expected = 950964;
            //Assert.AreEqual(expected, actual, $"actual = {actual}, expected = {expected}");

            ////actual = easyCalcSheetData.WaterBalanceSheet.WaterLossesErrorMargin_K31;
            //actual = Math.Round(easyCalcSheetData.WaterBalanceSheet.WaterLossesErrorMargin_K31, 3);
            //expected = 0.347;
            //Assert.AreEqual(expected, actual, $"actual = {actual}, expected = {expected}");
        }

        [TestMethod]
        public void ReadSheetData2_Test()
        {
            Dictionary<string, double> expectedDict = new Dictionary<string, double>()
            {
                {"SystemInputVolume_B19", 6593339},
                {"SystemInputVolumeErrorMargin_B21", 0.05},

                {"AuthorizedConsumption_K12", 5642375},
                {"AuthorizedConsumptionErrorMargin_K15", 0.0},
                {"WaterLosses_K29", 950964},
                {"WaterLossesErrorMargin_K31", 0.347},

                {"BilledAuthorizedConsumption_T8", 5333026},
                {"UnbilledAuthorizedConsumption_T16", 309349},
                {"UnbilledAuthorizedConsumptionErrorMargin_T20", 0.0},
                {"CommercialLosses_T26", 345294.464},
                {"CommercialLossesErrorMargin_T29", 0.001},
                {"PhysicalLossesM3_T34", 605669.536},
                {"PhyscialLossesErrorMargin_AH35", 0.544},

                {"BilledMeteredConsumption_AC4", 5332026},
                {"BilledUnmeteredConsumption_AC9", 1000},
                {"UnbilledMeteredConsumption_AC14", 309349},

                {"UnbilledUnmeteredConsumption_AC19", 0},
                {"UnbilledUnmeteredConsumptionErrorMargin_AO20", 0.0},
                {"UnauthorizedConsumption_AC24", 5880},
                {"UnauthorizedConsumptionErrorMargin_AO25", 0.082},
                {"CustomerMeterInaccuraciesAndErrorsM3_AC29", 339414.464},
                {"CustomerMeterInaccuraciesAndErrorsErrorMargin_AO30", 0.0},

                {"RevenueWaterM3_AY8", 5333026},
                {"NonRevenueWaterM3_AY24", 1260313},
                {"NonRevenueWaterErrorMargin_AY26", 0.262},
            };


            EasyCalcDataInput easyCalcDataInput = new EasyCalcDataInput
            {
                Start_PeriodDays_M21 = 30,
                SysInput_SystemInputVolumeM3_D6 = 6593339,
                SysInput_SystemInputVolumeError_F6 = 0.05,
                BilledCons_BilledMetConsBulkWatSupExpM3_D6 = 5332026,
                BilledCons_BilledUnmetConsBulkWatSupExpM3_H6 = 1000,
                UnbilledCons_MetConsBulkWatSupExpM3_D6 = 309349,
                UnauthCons_IllegalConnDomEstNo_D6 = 100,
                UnauthCons_IllegalConnDomPersPerHouse_H6 = 3,
                UnauthCons_IllegalConnDomConsLitPerPersDay_J6 = 120,
                UnauthCons_IllegalConnDomErrorMargin_F6 = 0.05,
                UnauthCons_IllegalConnOthersErrorMargin_F10 = 0.0,
                UnauthCons_MeterTampBypEtcEstNo_D14 = 1000,
                UnauthCons_MeterTampBypEtcErrorMargin_F14 = 0.10,
                UnauthCons_MeterTampBypEtcConsLitPerCustDay_J14 = 160,
                MetErrors_DetailedManualSpec_J6 = false,
                MetErrors_BilledMetConsWoBulkSupMetUndrreg_H8 = 0.03,
                MetErrors_BilledMetConsWoBulkSupErrorMargin_N8 = 0.02,
                MetErrors_MetBulkSupExpMetUnderreg_H32 = 0.03,
                MetErrors_UnbillMetConsWoBulkSupplMetUndrreg_H34 = 0.03,
                MetErrors_CorruptMetReadPractMetUndrreg_H38 = 0.03,
                Network_DistributionAndTransmissionMains_D7 = 260.0,
                Network_NoOfConnOfRegCustomers_H10 = 8000,
                Network_NoOfInactAccountsWSvcConns_H18 = 1500,
                Network_AvgLenOfSvcConnFromBoundaryToMeterM_H32 = 7,
                Prs_ApproxNoOfConn_D7 = 9500,
                Prs_DailyAvgPrsM_F7 = 30
            };


            WbEasyCalc easyCalcDataReaderMoq = new WbEasyCalc();
            EasyCalcSheetData easyCalcSheetData = easyCalcDataReaderMoq.ReadSheetData(easyCalcDataInput);
            //EasyCalcRefactored.GetWaterLosses(easyCalcSheetData);
            //EasyCalcRefactored.GetWaterLossesErrorMargin(easyCalcSheetData);

            foreach (var keyValuePair in expectedDict)
            {
                var actual = Math.Round((double)easyCalcSheetData.WaterBalanceSheet.GetType().GetProperty(keyValuePair.Key).GetValue(easyCalcSheetData.WaterBalanceSheet, null), 3);
                var expected = keyValuePair.Value;
                Assert.AreEqual(expected, actual, $"{keyValuePair.Key}: actual = {actual}, expected = {expected}");
            }
        }

        [TestMethod]
        public void ReadSheetData3_Test()
        {
            Dictionary<string, double> expectedDict = new Dictionary<string, double>()
            {
                {"SystemInputVolume_B19", 6593339},
                {"SystemInputVolumeErrorMargin_B21", 0.05},

                {"AuthorizedConsumption_K12", 5642375},
                {"AuthorizedConsumptionErrorMargin_K15", 0.0},
                {"WaterLosses_K29", 950964},
                {"WaterLossesErrorMargin_K31", 0.347},

                {"BilledAuthorizedConsumption_T8", 5333026},
                {"UnbilledAuthorizedConsumption_T16", 309349},
                {"UnbilledAuthorizedConsumptionErrorMargin_T20", 0.0},
                {"CommercialLosses_T26", 345294.464},
                {"CommercialLossesErrorMargin_T29", 0.001},
                {"PhysicalLossesM3_T34", 605669.536},
                {"PhyscialLossesErrorMargin_AH35", 0.544},

                {"BilledMeteredConsumption_AC4", 5332026},
                {"BilledUnmeteredConsumption_AC9", 1000},
                {"UnbilledMeteredConsumption_AC14", 309349},

                {"UnbilledUnmeteredConsumption_AC19", 0},
                {"UnbilledUnmeteredConsumptionErrorMargin_AO20", 0.0},
                {"UnauthorizedConsumption_AC24", 5880},
                {"UnauthorizedConsumptionErrorMargin_AO25", 0.082},
                {"CustomerMeterInaccuraciesAndErrorsM3_AC29", 339414.464},
                {"CustomerMeterInaccuraciesAndErrorsErrorMargin_AO30", 0.0},

                {"RevenueWaterM3_AY8", 5333026},
                {"NonRevenueWaterM3_AY24", 1260313},
                {"NonRevenueWaterErrorMargin_AY26", 0.262},
            };


            EasyCalcDataInput easyCalcDataInput = new EasyCalcDataInput
            {
                Start_PeriodDays_M21 = 30,
                SysInput_SystemInputVolumeM3_D6 = 6593339,
                SysInput_SystemInputVolumeError_F6 = 0.05,
                BilledCons_BilledMetConsBulkWatSupExpM3_D6 = 5332026,
                BilledCons_BilledUnmetConsBulkWatSupExpM3_H6 = 1000,
                UnbilledCons_MetConsBulkWatSupExpM3_D6 = 309349,
                UnauthCons_IllegalConnDomEstNo_D6 = 100,
                UnauthCons_IllegalConnDomPersPerHouse_H6 = 3,
                UnauthCons_IllegalConnDomConsLitPerPersDay_J6 = 120,
                UnauthCons_IllegalConnDomErrorMargin_F6 = 0.05,
                UnauthCons_IllegalConnOthersErrorMargin_F10 = 0.0,
                UnauthCons_MeterTampBypEtcEstNo_D14 = 1000,
                UnauthCons_MeterTampBypEtcErrorMargin_F14 = 0.10,
                UnauthCons_MeterTampBypEtcConsLitPerCustDay_J14 = 160,
                MetErrors_DetailedManualSpec_J6 = false,
                MetErrors_BilledMetConsWoBulkSupMetUndrreg_H8 = 0.03,
                MetErrors_BilledMetConsWoBulkSupErrorMargin_N8 = 0.02,
                MetErrors_MetBulkSupExpMetUnderreg_H32 = 0.03,
                MetErrors_UnbillMetConsWoBulkSupplMetUndrreg_H34 = 0.03,
                MetErrors_CorruptMetReadPractMetUndrreg_H38 = 0.03,
                Network_DistributionAndTransmissionMains_D7 = 260.0,
                Network_NoOfConnOfRegCustomers_H10 = 8000,
                Network_NoOfInactAccountsWSvcConns_H18 = 1500,
                Network_AvgLenOfSvcConnFromBoundaryToMeterM_H32 = 7,
                Prs_ApproxNoOfConn_D7 = 9500,
                Prs_DailyAvgPrsM_F7 = 30
            };


            WbEasyCalc easyCalcDataReaderMoq = new WbEasyCalc();
            EasyCalcDataOutput readEasyCalcDataOutput = easyCalcDataReaderMoq.Calculate(easyCalcDataInput);

            foreach (var keyValuePair in expectedDict)
            {
                var actual = Math.Round((double)readEasyCalcDataOutput.GetType().GetProperty(keyValuePair.Key).GetValue(readEasyCalcDataOutput, null), 3);
                var expected = keyValuePair.Value;
                Assert.AreEqual(expected, actual, $"{keyValuePair.Key}: actual = {actual}, expected = {expected}");
            }
        }
    }
}
