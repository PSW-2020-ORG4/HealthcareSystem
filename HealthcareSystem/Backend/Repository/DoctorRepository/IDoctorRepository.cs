using Model.Users;
using System.Collections.Generic;

namespace Backend.Repository
{
    public interface IDoctorRepository
    {
        Doctor GetDoctorByJmbg(string jmbg);
        void SetDoctor(Doctor doctor);
        List<Doctor> GetAllDoctors();
        void AddDoctor(Doctor doctor);
        Doctor CheckUsernameAndPassword(string username, string password);
        bool CheckIfDoctorExists(string jmbg);
        List<Doctor> GetDoctorsBySpecialty(int specialtyId);
    }
}
