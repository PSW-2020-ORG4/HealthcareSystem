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
   public class FileDeletedPatientRepository : IDeletedPatientRepository
   {
        private string _path;
        public FileDeletedPatientRepository()
        {
            string fileName = "deleted_patients.json";
            _path = Path.GetFullPath(fileName);
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
        public Patient AddPatient(Patient patient)
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