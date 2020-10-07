﻿using System.Collections.Generic;

namespace DataRepository
{
    public interface IMultiDeleteListRepository<T> : IBaseListRepository<T>
    {
        bool DeleteItem(List<int> idList);
    }
}
