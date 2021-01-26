﻿using Backend.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Repository
{
    public interface IActivePatientRepository
    {
        Patient GetPatientByJmbg(string jmbg);
        void UpdatePatient(Patient patient);
        List<Patient> GetAllPatients();
        void AddPatient(Patient patient);
        void DeletePatient(string jmbg);
        Patient CheckUsernameAndPassword(string username, string password);
        int GetNumberOfCanceledExaminations(string jmbg);
        public Patient GetPatientByUsernameAndPassword(string username, string password);
        public Patient GetPatientByPatientCardId(int patientCardId);
    }
}
