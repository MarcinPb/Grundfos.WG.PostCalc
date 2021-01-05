using Grundfos.TW.LegendBuilder.Configuration;
using NUnit.Framework;

namespace Grundfos.TW.LegendBuilder.Tests.Configiuration
{
    [TestFixture]
    public class ValueConverterTests
    {
        [TestCase("0.000001", 0.000001)]
        [TestCase("-0.000001", -0.000001)]
        [TestCase("-1615654.5464645", -1615654.5464645)]
        [TestCase("1.7976931348623157E+308", double.MaxValue)]
        [TestCase("-1.7976931348623157E+308", double.MinValue)]
        public void GetValue_Test(string raw, double expected)
        {
            var converter = new ValueConverter<double>();
            var value = converter.Convert(raw);
            Assert.AreEqual(expected, value);
        }
    }
}
