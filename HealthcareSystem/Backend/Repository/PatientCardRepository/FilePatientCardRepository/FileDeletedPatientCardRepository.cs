/***********************************************************************
 * Module:  DeletedPatientCardRepository.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Repository.DeletedPatientCardRepository
 ***********************************************************************/

using Model.Users;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Repository
{
   public class FileDeletedPatientCardRepository : IDeletedPatientCardRepository
   {
        private string _path;

        public FileDeletedPatientCardRepository()
        {
            string fileName = "deleted_patient_card.json";
            _path = Path.GetFullPath(fileName);
        }

        public PatientCard GetPatientCard(string jmbg)
        {
            List<PatientCard> patientCards = ReadFromFile();
            foreach (PatientCard pc in patientCards)
            {
                if (pc.Patient.Jmbg.Equals(jmbg))
                {
                    return pc;
                }
            }
            return null;
        }
        public PatientCard AddPatientCard(PatientCard patientCard)
        {
            List<PatientCard> patientCards = ReadFromFile();
            PatientCard searchPatientCard = GetPatientCard(patientCard.Patient.Jmbg);
            if (searchPatientCard != null)
            {
                return null;
            }
            patientCards.Add(patientCard);
            WriteInFile(patientCards);
            return patientCard;
        }
      
        private List<PatientCard> ReadFromFile()
        {
            List<PatientCard> patientCards;
            if (File.Exists(_path))
            {
                string json = File.ReadAllText(_path);
                patientCards = JsonConvert.DeserializeObject<List<PatientCard>>(json);
            }
            else
            {
                patientCards = new List<PatientCard>();
                WriteInFile(patientCards);
            }
            return patientCards;
        }

        private void WriteInFile(List<PatientCard> patientCards)
        {
            string json = JsonConvert.SerializeObject(patientCards);
            File.WriteAllText(_path, json);
        }

    }
}