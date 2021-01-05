using System.Configuration;
using System.IO;
using AutoMapper;
using Grundfos.TW.DataSourceMap.Wg2Opc;
using NUnit.Framework;

namespace Grundfos.TW.Tests.Wg2Opc
{
    [TestFixture]
    public class Wg2OpcMapReaderTests
    {
        [Test]
        public void Read_Test()
        {
            ConfigurationManager.AppSettings["opcTagExtractRegex"] = @"OPCTest_WG_\d\d%(?<opcTag>.*)#\d*";
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfiles(typeof(Wg2OpcMapReader).Assembly);
            });

            string fileName = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestData\KepEx.xlsx");
            var mapper = new Mapper(config);
            var reader = new Wg2OpcMapReader(fileName, mapper);

            var result = reader.Read();
            Assert.That(result.Count, Is.EqualTo(32285));
        }
    }
}
