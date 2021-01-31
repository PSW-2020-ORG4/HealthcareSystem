using Backend.Model.Users;
using System.Collections.Generic;

namespace Backend.Repository.DoctorSpecialtyRepository
{
    public interface IDoctorSpecialtyRepository
    {
        List<DoctorSpecialty> GetDoctorSpecialtyBySpecialtyId(int id);
    }
}
