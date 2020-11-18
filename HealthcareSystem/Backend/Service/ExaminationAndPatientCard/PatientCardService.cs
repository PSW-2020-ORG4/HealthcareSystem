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

        public PatientCardService(IActivePatientCardRepository activePatientCardRepository)
        {
            _activePatientCardRepository = activePatientCardRepository;
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