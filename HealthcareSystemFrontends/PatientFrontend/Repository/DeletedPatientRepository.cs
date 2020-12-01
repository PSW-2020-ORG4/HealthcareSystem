/***********************************************************************
 * Module:  DeletedPatientRepository.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Repository.DeletedPatientRepository
 ***********************************************************************/

using Model.Users;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Repository
{
   public class DeletedPatientRepository
   {
        private string path;

        public DeletedPatientRepository()
        {
            string fileName = "deleted_patients.json";
            path = Path.GetFullPath(fileName);
        }

        public Patient GetPatientByJmbg(string jmbg)
        {
            List<Patient> patients = ReadFromFile();
            foreach(Patient p in patients)
            {
                if (p.Jmbg.Equals(jmbg))
                {
                    return p;
                }
            }
            return null;
        }
        public Model.Users.Patient NewPatient(Model.Users.Patient patient)
        {
            List<Patient> patients = ReadFromFile();
            Patient searchPatient = GetPatientByJmbg(patient.Jmbg);
            if(searchPatient != null)
            {
                return null;
            }
            patients.Add(patient);
            WriteInFile(patients);
            return patient;
        }
      
      public bool DeletePatient(string jmbg)
      {
            List<Patient> patients = ReadFromFile();
            Patient patientForDelete = null;
            foreach(Patient p in patients)
            {
                if (p.Jmbg.Equals(jmbg))
                {
                    patientForDelete = p;
                    break;
                }
            }
            if (patientForDelete == null)
                return false;
            patients.Remove(patientForDelete);
            WriteInFile(patients);
            return true;
      }

        private List<Patient> ReadFromFile()
        {
            List<Patient> patients;
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
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
            File.WriteAllText(path, json);
        }

    }
}