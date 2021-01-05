using NUnit.Framework;

namespace Grundfos.TW.SQL.Tests
{
    [TestFixture]
    public class PageCounterTests
    {
        [TestCase(1000, 10, 100)]
        [TestCase(1000, 100, 10)]
        [TestCase(1001, 100, 11)]
        [TestCase(999, 100, 10)]
        public void GetPageCount_Tests(int recordCount, int pageSize, int expectedPageCount)
        {
            var counter = new PageCounter();
            var actual = counter.GetPageCount(recordCount, pageSize);
            Assert.AreEqual(expectedPageCount, actual);
        }
    }
}
