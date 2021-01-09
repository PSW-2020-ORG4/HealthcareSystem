using System.Collections.Generic;
using UserService.Model;

namespace UserService.Service
{
    public interface IPatientService
    {
        void Register(PatientAccount patientAccount);
        void Activate(string jmbg);
        void Block(string jmbg);
        IEnumerable<PatientAccount> GetMalicious();
        IEnumerable<PatientAccount> GetAll();
        PatientAccount Get(string jmbg);
    }
}
