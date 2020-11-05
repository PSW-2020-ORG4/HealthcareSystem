using Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class MySqlDeletedPatientRepository : IDeletedPatientRepository
    {
        public Patient AddPatient(Patient patient)
        {
            throw new NotImplementedException();
        }

        public Patient GetPatientByJmbg(string jmbg)
        {
            throw new NotImplementedException();
        }
    }
}
