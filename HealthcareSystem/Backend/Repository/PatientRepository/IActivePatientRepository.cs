using Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IActivePatientRepository
    {
        Patient GetPatientByJmbg(string jmbg);
        Patient SetPatient(Patient patient);
        List<Patient> GetAllPatients();
        Patient AddPatient(Patient patient);
        Patient DeletePatient(string jmbg);
        Patient CheckUsernameAndPassword(string username, string password);
    }
}
