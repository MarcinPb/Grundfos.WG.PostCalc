using System.Collections.Generic;
using System.Linq;
using Haestad.Domain;
using Haestad.Domain.ModelingObjects.Water;

namespace Grundfos.WG.ObjectReaders
{
    public class ZoneReader
    {
        public ZoneReader(IDomainDataSet domainDataSet)
        {
            this.DomainDataSet = domainDataSet;
        }

        public IDomainDataSet DomainDataSet { get; }

        public Dictionary<int, string> GetZones()
        {
            var zones = ((IdahoDomainDataSet)this.DomainDataSet).ZoneElementManager.Elements()
                .Cast<IModelingElement>()
                .ToDictionary(x => x.Id, x => x.Label);
            return zones;
        }
    }
}
