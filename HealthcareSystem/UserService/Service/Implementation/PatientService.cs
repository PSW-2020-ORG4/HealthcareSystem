using System;
using System.Collections.Generic;
using System.Linq;
using UserService.DTO;
using UserService.Mapper;
using UserService.Model;
using UserService.Model.Memento;
using UserService.Repository;

namespace UserService.Service
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly ICityRepository _cityRepository;

        public PatientService(IPatientRepository patientRepository, ICityRepository cityRepository)
        {
            _patientRepository = patientRepository;
            _cityRepository = cityRepository;
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

        public void Register(PatientRegistrationDTO registrationDTO)
        {
            PatientAccountMemento memento = registrationDTO.ToPatientAccountMemento();
            memento.IsActivated = false;
            memento.IsBlocked = false;
            memento.City = _cityRepository.Get(registrationDTO.CityId).GetMemento();
            PatientAccount patientAccount = new PatientAccount(memento);
            _patientRepository.Add(patientAccount);
        }

        public void ChangeImage(string jmbg, string imageName)
        {
            PatientAccount patientAccount = Get(jmbg);
            patientAccount.ChangeImage(imageName);
            _patientRepository.Update(patientAccount);
        }
    }
}
