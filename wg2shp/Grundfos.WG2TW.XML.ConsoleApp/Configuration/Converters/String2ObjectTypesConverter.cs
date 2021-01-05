using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using Grundfos.GeometryModel;

namespace Grundfos.WG2TW.XML.ConsoleApp.Configuration.Converters
{
    public class String2ObjectTypesConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return (sourceType == typeof(string));
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            string objectTypesString = (string)value;
            string[] colorStrings = objectTypesString.Trim().Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var objectTypes = colorStrings.Select(x => (ObjectTypes)Enum.Parse(typeof(ObjectTypes), x)).ToArray();
            return objectTypes;
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return (destinationType == typeof(string));
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (!(value is ObjectTypes[]))
            {
                return false;
            }

            return null;
        }
    }
}
