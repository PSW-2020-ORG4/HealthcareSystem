using PatientService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientService.Repository
{
    public interface IPatientRepository
    {
        Patient Get(string jmbg);

        Patient GetLazy(string jmbg);
        void Update(Patient patient); 
        void Add(Patient patient);
    }
}
