using System.Collections.Generic;
using Grundfos.WG.OPC.Publisher.Configuration;

namespace Grundfos.WG.PostCalc.PressureCalculation
{
    public class ZonePressurePublisherConfiguration : PublisherConfiguration
    {
        public Dictionary<int, string> Zones { get; set; }
    }
}
