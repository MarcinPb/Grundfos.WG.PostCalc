using System.IO;
using System.Linq;
using Grundfos.TW.DataSourceMap.Tw2Opc;
using NLog;
using NLog.Config;
using NLog.Targets;
using NUnit.Framework;

namespace Grundfos.TW.Tests.Tw2Opc
{
    [TestFixture]
    public class TwVar2IDMapReaderTests
    {
        [Test]
        public void Read_ReadingWithCodePage1250_PolishCharactersReadCorrectly()
        {
            var currentDirectory = TestContext.CurrentContext.TestDirectory;
            string fileName = Path.Combine(currentDirectory, @"TestData\TelSrv.map");

            var parser = new TwVar2IDMapReader(fileName, 1250, "<->");
            var result = parser.Read();

            Assert.That(result[15].VariableName[3], Is.EqualTo('Ś'));
        }

        [Test]
        public void Read_DistinctEntries_NumberOfLinesMatches()
        {
            var currentDirectory = TestContext.CurrentContext.TestDirectory;
            string fileName = Path.Combine(currentDirectory, @"TestData\TelSrv.map");

            var parser = new TwVar2IDMapReader(fileName, 1250, "<->");
            var result = parser.Read();

            Assert.That(result.Count, Is.EqualTo(38878));
        }

        [Test]
        public void Read_DuplicateEntries_NumberOfLinesMatches()
        {
            var currentDirectory = TestContext.CurrentContext.TestDirectory;
            string fileName = Path.Combine(currentDirectory, @"TestData\TelSrv_DuplicateEntries.map");

            var parser = new TwVar2IDMapReader(fileName, 1250, "<->");
            var result = parser.Read();

            Assert.That(result.Count, Is.EqualTo(24));
        }

        [Test]
        public void Read_IncorrectEntries_NumberOfLinesMatches()
        {
            var currentDirectory = TestContext.CurrentContext.TestDirectory;
            string fileName = Path.Combine(currentDirectory, @"TestData\TelSrv_IncorrectEntries.map");

            var parser = new TwVar2IDMapReader(fileName, 1250, "<->");
            var result = parser.Read();

            Assert.That(result.Count, Is.EqualTo(24));
        }

        [Test]
        public void Read_4DuplicateEntries_4DuplicatesAreLogged()
        {
            var configuration = new LoggingConfiguration();
            var memoryTarget = new MemoryTarget { Name = "mem" };
            configuration.AddTarget(memoryTarget);
            configuration.LoggingRules.Add(new LoggingRule("*", LogLevel.Trace, memoryTarget));
            LogManager.Configuration = configuration;
            var logger = LogManager.GetLogger("test");

            var currentDirectory = TestContext.CurrentContext.TestDirectory;
            string fileName = Path.Combine(currentDirectory, @"TestData\TelSrv_DuplicateEntries.map");

            var parser = new TwVar2IDMapReader(fileName, 1250, "<->");
            var result = parser.Read();

            //read the logs here
            var logs = memoryTarget.Logs;
            Assert.GreaterOrEqual(logs.Count(x => x.Contains("Skipped duplicate entry in line")), 4);
        }

        [Test]
        public void Read_IncorrectEntries_ErrorsAreLogged()
        {
            var configuration = new LoggingConfiguration();
            var memoryTarget = new MemoryTarget { Name = "mem" };
            configuration.AddTarget(memoryTarget);
            configuration.LoggingRules.Add(new LoggingRule("*", LogLevel.Trace, memoryTarget));
            LogManager.Configuration = configuration;
            var logger = LogManager.GetLogger("test");

            var currentDirectory = TestContext.CurrentContext.TestDirectory;
            string fileName = Path.Combine(currentDirectory, @"TestData\TelSrv_IncorrectEntries.map");

            var parser = new TwVar2IDMapReader(fileName, 1250, "<->");
            var result = parser.Read();

            var logs = memoryTarget.Logs;
            Assert.AreEqual(6, logs.Count(x => x.Contains("|TRACE|")));
        }
    }
}
