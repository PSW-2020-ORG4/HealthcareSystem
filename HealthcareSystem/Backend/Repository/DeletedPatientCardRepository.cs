/***********************************************************************
 * Module:  DeletedPatientCardRepository.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Repository.DeletedPatientCardRepository
 ***********************************************************************/

using Model.Secretary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Repository
{
   public class DeletedPatientCardRepository
   {
        private string path;

        public DeletedPatientCardRepository()
        {
            string fileName = "deleted_patient_card.json";
            path = Path.GetFullPath(fileName);
        }

        public Model.Secretary.PatientCard GetPatientCard(string jmbg)
        {
            List<PatientCard> patientCards = ReadFromFile();
            foreach (PatientCard pc in patientCards)
            {
                if (pc.patient.Jmbg.Equals(jmbg))
                {
                    return pc;
                }
            }
            return null;
        }
        public Model.Secretary.PatientCard NewPatientCard(Model.Secretary.PatientCard patientCard)
        {
            List<PatientCard> patientCards = ReadFromFile();
            PatientCard searchPatientCard = GetPatientCard(patientCard.patient.Jmbg);
            if (searchPatientCard != null)
            {
                return null;
            }
            patientCards.Add(patientCard);
            WriteInFile(patientCards);
            return patientCard;
        }
      
      public bool DeletePatientCard(string patientJmbg)
      {
            List<PatientCard> patientCards = ReadFromFile();
            PatientCard patientCardForDelete = null;
            foreach (PatientCard pc in patientCards)
            {
                if (pc.patient.Jmbg.Equals(patientJmbg))
                {
                    patientCardForDelete = pc;
                    break;
                }
            }
            if (patientCardForDelete == null)
                return false;

            patientCards.Remove(patientCardForDelete);
            WriteInFile(patientCards);
            return true;
        }
        private List<PatientCard> ReadFromFile()
        {
            List<PatientCard> patientCards;
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
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
            File.WriteAllText(path, json);
        }

    }
}