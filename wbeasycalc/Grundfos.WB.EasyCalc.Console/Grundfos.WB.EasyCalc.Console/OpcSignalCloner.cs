using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grundfos.WB.EasyCalc.Console
{
    public class OpcSignalCloner
    {
        private readonly OPC.OpcReader client;
        private readonly Configuration configuration;

        public OpcSignalCloner(OPC.OpcReader client, Configuration configuration)
        {
            this.client = client;
            this.configuration = configuration;
        }

        public void CloneSignals()
        {
            var sourceTags = client.Browse(this.configuration.SourceGroupName, 0);
            var sourceValues = client.GetValues(sourceTags);
            foreach (var destination in this.configuration.DestinationGroupNames)
            {
                var destinationValues = sourceValues
                    .Select(x => new OPC.Model.OpcValue { Tag = x.Tag.Replace(this.configuration.SourceGroupName, destination), Value = x.Value })
                    .ToList();
                client.WriteValues(destinationValues);
            }
        }

        public class Configuration
        {
            public string SourceGroupName { get; set; }

            public string[] DestinationGroupNames { get; set; }
        }
    }
}
