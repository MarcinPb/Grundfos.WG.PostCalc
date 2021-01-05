using System;
using System.Drawing;
using Grundfos.GeometryModel;
using Grundfos.GeometryModel.Builders.Painters;
using Grundfos.WG2SVG.Configuration;

namespace Grundfos.WG2SVG.ConsoleApp.Painters
{
    public class LabelToColorService : IColorService
    {
        private readonly Color defaultColor;
        private readonly LabelColorRule rules;

        public LabelToColorService(Color defaultColor, LabelColorRule rules)
        {
            this.defaultColor = defaultColor;
            this.rules = rules;
        }

        public Color GetColor(DomainObjectData item)
        {
            for (int i = 0; i < this.rules.Count; i++)
            {
                if (item.Label.IndexOf(this.rules[i].LabelToken, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return rules[i].Color;
                }
            }

            return this.defaultColor;
        }
    }
}
