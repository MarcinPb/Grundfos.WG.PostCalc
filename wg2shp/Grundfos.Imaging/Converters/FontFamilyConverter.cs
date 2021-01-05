using System;
using System.Drawing;

namespace Grundfos.Imaging.Converters
{
    public static class FontFamilyConverter
    {
        public static FontFamily Convert(Shapes.FontFamily fontFamily)
        {
            switch (fontFamily)
            {
                case Shapes.FontFamily.GenericSansSerif:
                    return FontFamily.GenericSansSerif;
                case Shapes.FontFamily.GenericSerif:
                    return FontFamily.GenericSerif;
                case Shapes.FontFamily.GenericMonospace:
                    return FontFamily.GenericMonospace;
                default:
                    throw new NotSupportedException("Unknown font family: " + fontFamily);
            }
        }
    }
}
