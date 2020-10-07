using System;
using DataModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataRepository.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            GlobalConfig.InitializeConnection(DatabaseType.Sql);
            DataModel.WbEasyCalcData model = new DataModel.WbEasyCalcData()
            {
                WbEasyCalcDataId = 0,
                ZoneId = 1,
                YearNo = 1,
                MonthNo = 1,
                Description = "Desc 1",

                Start_PeriodDays_M21 = 30,
                SysInput_SystemInputVolumeM3_D6 = 111111
            };
            GlobalConfig.WbEasyCalcDataRepo.SaveItem(model);
            var id = model.WbEasyCalcDataId;
        }

        [TestMethod]
        public void TestMethod2()
        {
            GlobalConfig.InitializeConnection(DatabaseType.Sql);
            var list = GlobalConfig.WbEasyCalcDataRepo.GetList();
        }
    }
}
