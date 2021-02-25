using System;
using Database.DataRepository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Database.DataRepository.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var pipeList = MainRepo.GetPipeList();
        }

        [TestMethod]
        public void TestMethod2()
        {
            var topLeft = MainRepo.GetPointTopLeft();
            var bottomRight = MainRepo.GetPointBottomRight();
        }

        [TestMethod]
        public void TestMethod3()
        {
            var list = MainRepo.GetJunctionRecalcList(2000, 1000);
        }

        //[TestMethod]
        //public void TestMethod4()
        //{
        //    var list = MainRepo.GetPipeRecalcList(2000, 1000);
        //}
    }
}
