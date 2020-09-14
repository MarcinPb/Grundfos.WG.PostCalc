using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Grundfos.WB.EasyCalc.Calculations;
using Grundfos.WB.EasyCalc.Calculations.Model;

namespace Grundfos.WB.EasyCalc.Calculations.Test
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



            EasyCalcDataReaderMoq easyCalcDataReaderMoq = new EasyCalcDataReaderMoq();
            EasyCalcSheetData easyCalcSheetData = easyCalcDataReaderMoq.ReadSheetData("", DateTime.Now);
            EasyCalcRefactored.GetWaterLosses(easyCalcSheetData);
            EasyCalcRefactored.GetWaterLossesErrorMargin(easyCalcSheetData);

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
    }
}
