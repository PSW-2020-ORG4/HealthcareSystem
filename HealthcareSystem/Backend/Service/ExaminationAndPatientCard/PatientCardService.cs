/***********************************************************************
 * Module:  PatientCardService.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Service.UsersService.PatientCardService
 ***********************************************************************/

using Backend.Model.Exceptions;
using Backend.Repository;
using Backend.Service;
using Model.Users;
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
            try
            {
                _activePatientCardRepository.AddPatientCard(patientCard);
            }
            catch (Exception)
            {
                throw new DatabaseException("Patient card with id=" + patientCard.Id + " already exists in database.");
            }
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