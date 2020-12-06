using Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
