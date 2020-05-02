using System;
using System.Collections.Generic;
using System.Linq;
using Haestad.Domain;
using Haestad.Domain.ModelingObjects;
using Haestad.Domain.ModelingObjects.Water;

namespace Grundfos.WG.ObjectReaders
{
    public class WaterDemandPatternReader
    {
        public WaterDemandPatternReader(IDomainDataSet domainDataSet)
        {
            this.DomainDataSet = domainDataSet;
        }

        public IDomainDataSet DomainDataSet { get; }

        public Dictionary<string, int> GetPatterns()
        {
            var ids = ((IdahoDomainDataSet)this.DomainDataSet).IdahoPatternElementManager.Elements()
                .Cast<ModelingElementBase>()
                .ToDictionary(x => x.Label, x => x.Id, StringComparer.OrdinalIgnoreCase);
            ids[Constants.FixedPattern] = -1;
            return ids;
        }
    }
}
