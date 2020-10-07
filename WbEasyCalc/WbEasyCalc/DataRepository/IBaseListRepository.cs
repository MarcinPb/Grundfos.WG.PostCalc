using System.Collections.Generic;

namespace DataRepository
{
    public interface IBaseListRepository<T>
    {
        List<T> GetList();

        T GetItem(int id);
        T SaveItem(T model);
        bool DeleteItem(int id);
    }
}
