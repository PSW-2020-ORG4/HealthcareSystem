using Backend.Model.Users;
using System.Collections.Generic;

namespace Backend.Service.UsersAndWorkingTime
{
    public interface IDoctorSpecialtyService
    {
        List<DoctorSpecialty> GetDoctorSpecialtyBySpecialtyId(int id);
    }
}
