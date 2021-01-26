using Model.Users;
using System.Collections.Generic;

namespace Backend.Service
{
    public interface IDoctorService
    {
        Doctor RegisterDoctor(Doctor doctor);
        Doctor EditDoctor(Doctor doctor);
        List<Doctor> ViewDoctors();
        Doctor GetDoctorByJmbg(string jmbg);
        Doctor SignIn(string username, string password);
        bool IsUsernameValid(string username);
        bool IsPasswordValid(string password);
        List<Doctor> ViewDoctorsBySpecialty(int specialtyId);
    }
}
