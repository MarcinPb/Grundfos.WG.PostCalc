using System.Collections.Generic;
using System.Data;
using Grundfos.OPC.Model;
using Grundfos.WB.ReportConnector.Mapping;
using Grundfos.WB.ReportConnector.Repository;
using NLog;

namespace Grundfos.WB.ReportConnector
{
    public class ZoneDataReader
    {
        private static readonly ILogger log = LogManager.GetCurrentClassLogger();
        private readonly IRepository<DataRow> repository;
        private readonly IMapper<ICollection<DataRow>, ICollection<OpcValue>> mapper;

        public ZoneDataReader(IRepository<DataRow> repository, IMapper<ICollection<DataRow>, ICollection<OpcValue>> mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public ICollection<OpcValue> Get()
        {
            var rows = this.repository.GetAll();
            var results = this.mapper.Map(rows);
            return results;
        }
    }
}
