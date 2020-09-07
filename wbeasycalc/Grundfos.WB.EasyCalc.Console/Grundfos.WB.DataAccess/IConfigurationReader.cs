using System;

namespace Grundfos.WB.DataAccess
{
    public interface IConfigurationReader
    {
        Configuration GetConfiguration(string zoneID);
    }
}
