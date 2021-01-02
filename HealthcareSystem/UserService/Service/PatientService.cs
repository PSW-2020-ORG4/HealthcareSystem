using System.Collections.Generic;
using UserService.Model;
using UserService.Repository;

namespace UserService.Service
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        public PatientService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }
        public void Activate(string jmbg)
        {
            PatientAccount patientAccount = GetByJmbg(jmbg);
            patientAccount.Activate();
            _patientRepository.Update(patientAccount);
        }

        public void Block(string jmbg)
        {
            PatientAccount patientAccount = GetByJmbg(jmbg);
            patientAccount.Block();
            _patientRepository.Update(patientAccount);
        }

        public IEnumerable<PatientAccount> GetAll()
        {
            return _patientRepository.GetAll();
        }

        public PatientAccount GetByJmbg(string jmbg)
        {
            return _patientRepository.Get(jmbg);
        }

        public IEnumerable<PatientAccount> GetMalicious()
        {
            List<PatientAccount> maliciousPatients = new List<PatientAccount>();
            IEnumerable<PatientAccount> patientAccounts = GetAll();

            foreach (PatientAccount patientAccount in patientAccounts)
                if (patientAccount.IsMalicious())
                    maliciousPatients.Add(patientAccount);
            
            return maliciousPatients;
        }

        public void Register(PatientAccount patientAccount)
        {
            _patientRepository.Add(patientAccount);
        }
    }
}
