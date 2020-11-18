using Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Service.UsersAndWorkingTime
{
    public interface IPatientService
    {
        Patient RegisterPatient(Patient patient);
        Patient EditPatient(Patient patient);
        List<Patient> ViewPatients();
        Patient ViewProfile(string jmbg);
        Patient SignIn(string username, string password);
        bool IsUsernameValid(string username);
        bool IsPasswordValid(string password);
    }
}
