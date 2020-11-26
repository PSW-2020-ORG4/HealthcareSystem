/***********************************************************************
 * Module:  PatientService.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Service.Users&WorkingTimeService.PatientService
 ***********************************************************************/

using Backend.Model.Exceptions;
using Backend.Service;
using Model.Users;
using Repository;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Backend.Service
{
    public class PatientService : IPatientService
    {
        private IActivePatientRepository _activePatientRepository;
        public PatientService(IActivePatientRepository activePatientRepository)
        {
            _activePatientRepository = activePatientRepository;
        }
        public void RegisterPatient(Patient patient)
        {
            try
            {
                _activePatientRepository.AddPatient(patient);
            }
            catch (Exception)
            {
                throw new DatabaseException("Patient with jmbg=" + patient.Jmbg + " already exists in database.");
            }
        }
        public List<Patient> ViewPatients()
        {
            return _activePatientRepository.GetAllPatients();
        }

        public Patient ViewProfile(string jmbg)
        {
            Patient patient = _activePatientRepository.GetPatientByJmbg(jmbg);
            if (patient == null)
                throw new NotFoundException("Patient with jmbg=" + jmbg + " doesn't exist in database.");
            return patient;
        }

        public void ActivatePatientStatus(string jmbg)
        {
            Patient patient = ViewProfile(jmbg);
            if (patient == null)
                throw new NotFoundException("Patient with jmbg=" + jmbg + "doesn't exist in database.");

            patient.IsActive = true;
            _activePatientRepository.UpdatePatient(patient);
        }

        public void EditPatient(Patient patient)
        {
            _activePatientRepository.UpdatePatient(patient);
        }
        
        public Patient SignIn(string username, string password)
        {
            return _activePatientRepository.CheckUsernameAndPassword(username, password);
        }

        public void SavePatientImageName(string jmbg, string imageName)
        {
            Patient patient = ViewProfile(jmbg);
            if (patient == null)
                throw new NotFoundException("Patient with jmbg=" + jmbg + " doesn't exist in database.");
          //  patient.ImageName = imageName;
            _activePatientRepository.UpdatePatient(patient);
        }
    }
}