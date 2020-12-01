/***********************************************************************
 * Module:  PatientService.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Service.Users&WorkingTimeService.PatientService
 ***********************************************************************/

using Model.Users;
using Repository;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Service.UsersAndWorkingTime
{
    public class PatientService
    {

        private ActivePatientRepository activePatientRepository = new ActivePatientRepository();
        private DeletedPatientRepository deletedPatientRepository = new DeletedPatientRepository();
        public Patient RegisterPatient(Patient patient)
        {
            if (!patient.IsGuest &&  (!IsUsernameValid(patient.Username) || !IsPasswordValid(patient.Password)))
            {
                return null;
            }
            return activePatientRepository.NewPatient(patient);
        }

        public Patient EditPatient(Patient patient)
        {
            if ((!IsUsernameValid(patient.Username) || !IsPasswordValid(patient.Password)) && !patient.IsGuest)
            {
                return null;
            }
            return activePatientRepository.SetPatient(patient);
        }

        public bool DeletePatient(string jmbg)
        {
            Patient deletedPatient = activePatientRepository.DeletePatient(jmbg);
            if (deletedPatient != null)
            {
                Patient newPatient = deletedPatientRepository.NewPatient(deletedPatient);
                if (newPatient != null)
                    return true;
            }
            return false;
        }

        public List<Patient> ViewPatients()
        {
            return activePatientRepository.GetAllPatients();
        }

        public Patient ViewProfile(string jmbg)
        {
            return activePatientRepository.GetPatientByJmbg(jmbg);
        }

        public Patient SignIn(string username, string password)
        {
            return activePatientRepository.CheckUsernameAndPassword(username, password);
        }

        private bool IsUsernameValid(string username)
        {
            Regex regex = new Regex(@"^[a-zA-Z0-9\.\-_]{5,13}$");
            Match match = regex.Match(username);

            return match.Success;
        }
      
        private bool IsPasswordValid(string password)
        {
            Regex regex = new Regex(@"^[a-zA-Z0-9\.\-_]{8,30}$");
            Match match = regex.Match(password);

            return match.Success;
        }
   
   
   }
}