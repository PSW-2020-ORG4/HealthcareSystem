using System.Collections.Generic;
using UserService.Model;
using UserService.Repository.CRUD;

namespace UserService.Repository
{
    public interface IDoctorRepository : IReadCollection<DoctorAccount>
    {
        IEnumerable<DoctorAccount> GetBySpecialty(int specialtyId);
    }
}
