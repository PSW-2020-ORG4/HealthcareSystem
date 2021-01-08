using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Repository.CRUD
{
    public interface IReadCollection<T>
    {
        IEnumerable<T> GetAll();
    }
}
