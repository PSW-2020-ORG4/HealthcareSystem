using System.Collections.Generic;

namespace UserService.Repository.CRUD
{
    public interface IReadCollection<T>
    {
        IEnumerable<T> GetAll();
    }
}
