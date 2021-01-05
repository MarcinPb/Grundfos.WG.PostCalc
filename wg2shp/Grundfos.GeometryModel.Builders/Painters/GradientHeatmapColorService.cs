using System;
using System.Drawing;
using Grundfos.GeometryModel;
using Grundfos.TW2WG.AttributeService;

namespace Grundfos.GeometryModel.Builders.Painters
{
    public class GradientHeatmapColorService : IColorService
    {
        private readonly IWgAttributeService attributeService;
        private readonly GradientColorHeatMap heatMap;
        private readonly double min;
        private readonly double max;

        public GradientHeatmapColorService(IWgAttributeService attributeService, GradientColorHeatMap heatMap, double min, double max)
        {
            this.attributeService = attributeService;
            this.heatMap = heatMap;
            this.min = min;
            this.max = max;
        }

        public Color GetColor(DomainObjectData item)
        {
            double flowValue = this.attributeService.GetAttributeValue(item.ID, (int)WgAttributes.PipeFlow);
            if (double.IsNaN(flowValue))
            {
                return Color.FromArgb(0, 0, 0);
            }

            double absoluteValue = Math.Abs(flowValue);
            return this.heatMap.GetColorForValue(this.min, this.max, absoluteValue);
        }
    }
}
