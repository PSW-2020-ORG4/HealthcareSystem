using System.Collections.Generic;
using UserService.Model;

namespace UserService.Service
{
    interface IDoctorService
    {
        IEnumerable<DoctorAccount> GetBySpecialty(int specialtyId);
    }
}
