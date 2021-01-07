/*
WPF bing maps control polylines/polygons not draw on first add to collection
https://stackoverflow.com/questions/10950421/wpf-bing-maps-control-polylines-polygons-not-draw-on-first-add-to-collection
*/

using System.Windows;
using Microsoft.Maps.MapControl.WPF;

namespace WpfApplication1.Utility.BingMapFix
{
    public static class MapLayerExtensions
    {
        private static readonly DependencyProperty ForceMeasureProperty =
            DependencyProperty.RegisterAttached("ForceMeasure",
                typeof(int),
                typeof(MapLayerExtensions),
                new FrameworkPropertyMetadata(0,
                    FrameworkPropertyMetadataOptions.AffectsRender |
                    FrameworkPropertyMetadataOptions.AffectsArrange |
                    FrameworkPropertyMetadataOptions.AffectsMeasure));

        private static int GetForceMeasure(DependencyObject mapLayer)
        {
            return (int)mapLayer.GetValue(ForceMeasureProperty);
        }
        private static void SetForceMeasure(DependencyObject mapLayer, int value)
        {
            mapLayer.SetValue(ForceMeasureProperty, value);
        }

        public static void ForceMeasure(this MapLayer mapLayer)
        {
            SetForceMeasure(mapLayer, GetForceMeasure(mapLayer) + 1);
        }
    }
}
