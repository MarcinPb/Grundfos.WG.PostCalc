using System;
using System.Collections.Generic;
using System.Linq;
using Grundfos.TW.LegendBuilder.Configuration;
using Grundfos.WG2SVG.Configuration;

namespace Grundfos.TW.LegendBuilder
{
    public class SymmetricalRangeDetection
    {
        private readonly double toleranceFactor;

        public SymmetricalRangeDetection(IAppSettingsManager settingsManager)
        {
            this.toleranceFactor = settingsManager.GetValue(Constants.ToleranceFactor, Constants.DefaultToleranceFactor);
        }

        public List<ValueColor> Merge(List<ValueColor> colors)
        {
            if (colors.Count == 0)
            {
                return new List<ValueColor>();
            }

            var ordered = colors.OrderByDescending(x => x.LessThan).ToList();
            var merged = new List<ValueColor>();
            while (ordered.Count > 0)
            {
                var item = ordered[0];
                var mirror = ordered.FirstOrDefault(x => AreSymmetrical(item, x));
                if (mirror != null)
                {
                    ordered.Remove(mirror);
                }

                ordered.Remove(item);
                merged.Insert(0, item);
            }

            if (merged[0].GreaterOrEqualTo < 0)
            {
                merged[0].GreaterOrEqualTo = 0;
            }

            return merged;
        }

        private bool AreSymmetrical(ValueColor a, ValueColor b)
        {
            return AbsoluteValuesAreEqualWithTolerance(a.GreaterOrEqualTo, b.LessThan) &&
                    AbsoluteValuesAreEqualWithTolerance(a.LessThan, b.GreaterOrEqualTo) &&
                    object.Equals(a.Color, b.Color);
        }

        private bool AbsoluteValuesAreEqualWithTolerance(double a, double b)
        {
            var tolerance = this.toleranceFactor * a;
            bool areEqualWithTolerance = Math.Abs(Math.Abs(a) - Math.Abs(b)) <= tolerance;
            return areEqualWithTolerance;
        }
    }
}
