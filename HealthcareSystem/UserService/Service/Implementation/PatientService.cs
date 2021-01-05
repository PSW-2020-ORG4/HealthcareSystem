using System;
using System.Collections.Generic;
using System.Linq;
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
            PatientAccount patientAccount = Get(jmbg);
            patientAccount.Activate();
            _patientRepository.Update(patientAccount);
        }

        public void Block(string jmbg)
        {
            DateTime earliestMaliciousAction = DateTime.Now.AddMonths(-1);
            PatientAccount patientAccount = _patientRepository.Get(jmbg, earliestMaliciousAction);
            patientAccount.Block();
            _patientRepository.Update(patientAccount);
        }

        public IEnumerable<PatientAccount> GetAll()
        {
            return _patientRepository.GetAll();
        }

        public PatientAccount Get(string jmbg)
        {
            return _patientRepository.Get(jmbg);
        }

        public IEnumerable<PatientAccount> GetMalicious()
        {
            DateTime earliestMaliciousAction = DateTime.Now.AddMonths(-1);
            IEnumerable<PatientAccount> patientAccounts = _patientRepository.GetAll(earliestMaliciousAction);

            return patientAccounts.Where(patient => patient.IsMalicious());
        }

        public void Register(PatientAccount patientAccount)
        {
            _patientRepository.Add(patientAccount);
        }
    }
}
