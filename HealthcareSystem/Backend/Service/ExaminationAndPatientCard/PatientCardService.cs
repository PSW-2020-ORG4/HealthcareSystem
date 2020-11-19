/***********************************************************************
 * Module:  PatientCardService.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Service.UsersService.PatientCardService
 ***********************************************************************/

using Backend.Model.Exceptions;
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
        public void CreatePatientCard(PatientCard patientCard)
        {
            _activePatientCardRepository.AddPatientCard(patientCard);
        }
        public PatientCard ViewPatientCard(string patientJmbg)
        {
            PatientCard patientCard = _activePatientCardRepository.GetPatientCard(patientJmbg);
            if (patientCard == null)
                throw new NotFoundException("Patient card with jmbg=" + patientCard.PatientJmbg + " doesn't exist in database.");
            return patientCard;
        }
        public void EditPatientCard(PatientCard patientCard)
        {
            _activePatientCardRepository.UpdatePatientCard(patientCard);
        }    
   
   }
}