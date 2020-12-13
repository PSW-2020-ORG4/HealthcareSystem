using System;
using System.Collections.Generic;
using System.Text;
using Backend.Model.Users;

namespace Backend.Service.UsersAndWorkingTime
{
    public interface IDoctorSpecialtyService
    {
        List<DoctorSpecialty> GetDoctorSpecialtyBySpecialtyId(int id);
    }
}
