using System.Collections.Generic;
using Grundfos.WG.PostCalc.Persistence.Model;

namespace Grundfos.WG.PostCalc.Persistence.Repositories
{
    public interface IPostCalcRepository
    {
        void ClearResultsByAttribute(string attribute);
        IList<Result> GetResultsByAttribute(string attribute);
        ResultTimestamp GetResultTimestamp(string attribute);
        void SaveChanges();
        void SetResults(string resultTypeName, IEnumerable<Result> results);
        void SetResultTimestamp(ResultTimestamp resultTimestamp);
    }
}