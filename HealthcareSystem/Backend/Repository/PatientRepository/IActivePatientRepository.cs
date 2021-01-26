using Backend.Model.Users;
using System.Collections.Generic;

namespace Backend.Repository
{
    public interface IActivePatientRepository
    {
        Patient GetPatientByJmbg(string jmbg);
        void UpdatePatient(Patient patient);
        List<Patient> GetAllPatients();
        void AddPatient(Patient patient);
        void DeletePatient(string jmbg);
        Patient CheckUsernameAndPassword(string username, string password);
        int GetNumberOfCanceledExaminations(string jmbg);
        public Patient GetPatientByUsernameAndPassword(string username, string password);
    }
}
