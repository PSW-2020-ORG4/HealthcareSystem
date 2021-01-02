using System.Collections.Generic;
using UserService.Model;

namespace UserService.Service
{
    public interface IDoctorService
    {
        IEnumerable<DoctorAccount> GetBySpecialty(int specialtyId);
    }
}
