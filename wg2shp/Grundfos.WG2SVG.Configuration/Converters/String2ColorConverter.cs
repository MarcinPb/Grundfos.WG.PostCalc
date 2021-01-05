using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;

namespace Grundfos.WG2SVG.Configuration.Converters
{
    public class String2ColorConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return (sourceType == typeof(string));
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            string colorString = (string)value;
            string[] colorStrings = colorString.Trim().Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (colorStrings.Length < 3)
            {
                throw new ArgumentException($"Provided string does not define valid color: '{colorString}'.");
            }

            int shift = colorStrings.Length > 3 ? 1 : 0;
            int a = colorStrings.Length > 3 ? int.Parse(colorStrings[0]) : 255;
            int r = int.Parse(colorStrings[0 + shift]);
            int g = int.Parse(colorStrings[1 + shift]);
            int b = int.Parse(colorStrings[2 + shift]);
            var color = Color.FromArgb(a, r, g, b);
            return color;
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return (destinationType == typeof(string));
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (!(value is Color))
            {
                return false;
            }

            var color = (Color)value;


            return null;
        }
    }
}
