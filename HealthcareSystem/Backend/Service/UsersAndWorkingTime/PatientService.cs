/***********************************************************************
 * Module:  PatientService.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Service.Users&WorkingTimeService.PatientService
 ***********************************************************************/

using Backend.Model.Exceptions;
using Backend.Service.UsersAndWorkingTime;
using Model.Users;
using Repository;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Service.UsersAndWorkingTime
{
    public class PatientService : IPatientService
    {
        private IActivePatientRepository _activePatientRepository;
        private IDeletedPatientRepository _deletedPatientRepository;

        public PatientService(IActivePatientRepository activePatientRepository,IDeletedPatientRepository deletedPatientRepository)
        {
            _activePatientRepository = activePatientRepository;
            _deletedPatientRepository = deletedPatientRepository;
        }
        public Patient RegisterPatient(Patient patient)
        {
            if (!patient.IsGuest && (!IsUsernameValid(patient.Username) || !IsPasswordValid(patient.Password)))
            {
                throw new BadRequestException("Your username or password is incorrect. Please try again.");
            }
            _activePatientRepository.AddPatient(patient);
            return patient;
        }

        public Patient EditPatient(Patient patient)
        {
            if ((!IsUsernameValid(patient.Username) || !IsPasswordValid(patient.Password)) && !patient.IsGuest)
            {
                return null;
            }
            _activePatientRepository.SetPatient(patient);
            return patient;
        }

        public bool DeletePatient(string jmbg)
        {
            Patient patient = _activePatientRepository.GetPatientByJmbg(jmbg);
            if (patient == null)
            {
                return false;
            }
            _activePatientRepository.DeletePatient(jmbg);
            _deletedPatientRepository.AddPatient(patient);
            return true;
        }

        public List<Patient> ViewPatients()
        {
            return _activePatientRepository.GetAllPatients();
        }

        public Patient ViewProfile(string jmbg)
        {
            return _activePatientRepository.GetPatientByJmbg(jmbg);
        }

        public Patient SignIn(string username, string password)
        {
            return _activePatientRepository.CheckUsernameAndPassword(username, password);
        }

        public bool IsUsernameValid(string username)
        {
            Regex regex = new Regex(@"^[a-zA-Z0-9\.\-_]{5,13}$");
            Match match = regex.Match(username);

            return match.Success;
        }
      
        public bool IsPasswordValid(string password)
        {
            Regex regex = new Regex(@"^[a-zA-Z0-9\.\-_]{8,30}$");
            Match match = regex.Match(password);

            return match.Success;
        }
   
   
   }
}