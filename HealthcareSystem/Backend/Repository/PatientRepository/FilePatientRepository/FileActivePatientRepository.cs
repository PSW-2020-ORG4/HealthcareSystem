/***********************************************************************
 * Module:  PatientRepository.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Repository.PatientRepository
 ***********************************************************************/

using Backend.Model.Exceptions;
using Model.Users;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Repository
{
   public class FileActivePatientRepository : IActivePatientRepository
   {
        private string _path;
        public FileActivePatientRepository()
        {
            string fileName = "active_patients.json";
            _path = Path.GetFullPath(fileName);
        }
        public Patient GetPatientByJmbg(string jmbg)
        {
            List<Patient> patients = ReadFromFile();
            foreach (Patient p in patients)
            {
                if (p.Jmbg.Equals(jmbg))
                {
                    return p;
                }
            }
            return null;
        }

        public void UpdatePatient(Patient patient)
        {
            List<Patient> patients = ReadFromFile();
            foreach (Patient p in patients)
            {
                if (p.Jmbg.Equals(patient.Jmbg))
                {
                    p.Name = patient.Name;
                    p.Surname = patient.Surname;
                    p.Gender = patient.Gender;
                    p.DateOfBirth = patient.DateOfBirth;
                    p.City = patient.City;
                    p.Phone = patient.Phone;
                    p.Email = patient.Email;
                    p.HomeAddress = patient.HomeAddress;
                    p.Username = patient.Username;
                    p.Password = patient.Password;
                    p.DateOfRegistration = patient.DateOfRegistration;
                    p.IsGuest = patient.IsGuest;
                    break;
                }
            }
            WriteInFile(patients);
        }

        public List<Patient> GetAllPatients()
        {
            List<Patient> patients = ReadFromFile();
            return patients;
        }

        public void AddPatient(Patient patient)
        {
            List<Patient> patients = ReadFromFile();
            Patient searchPatient = GetPatientByJmbg(patient.Jmbg);
            if (searchPatient != null)
            {
                throw new ValidationException();
            }
            patients.Add(patient);
            WriteInFile(patients);
        }

        public void DeletePatient(string jmbg)
        {
            List<Patient> patients = ReadFromFile();
            Patient patientForDelete = null;
            foreach (Patient p in patients)
            {
                if (p.Jmbg.Equals(jmbg))
                {
                    patientForDelete = p;
                    break;
                }
            }
            if (patientForDelete == null)
                throw new ValidationException();
            patients.Remove(patientForDelete);
            WriteInFile(patients);
        }

        public Patient CheckUsernameAndPassword(string username, string password)
        {
            List<Patient> patients = ReadFromFile();
            foreach (Patient p in patients)
            {
                if (p.Username.Equals(username) && p.Password.Equals(password))
                {
                    return p;
                }
            }
            return null;
        }
        private List<Patient> ReadFromFile()
        {
            List<Patient> patients;
            if (File.Exists(_path))
            {
                string json = File.ReadAllText(_path);
                patients = JsonConvert.DeserializeObject<List<Patient>>(json);
            }
            else
            {
                patients = new List<Patient>();
                WriteInFile(patients);
            }
            return patients;
        }

        private void WriteInFile(List<Patient> patients)
        {
            string json = JsonConvert.SerializeObject(patients);
            File.WriteAllText(_path, json);
        }
    }
}