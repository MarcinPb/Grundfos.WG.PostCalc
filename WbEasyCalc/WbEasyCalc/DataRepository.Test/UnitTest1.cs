﻿using System;
using System.Configuration;
using System.Linq;
using DataModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WbEasyCalcModel;

namespace DataRepository.Test
{
    [TestClass]
    public class UnitTest1
    {
        string _cnnString = ConfigurationManager.ConnectionStrings["TWDB"].ConnectionString;



        [TestMethod]
        public void TestMethod1()
        {
            DataRepository dataRepository = new DataRepository(_cnnString);

            //GlobalConfig.InitializeConnection(DatabaseType.Sql);
            DataModel.WbEasyCalcData model = new DataModel.WbEasyCalcData()
            {
                WbEasyCalcDataId = 0,
                ZoneId = 1,
                YearNo = 1,
                MonthNo = 1,
                Description = "Desc 1",
                EasyCalcModel = new EasyCalcModel
                { 
                    StartModel = new WbEasyCalcModel.WbEasyCalc.StartModel
                    {
                        Start_PeriodDays_M21 = 30,
                    }, 
                    SysInputModel = new WbEasyCalcModel.WbEasyCalc.SysInputModel
                    { 
                        SysInput_SystemInputVolumeM3_D6 = 111111,                    
                    },
                    BilledConsModel = new WbEasyCalcModel.WbEasyCalc.BilledConsModel(),
                    UnbilledConsModel = new WbEasyCalcModel.WbEasyCalc.UnbilledConsModel(),
                    UnauthConsModel = new WbEasyCalcModel.WbEasyCalc.UnauthConsModel(),
                    MetErrorsModel = new WbEasyCalcModel.WbEasyCalc.MetErrorsModel(),
                    NetworkModel = new WbEasyCalcModel.WbEasyCalc.NetworkModel(),
                    PressureModel = new WbEasyCalcModel.WbEasyCalc.PressureModel(),
                    IntermModel = new WbEasyCalcModel.WbEasyCalc.IntermModel(),
                    FinancDataModel = new WbEasyCalcModel.WbEasyCalc.FinancDataModel
                    {
                        FinancData_K6 = "PLN",
                    },
                },
                    
            };
            dataRepository.WbEasyCalcDataListRepository.SaveItem(model);          
            var wbEasyCalcDataId = model.WbEasyCalcDataId;

            var list = dataRepository.WbEasyCalcDataListRepository.GetList();
            var wbEasyCalcData = list.FirstOrDefault(x => x.WbEasyCalcDataId == wbEasyCalcDataId);

            var actual = wbEasyCalcData;
            var expected = model;
            Assert.AreEqual(wbEasyCalcData, actual, $"actual = {actual}, expected = {expected}");
        }

        [TestMethod]
        public void TestMethod2()
        {
            DataRepository dataRepository = new DataRepository("_cnnString");
            //GlobalConfig.InitializeConnection(DatabaseType.Sql);
            var list = dataRepository.WbEasyCalcDataListRepository.GetList();
        }
    }
}
