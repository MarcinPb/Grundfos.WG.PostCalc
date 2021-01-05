using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Grundfos.GeometryModel;
using Grundfos.GeometryModel.Builders.Painters;
using Grundfos.TW2WG.AttributeService;
using Grundfos.WG2SVG.Configuration;

namespace Grundfos.WG2SVG.ConsoleApp.Painters
{
    public class RangeColorService : IColorService
    {
        private readonly IWgAttributeService attributeService;

        public RangeColorService(IWgAttributeService attributeService, ValueColorCollection colorRanges)
        {
            this.attributeService = attributeService;
            this.ColorRanges = colorRanges.Cast<ValueColor>().ToList();
        }

        public List<ValueColor> ColorRanges { get; set; }

        public Color GetColor(DomainObjectData item)
        {
            double flowValue = this.attributeService.GetAttributeValue(item.ID, (int)WgAttributes.PipeFlow);
            if (double.IsNaN(flowValue))
            {
                return Color.FromArgb(0, 0, 0);
            }

            var range = this.ColorRanges.FirstOrDefault(x => x.GreaterOrEqualTo <= flowValue && flowValue < x.LessThan);
            return range != null ? range.Color : new Color();
        }
    }
}
