/***********************************************************************
 * Module:  PatientCardService.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Service.UsersService.PatientCardService
 ***********************************************************************/

using Backend.Model.Exceptions;
using Backend.Service;
using Model.Users;
using Repository;
using System;

namespace Backend.Service
{
   public class PatientCardService : IPatientCardService
   {
        private IActivePatientCardRepository _activePatientCardRepository;

        public PatientCardService(IActivePatientCardRepository activePatientCardRepository)
        {
            _activePatientCardRepository = activePatientCardRepository;
        }
        public void AddPatientCard(PatientCard patientCard)
        {
            _activePatientCardRepository.AddPatientCard(patientCard);
        }
        public PatientCard ViewPatientCard(string patientJmbg)
        {
            PatientCard patientCard = _activePatientCardRepository.GetPatientCardByJmbg(patientJmbg);
            if (patientCard == null)
                throw new NotFoundException("Patient card with jmbg=" + patientJmbg + " doesn't exist in database.");
            return patientCard;
        }
        public void EditPatientCard(PatientCard patientCard)
        {
            _activePatientCardRepository.UpdatePatientCard(patientCard);
        }    
   
   }
}