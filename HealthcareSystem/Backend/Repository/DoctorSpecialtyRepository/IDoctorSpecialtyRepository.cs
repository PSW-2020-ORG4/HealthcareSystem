using System;
using System.Collections.Generic;
using System.Text;
using Backend.Model.Users;

namespace Backend.Repository.DoctorSpecialtyRepository
{
    public interface IDoctorSpecialtyRepository
    {
        List<DoctorSpecialty> GetDoctorSpecialtyBySpecialtyId(int id);
    }
}
