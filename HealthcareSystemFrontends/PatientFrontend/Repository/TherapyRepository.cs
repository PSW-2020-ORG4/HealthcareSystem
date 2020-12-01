/***********************************************************************
 * Module:  TherapyRepository.cs
 * Author:  Dragana Carapic
 * Purpose: Definition of the Class Repository.TherapyRepository
 ***********************************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using Model.Doctor;
using Newtonsoft.Json;

namespace Repository
{
    public class TherapyRepository
    {
        private string path;

        public TherapyRepository()
        {

            string fileName = "therapy.json";
            path = Path.GetFullPath(fileName);

        }
		
		 public int getLastId()
        {
            List<Therapy> therapies = ReadFromFile();
            if (therapies.Count == 0)
            {
                return 0;
            }
            return therapies[therapies.Count - 1].Id;
        }
		
        public Model.Doctor.Therapy NewTherapy(Model.Doctor.Therapy therapy)
        {
            List<Therapy> therapyList = ReadFromFile();
            Therapy searchTherapy = GetTherapy(therapy.Id);
            if (searchTherapy != null)
            {
                return null;
            }
            therapyList.Add(therapy);
            WriteInFile(therapyList);
            return therapy;

        }

        public Model.Doctor.Therapy SetTherapy(Model.Doctor.Therapy therapy)
        {
           
            List<Therapy> therapyList = ReadFromFile();

            foreach (Therapy t in therapyList)
            {
                if (t.Id.Equals(therapy.Id))
                {
                    t.drug = therapy.drug;
                    t.patientCard = therapy.patientCard;
                    t.Id = therapy.Id;
                    t.Anamnesis = therapy.Anamnesis;
                    t.StartDate = therapy.StartDate;
                    t.EndDate = therapy.EndDate;
                    t.DailyDose = therapy.DailyDose;
                    break;
                }
            }
            WriteInFile(therapyList);
            return null;

        }

        public Therapy GetTherapy(int id)
        {
            List<Therapy> therapyList = ReadFromFile();
            foreach (Therapy t in therapyList)
            {
                if (t.Id == id)
                {
                    return t;
                }
            }

            return null;
        }

        public bool DeleteTherapy(int idTherapy)
        {
            
            List<Therapy> therapyList = ReadFromFile();
            Therapy therapyForDelete = null;
            foreach (Therapy t in therapyList)
            {
                if (t.Id == idTherapy)
                {
                    therapyForDelete = t;
                    break;
                }
            }
            if (therapyForDelete == null)
            {
                return false;
            }

            therapyList.Remove(therapyForDelete);
            WriteInFile(therapyList);
            return true;

        }

        public List<Therapy> GetTherapyByPatient(string patientJmbg)
        {
            
            List<Therapy> therapyList = ReadFromFile();
            List<Therapy> therapyListPatinet = new List<Therapy>();
            foreach (Therapy t in therapyList)
            {
                if (t.patientCard.patient.Jmbg.Equals(patientJmbg))
                {
                    therapyListPatinet.Add(t);
                }
            }
            return therapyListPatinet;

        }

        public List<Therapy> GetActiveTherapyByPatient(string patientJmbg)
        {
            
            List<Therapy> therapyList = ReadFromFile();
            List<Therapy> therapyListPatinet = new List<Therapy>();
            foreach (Therapy t in therapyList)
            {
                if (t.patientCard.patient.Jmbg.Equals(patientJmbg))
                {
                    if (DateTime.Compare(t.EndDate,DateTime.Now) >= 0)
                    {
                        therapyListPatinet.Add(t);
                    }
                }
            }
            return therapyListPatinet;

        }

        public List<Therapy> GetTherapyForNextSevenDays(string patientJmbg)
        {
            List<Therapy> therapyList = ReadFromFile();
            List<Therapy> therapyListForNextSevenDays = new List<Therapy>();

            DateTime firstDay = DateTime.Now;
            DateTime seventhDay = firstDay.AddDays(6);

            foreach (Therapy t in therapyList)
            {
                if (t.patientCard.patient.Jmbg.Equals(patientJmbg))
                {
                    if (DateTime.Compare(t.StartDate, firstDay) >= 0 && DateTime.Compare(t.EndDate, seventhDay) <= 0)
                    {
                        therapyListForNextSevenDays.Add(t);
                    }
                }
            }

            return therapyListForNextSevenDays;
        }

        private List<Therapy> ReadFromFile()
        {
            List<Therapy> therapyList;
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                therapyList = JsonConvert.DeserializeObject<List<Therapy>>(json);
            }
            else
            {
                therapyList = new List<Therapy>();
                WriteInFile(therapyList);
            }
            return therapyList;
        }

        private void WriteInFile(List<Therapy> therapyList)
        {
            string json = JsonConvert.SerializeObject(therapyList);
            File.WriteAllText(path, json);
        }
    }
}
