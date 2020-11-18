/***********************************************************************
 * Module:  PatientCardService.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Service.UsersService.PatientCardService
 ***********************************************************************/

using Model.Users;
using Repository;
using System;

namespace Service.ExaminationAndPatientCard
{
   public class PatientCardService
   {
        private IActivePatientCardRepository _activePatientCardRepository;
        private IDeletedPatientCardRepository _deletedPatientCardRepository;

        public PatientCardService(IActivePatientCardRepository activePatientCardRepository,IDeletedPatientCardRepository deletedPatientCardRepository)
        {
            _activePatientCardRepository = activePatientCardRepository;
            _deletedPatientCardRepository = deletedPatientCardRepository;
        }
        public void DeletePatientCard(string patientJmbg)
        {
            PatientCard patientCard = _activePatientCardRepository.GetPatientCard(patientJmbg);
            _activePatientCardRepository.DeletePatientCard(patientJmbg);
            _deletedPatientCardRepository.AddPatientCard(patientCard);
        }
      
        public PatientCard ViewPatientCard(string patientJmbg)
        {
            return _activePatientCardRepository.GetPatientCard(patientJmbg);
        }
      
        public void EditPatientCard(PatientCard patientCard)
        {
            _activePatientCardRepository.SetPatientCard(patientCard);
        }
      
      public void CreatePatientCard(PatientCard patientCard)
      {
            _activePatientCardRepository.AddPatientCard(patientCard);
      }
   
   }
}