using System.Collections.Generic;
using System.Drawing;
using Grundfos.TW.LegendBuilder.Configuration;
using Grundfos.WG2SVG.Configuration;
using Moq;
using NUnit.Framework;

namespace Grundfos.TW.LegendBuilder.Tests
{
    [TestFixture]
    public class SymmetricalRangeDetectionTests
    {
        [Test]
        public void Merge_SymmetricalRangesEqualColors_MergedAsExpected()
        {
            var colors = new List<ValueColor>
            {
                new ValueColor { GreaterOrEqualTo = -10000, LessThan = -5000 },
                new ValueColor { GreaterOrEqualTo = -5000, LessThan = -1000 },
                new ValueColor { GreaterOrEqualTo = -1000, LessThan = -100 },
                new ValueColor { GreaterOrEqualTo = -100, LessThan = -1 },
                new ValueColor { GreaterOrEqualTo = -1, LessThan = -0.01 },
                new ValueColor { GreaterOrEqualTo = -0.01, LessThan = 0 },
                new ValueColor { GreaterOrEqualTo = 0, LessThan = 0.01 },
                new ValueColor { GreaterOrEqualTo = 0.01, LessThan = 1 },
                new ValueColor { GreaterOrEqualTo = 1, LessThan = 100 },
                new ValueColor { GreaterOrEqualTo = 100, LessThan = 1000 },
                new ValueColor { GreaterOrEqualTo = 1000, LessThan = 5000 },
                new ValueColor { GreaterOrEqualTo = 5000, LessThan = 10000 },
            };

            var settingsManager = new Mock<IAppSettingsManager>();
            settingsManager
                .Setup(x => x.GetValue<double>(Constants.ToleranceFactor, It.IsAny<double>()))
                .Returns(0);
            var rangeDetection = new SymmetricalRangeDetection(settingsManager.Object);

            var merged = rangeDetection.Merge(colors);
            Assert.AreEqual(6, merged.Count);
        }

        [Test]
        public void Merge_SymmetricalRangesDifferentColors_NotMerged()
        {
            var colors = new List<ValueColor>
            {
                new ValueColor { GreaterOrEqualTo = -10000, LessThan = -5000, Color = Color.FromArgb(0, 0, 1) },
                new ValueColor { GreaterOrEqualTo = -5000, LessThan = -1000, Color = Color.FromArgb(0, 0, 2) },
                new ValueColor { GreaterOrEqualTo = -1000, LessThan = -100, Color = Color.FromArgb(0, 0, 3) },
                new ValueColor { GreaterOrEqualTo = -100, LessThan = -1, Color = Color.FromArgb(0, 0, 4) },
                new ValueColor { GreaterOrEqualTo = -1, LessThan = -0.01, Color = Color.FromArgb(0, 0, 5) },
                new ValueColor { GreaterOrEqualTo = -0.01, LessThan = 0, Color = Color.FromArgb(0, 0, 6) },
                new ValueColor { GreaterOrEqualTo = 0, LessThan = 0.01 },
                new ValueColor { GreaterOrEqualTo = 0.01, LessThan = 1 },
                new ValueColor { GreaterOrEqualTo = 1, LessThan = 100 },
                new ValueColor { GreaterOrEqualTo = 100, LessThan = 1000 },
                new ValueColor { GreaterOrEqualTo = 1000, LessThan = 5000 },
                new ValueColor { GreaterOrEqualTo = 5000, LessThan = 10000 },
            };

            var settingsManager = new Mock<IAppSettingsManager>();
            settingsManager
                .Setup(x => x.GetValue<double>(Constants.ToleranceFactor, It.IsAny<double>()))
                .Returns(0);
            var rangeDetection = new SymmetricalRangeDetection(settingsManager.Object);

            var merged = rangeDetection.Merge(colors);
            Assert.AreEqual(12, merged.Count);
        }

        [Test]
        public void Merge_SymmetricalRangesEqualColorsOddNumber_MergedAsExpected_AbsoluteValues()
        {
            var colors = new List<ValueColor>
            {
                new ValueColor { GreaterOrEqualTo = -10000, LessThan = -5000 },
                new ValueColor { GreaterOrEqualTo = -5000, LessThan = -1000 },
                new ValueColor { GreaterOrEqualTo = -1000, LessThan = -100 },
                new ValueColor { GreaterOrEqualTo = -100, LessThan = -1 },
                new ValueColor { GreaterOrEqualTo = -1, LessThan = -0.01 },
                new ValueColor { GreaterOrEqualTo = -0.01, LessThan = 0.01 },
                new ValueColor { GreaterOrEqualTo = 0.01, LessThan = 1 },
                new ValueColor { GreaterOrEqualTo = 1, LessThan = 100 },
                new ValueColor { GreaterOrEqualTo = 100, LessThan = 1000 },
                new ValueColor { GreaterOrEqualTo = 1000, LessThan = 5000 },
                new ValueColor { GreaterOrEqualTo = 5000, LessThan = 10000 },
            };

            var settingsManager = new Mock<IAppSettingsManager>();
            settingsManager
                .Setup(x => x.GetValue<double>(Constants.ToleranceFactor, It.IsAny<double>()))
                .Returns(0);
            var rangeDetection = new SymmetricalRangeDetection(settingsManager.Object);

            var merged = rangeDetection.Merge(colors);
            Assert.AreEqual(6, merged.Count);
            Assert.AreEqual(0, merged[0].GreaterOrEqualTo);
        }
    }
}
