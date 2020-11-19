/***********************************************************************
 * Module:  PatientCard.cs
 * Author:  LukaRA252017
 * Purpose: Definition of the Class Repository.Patients.PatientCard
 ***********************************************************************/

using Model.Secretary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Repository
{
    public class ActivePatientCardRepository
    {
        private string path;

        public ActivePatientCardRepository()
        {
            string fileName = "activepatientcards.json";
            path = Path.GetFullPath(fileName);
        }

        public Model.Secretary.PatientCard GetPatientCard(string jmbg)
        {
            List<PatientCard> patientCards = ReadFromFile();

            foreach (PatientCard patientCard in patientCards)
            {
                if (patientCard.patient.Jmbg.Equals(jmbg))
                {
                    return patientCard;
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

        public Model.Secretary.PatientCard DeletePatientCard(string patientJmbg)
        {
            List<PatientCard> patientCards = ReadFromFile();
            PatientCard patientCardForDelete = null;

            foreach (PatientCard patientCard in patientCards)
            {
                if (patientCard.patient.Jmbg.Equals(patientJmbg))
                {
                    patientCardForDelete = patientCard;
                    break;
                }
            }

            if (patientCardForDelete == null)
                return null;

            patientCards.Remove(patientCardForDelete);
            WriteInFile(patientCards);
            return patientCardForDelete;
        }

        public void SaveExaminationInPatientCard(Model.Doctor.Examination examination)
        {

            List<PatientCard> patientCards = ReadFromFile();
            
            foreach (PatientCard patientCard in patientCards)
            {
                if (examination.patientCard.patient.Jmbg.Equals(patientCard.patient.Jmbg))
                {
                    patientCard.examinationList.Add(examination);
                    break;
                }
            }
            WriteInFile(patientCards);
        }

        public Model.Secretary.PatientCard SetPatientCard(Model.Secretary.PatientCard card)
        {
            List<PatientCard> patientCards = ReadFromFile();

            foreach (PatientCard patientCard in patientCards)
            {
                if (patientCard.patient.Jmbg.Equals(card.patient.Jmbg))
                {
					patientCard.patient = card.patient;
                    patientCard.Alergies = card.Alergies;
                    patientCard.BloodType = card.BloodType;
                    patientCard.HasInsurance = card.HasInsurance;
					patientCard.Lbo = card.Lbo;
                    patientCard.examinationList = card.examinationList;
                    patientCard.MedicalHistory = card.MedicalHistory;
                    patientCard.RhFactor = card.RhFactor;
                    break;
                }
            }
            WriteInFile(patientCards);
            return card;
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
