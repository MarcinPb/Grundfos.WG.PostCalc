using System.Collections.Generic;
using System.IO;
using System.Linq;
using Grundfos.WG2SVG.Configuration;

namespace Grundfos.TW.LegendBuilder
{
    public class LegendBuilderManager
    {
        private readonly SymmetricalRangeDetection symmetricalRangeDetection;
        private readonly LegendBuilder legendBuilder;

        public LegendBuilderManager(SymmetricalRangeDetection symmetricalRangeDetection, LegendBuilder legendBuilder)
        {
            this.symmetricalRangeDetection = symmetricalRangeDetection;
            this.legendBuilder = legendBuilder;
        }

        public void BuildTargetLegends(IEnumerable<Target> targets)
        {
            foreach (var target in targets)
            {
                this.BuildLegendsForTarget(target);
            }
        }

        private void BuildLegendsForTarget(Target target)
        {
            var ruleCollection = target.AttributeColorRules.Cast<AttributeColorRule>()
                .Select(x => new
                {
                    AttributeColorRule = x,
                    MergedColorRules = this.symmetricalRangeDetection.Merge(x.ValueColors.Cast<ValueColor>().ToList()).ToList()
                }).ToList();
            var colorPatterns = ruleCollection.Select(x => x.MergedColorRules).ToList();
            bool rulesAreEqual = RulesAreEqual(colorPatterns);
            if (!rulesAreEqual)
            {
                foreach (var rule in ruleCollection)
                {
                    var fileName = Path.Combine(
                        Path.GetDirectoryName(target.Legend.FileName),
                        $"{Path.GetFileNameWithoutExtension(target.Legend.FileName)}-{rule.AttributeColorRule.ObjectTypeID}-{rule.AttributeColorRule.AttributeID}");
                    fileName = Path.ChangeExtension(fileName, Path.GetExtension(target.Legend.FileName));
                    this.legendBuilder.BuildLegendFile(target, rule.MergedColorRules);
                }
            }
            else
            {
                this.legendBuilder.BuildLegendFile(target, ruleCollection[0].MergedColorRules);
            }
        }

        public bool RulesAreEqual(List<List<ValueColor>> colorPatterns)
        {
            if (colorPatterns.Count == 1)
            {
                return true;
            }

            var colorPattern = colorPatterns[0];
            for (int i = 1; i < colorPatterns.Count; i++)
            {
                var item = colorPatterns[i];
                if (item.Count != colorPattern.Count)
                {
                    return false;
                }

                for (int j = 0; j < item.Count; j++)
                {
                    var a = item[j];
                    var b = colorPattern[j];
                    if (a.GreaterOrEqualTo != b.GreaterOrEqualTo || a.LessThan != b.LessThan || !object.Equals(a.Color, b.Color))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

    }
}
