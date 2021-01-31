using System.Collections.Generic;
using UserService.Model;

namespace UserService.Service
{
    public interface ISpecialtyService
    {
        IEnumerable<Specialty> GetAll();
    }
}
