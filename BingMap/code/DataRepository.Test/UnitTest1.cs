using System;
using System.Collections.Generic;
using System.Linq;
using DataModel.Files;
using DataModel.Files.Osm;
using DataModel.Files.Ztm;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataRepository.Test
{
    [TestClass]
    public class UnitTest1
    {
        private readonly List <ZtmOsm> _ztmOsmList = new List<ZtmOsm>()
        {
            new ZtmOsm(new Stop(){Id=11}, new Node(){Id=21}, 0),
            new ZtmOsm(new Stop(){Id=12}, new Node(){Id=22}, 0),
            new ZtmOsm(new Stop(){Id=13}, new Node(){Id=23}, 0),
        };

        [TestMethod]
        public void TestMethod1()
        {
            var ztmOsm = _ztmOsmList.First();
            var result = Methods.IsDuplicate(ztmOsm, _ztmOsmList);

            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var ztmOsm = new ZtmOsm(new Stop() { Id = 14 }, new Node() {Id = 23}, 0);
            _ztmOsmList.Add(ztmOsm);
            var result = Methods.IsDuplicate(ztmOsm, _ztmOsmList);

            Assert.AreEqual(result, true);
        }
    }
}
