using System;
using System.Collections.Generic;
using Grundfos.GeometryModel;
using Grundfos.WO;
using Grundfos.WO.ObjectReaders;

namespace Grundfos.WG2TW.XML.ConsoleApp
{
    public class WgObjectsProvider : IDisposable
    {
        private readonly DomainDataSetProxy domainDataSet;

        public WgObjectsProvider(DomainDataSetProxy domainDataSet)
        {
            this.domainDataSet = domainDataSet;
        }

        public IList<DomainObjectData> GetWgObjects(ObjectTypes objectType)
        {
            if (objectType == ObjectTypes.Pipe)
            {
                var pipeReader = new PipeReader(this.domainDataSet);
                var pipes = pipeReader.ReadObjects(new List<string>());
                return pipes;
            }

            var reader = new GenericObjectReader(this.domainDataSet, (WO.Constants.ObjectTypes)objectType);
            var items = reader.ReadObjects(new List<string>());
            return items;
        }

        public void Dispose()
        {
            this.domainDataSet.Dispose();
        }
    }
}
