using System.Collections.Generic;

namespace Grundfos.WB.ReportConnector.Repository
{
    public interface IRepository<T>
    {
        ICollection<T> GetAll();
    }
}
