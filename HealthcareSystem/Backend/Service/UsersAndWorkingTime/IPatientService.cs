﻿using Backend.Model.Users;
using System.Collections.Generic;

namespace Backend.Service
{
    public interface IPatientService
    {
        void RegisterPatient(Patient patient);
        List<Patient> ViewPatients();
        Patient ViewProfile(string jmbg);
        void ActivatePatientStatus(string jmbg);
        void EditPatient(Patient patient);
        void SavePatientImageName(string jmbg, string imageName);
        Patient SignIn(string username, string password);
        List<Patient> ViewMaliciousPatients();
        int GetNumberOfCanceledExaminations(string jmbg);
        public Patient GetPatientByJmbg(string jmbg);
        void BlockPatient(string jmbg);
        public Patient GetPatientByUsernameAndPassword(string username, string password);
        public Patient GetPatientByPatientCardId(int patientCardId);
    }
}
