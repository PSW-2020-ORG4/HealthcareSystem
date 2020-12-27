using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Repository.CRUD
{
    public interface IRead<T, K>
    {
        T Get(K id);
        IEnumerable<T> GetAll();
    }
}
