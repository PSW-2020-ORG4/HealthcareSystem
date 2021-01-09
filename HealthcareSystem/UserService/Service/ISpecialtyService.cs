using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Model;

namespace UserService.Service
{
    public interface ISpecialtyService
    {
        IEnumerable<Specialty> GetAll();
    }
}
