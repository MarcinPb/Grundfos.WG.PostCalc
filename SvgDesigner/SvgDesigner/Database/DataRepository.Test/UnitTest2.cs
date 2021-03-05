using System;
using System.Collections.Generic;
using Database.DataModel;
using Database.DataRepository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Database.DataRepository.Test
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void TestMethod1()
        {
            MainRepo.SaveInfra();
        }

        [TestMethod]
        public void TestMethod2()
        {
            List<InfraNodeField2> list = MainRepo.GetInfraFieldList();
        }
    }
}
