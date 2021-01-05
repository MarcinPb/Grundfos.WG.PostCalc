using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Grundfos.TW2WG.AttributeService;
using Grundfos.WG2SVG.Configuration;

namespace Grundfos.GeometryModel.Builders.Painters
{
    public class ColorSelectorService : IColorSelectorService
    {
        private readonly IWgAttributeService attributeService;
        private readonly List<ValueColor> colorRanges;
        private readonly AttributeColorRule brush;

        public ColorSelectorService(IWgAttributeService attributeService, AttributeColorRule brush)
        {
            this.attributeService = attributeService;
            this.colorRanges = brush.ValueColors.Cast<ValueColor>().ToList();
            this.brush = brush;
        }

        public int ObjectTypeID => this.brush.ObjectTypeID;

        public Color GetColor(DomainObjectData item)
        {
            double value = this.attributeService.GetAttributeValue(item.ID, brush.AttributeID);
            if (double.IsNaN(value))
            {
                return Color.FromArgb(0, 0, 0);
            }

            var range = this.colorRanges.FirstOrDefault(x => x.GreaterOrEqualTo <= value && value < x.LessThan);
            return range != null ? range.Color : new Color();
        }
    }
}
