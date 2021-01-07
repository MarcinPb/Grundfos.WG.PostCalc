using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Maps.MapControl.WPF;

namespace WpfApplication1.Helpers
{
    public static class MapHelper
    {
        public static readonly DependencyProperty StrokeThicknessProperty = DependencyProperty.RegisterAttached(
            "StrokeThickness",
            typeof(int),
            typeof(MapHelper),
            new PropertyMetadata(0, new PropertyChangedCallback(StrokeThicknessChanged))
        );

        public static void SetStrokeThickness(DependencyObject obj, int value)
        {
            obj.SetValue(StrokeThicknessProperty, value);
        }

        public static int GetStrokeThickness(DependencyObject obj)
        {
            return (int)obj.GetValue(StrokeThicknessProperty);
        }

        private static void StrokeThicknessChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            MapPolyline map = (MapPolyline)obj;
            if (map != null)
                map.StrokeThickness = (int)args.NewValue;
        }
    }
}
