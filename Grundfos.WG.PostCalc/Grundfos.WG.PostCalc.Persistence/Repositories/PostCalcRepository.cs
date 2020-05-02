using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Grundfos.WG.PostCalc.Persistence.Model;
using Grundfos.WG.PostCalc.SQLiteEf;

namespace Grundfos.WG.PostCalc.Persistence.Repositories
{
    public class PostCalcRepository : IPostCalcRepository
    {
        private readonly DatabaseContext repository;
        private readonly IMapper mapper;

        public PostCalcRepository(DatabaseContext repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public void ClearResultsByAttribute(string attribute)
        {
            var itemsToRemove = this.repository.Results.Where(x => x.Attribute.Equals(attribute, StringComparison.OrdinalIgnoreCase));
            this.repository.Results.RemoveRange(itemsToRemove);
        }

        public IList<Result> GetResultsByAttribute(string attribute)
        {
            var results = this.repository.Results.Where(x => x.Attribute.Equals(attribute, StringComparison.OrdinalIgnoreCase));
            var mapped = this.mapper.Map<IList<Result>>(results);
            return mapped;
        }

        public void SetResults(string attribute, IEnumerable<Result> results)
        {
            var mapped = this.mapper.Map<List<SQLiteEf.Model.Result>>(results);
            mapped.ForEach(x => x.Attribute = attribute);
            this.repository.Results.AddRange(mapped);
        }

        public void SetResultTimestamp(ResultTimestamp resultTimestamp)
        {
            var resultInfo = this.repository.ResultTimestamps
                .FirstOrDefault(x => x.Attribute.Equals(resultTimestamp.Attribute, StringComparison.OrdinalIgnoreCase));
            if (resultInfo == null)
            {
                resultInfo = this.repository.ResultTimestamps.Create();
                this.repository.ResultTimestamps.Add(resultInfo);
            }

            this.mapper.Map(resultTimestamp, resultInfo);
        }

        public ResultTimestamp GetResultTimestamp(string attribute)
        {
            var resultInfo = this.repository.ResultTimestamps
                .FirstOrDefault(x => x.Attribute.Equals(attribute, StringComparison.OrdinalIgnoreCase));
            if (resultInfo == null)
            {
                return null;
            }

            var mapped = this.mapper.Map<ResultTimestamp>(resultInfo);
            return mapped;
        }

        public void SaveChanges()
        {
            this.repository.SaveChanges();
        }
    }
}
