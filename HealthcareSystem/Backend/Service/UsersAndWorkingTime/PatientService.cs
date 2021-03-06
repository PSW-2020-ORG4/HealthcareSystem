/***********************************************************************
 * Module:  PatientService.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Service.Users&WorkingTimeService.PatientService
 ***********************************************************************/

using Backend.Model.Exceptions;
using Backend.Model.Users;
using Backend.Repository;
using System;
using System.Collections.Generic;

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
            patient.DateOfRegistration = DateTime.Now;
            _activePatientRepository.AddPatient(patient);
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
            patient.ImageName = imageName;
            _activePatientRepository.UpdatePatient(patient);
        }
        public List<Patient> ViewMaliciousPatients()
        {
            List<Patient> patients = _activePatientRepository.GetAllPatients();
            List<Patient> result = new List<Patient>();
            foreach (Patient patient in patients)
            {
                if (_activePatientRepository.GetNumberOfCanceledExaminations(patient.Jmbg) >= 3)
                {
                    result.Add(patient);
                }
            }
            return result;
        }

        public int GetNumberOfCanceledExaminations(string jmbg)
        {
            return _activePatientRepository.GetNumberOfCanceledExaminations(jmbg);
        }

        public void BlockPatient(string jmbg)
        {
            Patient patient = GetPatientByJmbg(jmbg);
            if (patient == null)
            {
                throw new NotFoundException("Patient with jmbg=" + jmbg + " doesn't exist in database.");
            }
            patient.IsBlocked = true;
            _activePatientRepository.UpdatePatient(patient);
        }

        public Patient GetPatientByJmbg(string jmbg)
        {
            return _activePatientRepository.GetPatientByJmbg(jmbg);
        }
        public Patient GetPatientByUsernameAndPassword(string username, string password)
        {
            return _activePatientRepository.GetPatientByUsernameAndPassword(username, password);
        }

        public Patient GetPatientByPatientCardId(int patientCardId)
        {
            return _activePatientRepository.GetPatientByPatientCardId(patientCardId);
        }
    }
}