using System;
using System.Globalization;

namespace Grundfos.TW.LegendBuilder.Configuration
{
    public class ValueConverter<T> where T : IConvertible
    {
        public T Convert(string raw)
        {
            T value = (T)System.Convert.ChangeType(raw, typeof(T), CultureInfo.InvariantCulture);
            return value;
        }
    }
}
