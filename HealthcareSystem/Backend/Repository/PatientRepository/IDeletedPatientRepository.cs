using Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IDeletedPatientRepository
    {
        Patient GetPatientByJmbg(string jmbg);
        Patient AddPatient(Patient patient);

    }
}
