using System;
using System.Configuration;

namespace Grundfos.TW.LegendBuilder.Configuration
{
    public class AppSettingsManager : IAppSettingsManager
    {
        public T GetValue<T>(string key, T defaultValue) where T : IConvertible
        {
            string rawValue = ConfigurationManager.AppSettings[key];
            if (rawValue == null)
            {
                return defaultValue;
            }

            var valueConverter = new ValueConverter<T>();
            return valueConverter.Convert(rawValue);
        }


    }
}
