/***********************************************************************
 * Module:  PatientCard.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Repository.Patients.PatientCard
 ***********************************************************************/

using Backend.Model.Exceptions;
using Model.Users;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Repository
{
    public class FileActivePatientCardRepository : IActivePatientCardRepository
    {
        private string _path;

        public FileActivePatientCardRepository()
        {
            string fileName = "activepatientcards.json";
            _path = Path.GetFullPath(fileName);
        }

        public PatientCard GetPatientCard(string jmbg)
        {
            List<PatientCard> patientCards = ReadFromFile();

            foreach (PatientCard patientCard in patientCards)
            {
                if (patientCard.Patient.Jmbg.Equals(jmbg))
                {
                    return patientCard;
                }
            }
            return null;
        }

        public void AddPatientCard(PatientCard patientCard)
        {
            List<PatientCard> patientCards = ReadFromFile();
            PatientCard searchPatientCard = GetPatientCard(patientCard.Patient.Jmbg);

            if (searchPatientCard != null)
            {
                throw new ValidationException();
            }

            patientCards.Add(patientCard);
            WriteInFile(patientCards);
        }

        public void DeletePatientCard(string patientJmbg)
        {
            List<PatientCard> patientCards = ReadFromFile();
            PatientCard patientCardForDelete = null;

            foreach (PatientCard patientCard in patientCards)
            {
                if (patientCard.Patient.Jmbg.Equals(patientJmbg))
                {
                    patientCardForDelete = patientCard;
                    break;
                }
            }

            if (patientCardForDelete == null)
                throw new ValidationException();

            patientCards.Remove(patientCardForDelete);
            WriteInFile(patientCards);
        }


        public void SetPatientCard(PatientCard card)
        {
            List<PatientCard> patientCards = ReadFromFile();

            foreach (PatientCard patientCard in patientCards)
            {
                if (patientCard.Patient.Jmbg.Equals(card.Patient.Jmbg))
                {
                    patientCard.Patient = card.Patient;
                    patientCard.Alergies = card.Alergies;
                    patientCard.BloodType = card.BloodType;
                    patientCard.HasInsurance = card.HasInsurance;
                    patientCard.Lbo = card.Lbo;
                    patientCard.Examinations = card.Examinations;
                    patientCard.MedicalHistory = card.MedicalHistory;
                    patientCard.RhFactor = card.RhFactor;
                    break;
                }
            }
            WriteInFile(patientCards);
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
