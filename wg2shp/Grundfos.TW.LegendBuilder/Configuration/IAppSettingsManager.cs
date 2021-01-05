using System;

namespace Grundfos.TW.LegendBuilder.Configuration
{
    public interface IAppSettingsManager
    {
        T GetValue<T>(string key, T defaultValue) where T : IConvertible;
    }
}
