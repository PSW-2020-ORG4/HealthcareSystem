using System.Collections.Generic;
using UserService.DTO;
using UserService.Model;

namespace UserService.Service
{
    public interface IPatientService
    {
        void Register(PatientRegistrationDTO registrationDTO);
        void Activate(string jmbg);
        void Block(string jmbg);
        void ChangeImage(string jmbg, string imageName);
        IEnumerable<PatientAccount> GetMalicious();
        IEnumerable<PatientAccount> GetAll();
        PatientAccount Get(string jmbg);
    }
}
