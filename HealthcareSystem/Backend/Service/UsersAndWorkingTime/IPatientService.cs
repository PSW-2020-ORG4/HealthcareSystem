using Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Service
{
    public interface IPatientService
    {
        void RegisterPatient(Patient patient);
        List<Patient> ViewPatients();
        Patient ViewProfile(string jmbg);
        void ActivatePatientStatus(string jmbg);
        void EditPatient(Patient patient);
        void SavePatientImageName(string jmbg, string imageName);
        Patient SignIn(string username, string password);
        List<Patient> ViewMaliciousPatients();
        int GetNumberOfCanceledExaminations(string jmbg);
    }
}
