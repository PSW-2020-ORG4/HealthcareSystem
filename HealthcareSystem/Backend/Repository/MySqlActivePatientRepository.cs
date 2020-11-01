using Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    class MySqlActivePatientRepository : IActivePatientRepository
    {
        public Patient AddPatient(Patient patient)
        {
            throw new NotImplementedException();
        }

        public Patient CheckUsernameAndPassword(string username, string password)
        {
            throw new NotImplementedException();
        }

        public Patient DeletePatient(string jmbg)
        {
            throw new NotImplementedException();
        }

        public List<Patient> GetAllPatients()
        {
            throw new NotImplementedException();
        }

        public Patient GetPatientByJmbg(string jmbg)
        {
            throw new NotImplementedException();
        }

        public Patient SetPatient(Patient patient)
        {
            throw new NotImplementedException();
        }
    }
}
