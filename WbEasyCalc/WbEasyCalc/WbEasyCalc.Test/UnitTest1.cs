using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WbEasyCalc;
using WbEasyCalcRepository.Model;

namespace WbEasyCalcRepository.Test
{
    [TestClass]
    public class UnitTest1
    {
        #region Base test

        private EasyCalcDataInput _easyCalcDataInput_01 = new EasyCalcDataInput
        {
            Start_PeriodDays_M21 = 30,
            SysInput_SystemInputVolumeM3_D6 = 6593339,
            SysInput_SystemInputVolumeError_F6 = 0.05,
            BilledCons_BilledMetConsBulkWatSupExpM3_D6 = 5332026,
            BilledCons_BilledUnmetConsBulkWatSupExpM3_H6 = 1000,
            UnbilledCons_MetConsBulkWatSupExpM3_D6 = 309349,

            UnbilledCons_UnbMetConsM3_D8 = 0,
            UnbilledCons_UnbUnmetConsM3_H6 = 0,
            UnbilledCons_UnbUnmetConsError_J6 = 0,

            UnauthCons_IllegalConnDomEstNo_D6 = 100,
            UnauthCons_IllegalConnDomPersPerHouse_H6 = 3,
            UnauthCons_IllegalConnDomConsLitPerPersDay_J6 = 120,
            UnauthCons_IllegalConnDomErrorMargin_F6 = 0.05,
            UnauthCons_IllegalConnOthersErrorMargin_F10 = 0.0,
            UnauthCons_MeterTampBypEtcEstNo_D14 = 1000,
            UnauthCons_MeterTampBypEtcErrorMargin_F14 = 0.10,
            UnauthCons_MeterTampBypEtcConsLitPerCustDay_J14 = 160,

            IllegalConnectionsOthersEstimatedNumber_D10 = 0,
            IllegalConnectionsOthersConsumptionLitersPerConnectionPerDay_J10 = 0,

            MetErrors_DetailedManualSpec_J6 = false,
            MetErrors_BilledMetConsWoBulkSupMetUndrreg_H8 = 0.03,
            MetErrors_BilledMetConsWoBulkSupErrorMargin_N8 = 0.02,

            MeteredBulkSupplyExportErrorMargin_N32 = 0.0,
            UnbilledMeteredConsumptionWithoutBulkSupplyErrorMargin_N34 = 0.0,
            CorruptMeterReadingPracticessErrorMargin_N38 = 0.0,
            DataHandlingErrorsOffice_L40 = 0.0,
            DataHandlingErrorsOfficeErrorMargin_N40 = 0.0,

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

        private Dictionary<string, double> _expectedDict_01 = new Dictionary<string, double>()
        {
            {"SystemInputVolume_B19", 6593339},
            {"SystemInputVolumeErrorMargin_B21", 0.05},

            {"AuthorizedConsumption_K12", 5642375},                             // 5642375
            {"AuthorizedConsumptionErrorMargin_K15", 0.0},
            {"WaterLosses_K29", 950964},                                        // 950964
            {"WaterLossesErrorMargin_K31", 0.347},

            {"BilledAuthorizedConsumption_T8", 5333026},
            {"UnbilledAuthorizedConsumption_T16", 309349},                      // 309349
            {"UnbilledAuthorizedConsumptionErrorMargin_T20", 0.0},
            {"CommercialLosses_T26", 345294.464},
            {"CommercialLossesErrorMargin_T29", 0.001},
            {"PhysicalLossesM3_T34", 605669.536},                               // 605669.536
            {"PhyscialLossesErrorMargin_AH35", 0.544},                          // 0.544

            {"BilledMeteredConsumption_AC4", 5332026},
            {"BilledUnmeteredConsumption_AC9", 1000},
            {"UnbilledMeteredConsumption_AC14", 309349},                        

            {"UnbilledUnmeteredConsumption_AC19", 0},                           // 0
            {"UnbilledUnmeteredConsumptionErrorMargin_AO20", 0.0},              // 0.0
            {"UnauthorizedConsumption_AC24", 5880},
            {"UnauthorizedConsumptionErrorMargin_AO25", 0.082},
            {"CustomerMeterInaccuraciesAndErrorsM3_AC29", 339414.464},
            {"CustomerMeterInaccuraciesAndErrorsErrorMargin_AO30", 0.0},

            {"RevenueWaterM3_AY8", 5333026},
            {"NonRevenueWaterM3_AY24", 1260313},
            {"NonRevenueWaterErrorMargin_AY26", 0.262},

            {"AverageSupplyTimeHPerDayBestEstimate_F9", 24.0},
            {"AveragePressureMBestEstimate_F11", 30.0},
        };

        #endregion

        [TestMethod]
        public void ReadSheetData_Test_01()
        {
            ReadSheetData_Test(_easyCalcDataInput_01, _expectedDict_01);
        }

        [TestMethod]
        public void ReadSheetData_Test_02()
        {
            EasyCalcDataInput _easyCalcDataInputTemp = (EasyCalcDataInput)_easyCalcDataInput_01.Clone();

            _easyCalcDataInputTemp.BilledCons_UnbMetConsM3_D8 = 3000;
            _easyCalcDataInputTemp.BilledCons_UnbUnmetConsM3_H8 = 2000;
            _easyCalcDataInputTemp.UnbilledCons_UnbMetConsM3_D8 = 3000;
            _easyCalcDataInputTemp.UnbilledCons_UnbUnmetConsM3_H6 = 2000;
            _easyCalcDataInputTemp.UnbilledCons_UnbUnmetConsError_J6 = 0.05;

            var dictTemp = CloneDictionaryCloningValues<string, double>(_expectedDict_01);


            //dictTemp["SystemInputVolume_B19"] = 6593339;                         
            //dictTemp["SystemInputVolumeErrorMargin_B21"] = 0.05;                                    

            dictTemp["AuthorizedConsumption_K12"] = 5652375;                         
            //dictTemp["AuthorizedConsumptionErrorMargin_K15"] = 0.0;                         
            dictTemp["WaterLosses_K29"] = 940964;                                    
            dictTemp["WaterLossesErrorMargin_K31"] = 0.35; 
            
            dictTemp["BilledAuthorizedConsumption_T8"] = 5338026;                  
            dictTemp["UnbilledAuthorizedConsumption_T16"] = 314349;                  
            //dictTemp["UnbilledAuthorizedConsumptionErrorMargin_T20"] = 0.0;            
            dictTemp["CommercialLosses_T26"] = 345634.67;                              
            //dictTemp["CommercialLossesErrorMargin_T29"] = 0.001;                       
            dictTemp["PhysicalLossesM3_T34"] = 595329.33;                           
            dictTemp["PhyscialLossesErrorMargin_AH35"] = 0.554;
        
            dictTemp["BilledMeteredConsumption_AC4"] = 5335026;            
            dictTemp["BilledUnmeteredConsumption_AC9"] = 3000;            
            dictTemp["UnbilledMeteredConsumption_AC14"] = 312349;

            dictTemp["UnbilledUnmeteredConsumption_AC19"] = 2000;                    
            dictTemp["UnbilledUnmeteredConsumptionErrorMargin_AO20"] = 0.05;
            //dictTemp["UnauthorizedConsumption_AC24"] = 5880;         
            //dictTemp["UnauthorizedConsumptionErrorMargin_AO25"] = 0.082;         
            dictTemp["CustomerMeterInaccuraciesAndErrorsM3_AC29"] = 339754.67;         
            dictTemp["CustomerMeterInaccuraciesAndErrorsErrorMargin_AO30"] = 0.0;

            dictTemp["RevenueWaterM3_AY8"] = 5338026;         
            dictTemp["NonRevenueWaterM3_AY24"] = 1255313;
            dictTemp["NonRevenueWaterErrorMargin_AY26"] = 0.263;         

            ReadSheetData_Test(_easyCalcDataInputTemp, dictTemp);
        }



        [TestMethod]
        public void ReadSheetData_Test_03()
        {
            EasyCalcDataInput _easyCalcDataInputTemp = (EasyCalcDataInput)_easyCalcDataInput_01.Clone();

            _easyCalcDataInputTemp.BilledCons_UnbMetConsM3_D8 = 3000;
            _easyCalcDataInputTemp.BilledCons_UnbUnmetConsM3_H8 = 2000;
            _easyCalcDataInputTemp.UnbilledCons_UnbMetConsM3_D8 = 3000;
            _easyCalcDataInputTemp.UnbilledCons_UnbUnmetConsM3_H6 = 2000;
            _easyCalcDataInputTemp.UnbilledCons_UnbUnmetConsError_J6 = 0.05;

            _easyCalcDataInputTemp.IllegalConnectionsOthersEstimatedNumber_D10 = 500;
            _easyCalcDataInputTemp.UnauthCons_IllegalConnOthersErrorMargin_F10 = 0.05;
            _easyCalcDataInputTemp.IllegalConnectionsOthersConsumptionLitersPerConnectionPerDay_J10 = 200;

            var dictTemp = CloneDictionaryCloningValues<string, double>(_expectedDict_01);


            //dictTemp["SystemInputVolume_B19"] = 6593339;                         
            //dictTemp["SystemInputVolumeErrorMargin_B21"] = 0.05;                                    

            dictTemp["AuthorizedConsumption_K12"] = 5652375;
            //dictTemp["AuthorizedConsumptionErrorMargin_K15"] = 0.0;                         
            dictTemp["WaterLosses_K29"] = 940964;
            dictTemp["WaterLossesErrorMargin_K31"] = 0.35;

            dictTemp["BilledAuthorizedConsumption_T8"] = 5338026;                  
            dictTemp["UnbilledAuthorizedConsumption_T16"] = 314349;
            //dictTemp["UnbilledAuthorizedConsumptionErrorMargin_T20"] = 0.0;            
            dictTemp["CommercialLosses_T26"] = 348634.67;
            //dictTemp["CommercialLossesErrorMargin_T29"] = 0.001;                       
            dictTemp["PhysicalLossesM3_T34"] = 592329.33;
            dictTemp["PhyscialLossesErrorMargin_AH35"] = 0.557;

            dictTemp["BilledMeteredConsumption_AC4"] = 5335026;            
            dictTemp["BilledUnmeteredConsumption_AC9"] = 3000;            
            dictTemp["UnbilledMeteredConsumption_AC14"] = 312349;

            dictTemp["UnbilledUnmeteredConsumption_AC19"] = 2000;
            dictTemp["UnbilledUnmeteredConsumptionErrorMargin_AO20"] = 0.05;
            dictTemp["UnauthorizedConsumption_AC24"] = 8880;
            dictTemp["UnauthorizedConsumptionErrorMargin_AO25"] = 0.057;         
            dictTemp["CustomerMeterInaccuraciesAndErrorsM3_AC29"] = 339754.67;
            dictTemp["CustomerMeterInaccuraciesAndErrorsErrorMargin_AO30"] = 0.0;

            dictTemp["RevenueWaterM3_AY8"] = 5338026;         
            dictTemp["NonRevenueWaterM3_AY24"] = 1255313;         
            dictTemp["NonRevenueWaterErrorMargin_AY26"] = 0.263;         

            ReadSheetData_Test(_easyCalcDataInputTemp, dictTemp);
        }

        [TestMethod]
        public void ReadSheetData_Test_04_1()
        {
            EasyCalcDataInput _easyCalcDataInputTemp = (EasyCalcDataInput)_easyCalcDataInput_01.Clone();

            _easyCalcDataInputTemp.BilledCons_UnbMetConsM3_D8 = 3000;
            _easyCalcDataInputTemp.BilledCons_UnbUnmetConsM3_H8 = 2000;
            _easyCalcDataInputTemp.UnbilledCons_UnbMetConsM3_D8 = 3000;
            _easyCalcDataInputTemp.UnbilledCons_UnbUnmetConsM3_H6 = 2000;
            _easyCalcDataInputTemp.UnbilledCons_UnbUnmetConsError_J6 = 0.05;

            _easyCalcDataInputTemp.IllegalConnectionsOthersEstimatedNumber_D10 = 500;
            _easyCalcDataInputTemp.UnauthCons_IllegalConnOthersErrorMargin_F10 = 0.05;
            _easyCalcDataInputTemp.IllegalConnectionsOthersConsumptionLitersPerConnectionPerDay_J10 = 200;

            _easyCalcDataInputTemp.MeteredBulkSupplyExportErrorMargin_N32 = 0.02;
            _easyCalcDataInputTemp.UnbilledMeteredConsumptionWithoutBulkSupplyErrorMargin_N34 = 0.02;
            _easyCalcDataInputTemp.CorruptMeterReadingPracticessErrorMargin_N38 = 0.02;
            _easyCalcDataInputTemp.DataHandlingErrorsOffice_L40 = 100;
            _easyCalcDataInputTemp.DataHandlingErrorsOfficeErrorMargin_N40 = 0.02;

            var dictTemp = CloneDictionaryCloningValues<string, double>(_expectedDict_01);

            //dictTemp["SystemInputVolume_B19"] = 6593339;                         
            //dictTemp["SystemInputVolumeErrorMargin_B21"] = 0.05;                                    

            dictTemp["AuthorizedConsumption_K12"] = 5652375;
            //dictTemp["AuthorizedConsumptionErrorMargin_K15"] = 0.0;                         
            dictTemp["WaterLosses_K29"] = 940964;
            dictTemp["WaterLossesErrorMargin_K31"] = 0.35;

            dictTemp["BilledAuthorizedConsumption_T8"] = 5338026;                  
            dictTemp["UnbilledAuthorizedConsumption_T16"] = 314349;
            //dictTemp["UnbilledAuthorizedConsumptionErrorMargin_T20"] = 0.0;            
            dictTemp["CommercialLosses_T26"] = 348734.67;
            dictTemp["CommercialLossesErrorMargin_T29"] = 0.014;                       
            dictTemp["PhysicalLossesM3_T34"] = 592229.33;
            dictTemp["PhyscialLossesErrorMargin_AH35"] = 0.557;

            dictTemp["BilledMeteredConsumption_AC4"] = 5335026;
            dictTemp["BilledUnmeteredConsumption_AC9"] = 3000;            
            dictTemp["UnbilledMeteredConsumption_AC14"] = 312349;

            dictTemp["UnbilledUnmeteredConsumption_AC19"] = 2000;
            dictTemp["UnbilledUnmeteredConsumptionErrorMargin_AO20"] = 0.05;
            dictTemp["UnauthorizedConsumption_AC24"] = 8880;
            dictTemp["UnauthorizedConsumptionErrorMargin_AO25"] = 0.057;         
            dictTemp["CustomerMeterInaccuraciesAndErrorsM3_AC29"] = 339854.67;
            dictTemp["CustomerMeterInaccuraciesAndErrorsErrorMargin_AO30"] = 0.014;

            dictTemp["RevenueWaterM3_AY8"] = 5338026;         
            dictTemp["NonRevenueWaterM3_AY24"] = 1255313;         
            dictTemp["NonRevenueWaterErrorMargin_AY26"] = 0.263;         

            ReadSheetData_Test(_easyCalcDataInputTemp, dictTemp);
        }

        [TestMethod]
        public void ReadSheetData_Test_04_2()
        {
            EasyCalcDataInput _easyCalcDataInputTemp = (EasyCalcDataInput)_easyCalcDataInput_01.Clone();

            _easyCalcDataInputTemp.BilledCons_UnbMetConsM3_D8 = 3000;
            _easyCalcDataInputTemp.BilledCons_UnbUnmetConsM3_H8 = 2000;
            _easyCalcDataInputTemp.UnbilledCons_UnbMetConsM3_D8 = 3000;
            _easyCalcDataInputTemp.UnbilledCons_UnbUnmetConsM3_H6 = 2000;
            _easyCalcDataInputTemp.UnbilledCons_UnbUnmetConsError_J6 = 0.05;

            _easyCalcDataInputTemp.IllegalConnectionsOthersEstimatedNumber_D10 = 500;
            _easyCalcDataInputTemp.UnauthCons_IllegalConnOthersErrorMargin_F10 = 0.05;
            _easyCalcDataInputTemp.IllegalConnectionsOthersConsumptionLitersPerConnectionPerDay_J10 = 200;

            _easyCalcDataInputTemp.MetErrors_DetailedManualSpec_J6 = true;
            _easyCalcDataInputTemp.MeteredBulkSupplyExportErrorMargin_N32 = 0.02;
            _easyCalcDataInputTemp.UnbilledMeteredConsumptionWithoutBulkSupplyErrorMargin_N34 = 0.02;
            _easyCalcDataInputTemp.CorruptMeterReadingPracticessErrorMargin_N38 = 0.02;
            _easyCalcDataInputTemp.DataHandlingErrorsOffice_L40 = 100;
            _easyCalcDataInputTemp.DataHandlingErrorsOfficeErrorMargin_N40 = 0.02;

            var dictTemp = CloneDictionaryCloningValues<string, double>(_expectedDict_01);

            //dictTemp["SystemInputVolume_B19"] = 6593339;                         
            //dictTemp["SystemInputVolumeErrorMargin_B21"] = 0.05;                                    

            dictTemp["AuthorizedConsumption_K12"] = 5652375;
            //dictTemp["AuthorizedConsumptionErrorMargin_K15"] = 0.0;                         
            dictTemp["WaterLosses_K29"] = 940964;
            dictTemp["WaterLossesErrorMargin_K31"] = 0.35;

            dictTemp["BilledAuthorizedConsumption_T8"] = 5338026;                  
            dictTemp["UnbilledAuthorizedConsumption_T16"] = 314349;
            //dictTemp["UnbilledAuthorizedConsumptionErrorMargin_T20"] = 0.0;            
            dictTemp["CommercialLosses_T26"] = 348641.887;
            dictTemp["CommercialLossesErrorMargin_T29"] = 0.014;                       
            dictTemp["PhysicalLossesM3_T34"] = 592322.113;
            dictTemp["PhyscialLossesErrorMargin_AH35"] = 0.557;

            dictTemp["BilledMeteredConsumption_AC4"] = 5335026;
            dictTemp["BilledUnmeteredConsumption_AC9"] = 3000;            
            dictTemp["UnbilledMeteredConsumption_AC14"] = 312349;

            dictTemp["UnbilledUnmeteredConsumption_AC19"] = 2000;
            dictTemp["UnbilledUnmeteredConsumptionErrorMargin_AO20"] = 0.05;
            dictTemp["UnauthorizedConsumption_AC24"] = 8880;
            dictTemp["UnauthorizedConsumptionErrorMargin_AO25"] = 0.057;         
            dictTemp["CustomerMeterInaccuraciesAndErrorsM3_AC29"] = 339761.887;
            dictTemp["CustomerMeterInaccuraciesAndErrorsErrorMargin_AO30"] = 0.014;

            dictTemp["RevenueWaterM3_AY8"] = 5338026;         
            dictTemp["NonRevenueWaterM3_AY24"] = 1255313;         
            dictTemp["NonRevenueWaterErrorMargin_AY26"] = 0.263;         

            ReadSheetData_Test(_easyCalcDataInputTemp, dictTemp);
        }


        private void ReadSheetData_Test(EasyCalcDataInput easyCalcDataInput, Dictionary<string, double> expectedDict)
        {
            WbEasyCalcRepository.WbEasyCalc easyCalcDataReaderMoq = new WbEasyCalcRepository.WbEasyCalc();
            EasyCalcDataOutput readEasyCalcDataOutput = easyCalcDataReaderMoq.Calculate(easyCalcDataInput);

            foreach (var keyValuePair in expectedDict)
            {
                var actual = Math.Round((double)readEasyCalcDataOutput.GetType().GetProperty(keyValuePair.Key).GetValue(readEasyCalcDataOutput, null), 3);
                var expected = keyValuePair.Value;
                Assert.AreEqual(expected, actual, $"{keyValuePair.Key}: actual = {actual}, expected = {expected}");
            }
        }

        private static Dictionary<TKey, TValue> CloneDictionaryCloningValues<TKey, TValue>(Dictionary<TKey, TValue> original)
        {
            Dictionary<TKey, TValue> ret = new Dictionary<TKey, TValue>(original.Count, original.Comparer);
            foreach (KeyValuePair<TKey, TValue> entry in original)
            {
                ret.Add(entry.Key, (TValue)entry.Value);
            }
            return ret;
        }

    }
}
