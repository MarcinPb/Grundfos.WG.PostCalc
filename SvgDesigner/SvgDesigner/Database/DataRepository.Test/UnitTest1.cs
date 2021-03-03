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
            var pipeList = MainRepo.GetPipeList2();
        }

        [TestMethod]
        public void TestMethod2()
        {
            var topLeft = MainRepo.GetPointTopLeft();
            var bottomRight = MainRepo.GetPointBottomRight();
        }
    }
}
