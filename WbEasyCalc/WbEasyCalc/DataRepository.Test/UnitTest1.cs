using System;
using System.Configuration;
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
                },
                    
                //SysInput_SystemInputVolumeM3_D6 = 111111
            };
            dataRepository.WbEasyCalcDataListRepository.SaveItem(model);
            var id = model.WbEasyCalcDataId;
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
