using Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Service
{
    public interface IDoctorService
    {
        Doctor RegisterDoctor(Doctor doctor);
        Doctor EditDoctor(Doctor doctor);
        List<Doctor> ViewDoctors();
        Doctor ViewProfile(string jmbg);
        Doctor SignIn(string username, string password);
        bool IsUsernameValid(string username);
        bool IsPasswordValid(string password);
        List<Doctor> ViewDoctorsBySpecialty(int specialtyId);
    }
}
