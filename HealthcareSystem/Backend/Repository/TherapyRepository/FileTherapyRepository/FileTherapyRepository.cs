using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.PerformingExamination;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Backend.Repository.TherapyRepository.FileTherapyRepository
{
    class FileTherapyRepository : ITherapyRepository
    {
        private string path;

        public FileTherapyRepository()
        {

            string fileName = "therapy.json";
            path = Path.GetFullPath(fileName);

        }
        public void AddTherapy(Therapy therapy)
        {
            List<Therapy> therapyList = ReadFromFile();
            therapyList.Add(therapy);
            WriteInFile(therapyList);
        }

        public void DeleteDrugTherapies(int idDrug)
        {
            List<Therapy> therapyList = ReadFromFile();
            List<Therapy> therapyListForDelete = new List<Therapy>();
            foreach (Therapy therapy in therapyList)
            {
                if (therapy.Drug.Id == idDrug)
                {
                    therapyListForDelete.Add(therapy);
                }
            }

            foreach (Therapy therapy in therapyListForDelete)
            {
                if (therapy == null)
                    throw new ValidationException();
                therapyList.Remove(therapy);
            }
            WriteInFile(therapyList);
        }

        public void DeletePatientTherapies(string patientJmbg)
        {
            List<Therapy> therapyList = ReadFromFile();
            List<Therapy> therapyListForDelete = new List<Therapy>();
            foreach (Therapy therapy in therapyList)
            {
                if (therapy.Examination.PatientCard.Patient.Jmbg.Equals(patientJmbg))
                {
                    therapyListForDelete.Add(therapy);
                }
            }

            foreach (Therapy therapy in therapyListForDelete)
            {
                if (therapy == null)
                    throw new ValidationException();
                therapyList.Remove(therapy);
            }
            WriteInFile(therapyList);
        }

        public void DeleteTherapy(int idTherapy)
        {
            List<Therapy> therapyList = ReadFromFile();

            foreach (Therapy therapy in therapyList)
            {
                if (therapy.Id == idTherapy)
                {
                    if (therapy == null)
                        throw new ValidationException();
                    therapyList.Remove(therapy);
                    WriteInFile(therapyList);
                    return;
                }
            }
        }

        public List<Therapy> GetActiveTherapyByPatient(string patientJmbg)
        {
            List<Therapy> therapyList = ReadFromFile();
            List<Therapy> therapyListPatinet = new List<Therapy>();
            foreach (Therapy therapy in therapyList)
            {
                if (therapy.Examination.PatientCard.Patient.Jmbg.Equals(patientJmbg))
                {
                    if (therapy.EndDate > DateTime.Now)
                    {
                        therapyListPatinet.Add(therapy);
                    }
                }
            }
            return therapyListPatinet;
        }

        public List<Therapy> GetTherapyByDrug(int idDrug)
        {
            List<Therapy> therapyList = ReadFromFile();
            List<Therapy> therapyListDrug = new List<Therapy>();
            foreach (Therapy t in therapyList)
            {
                if (t.Drug.Id == idDrug)
                {
                    therapyListDrug.Add(t);
                }
            }
            return therapyListDrug;
        }

        public Therapy GetTherapyById(int id)
        {
            List<Therapy> therapyList = ReadFromFile();
            foreach (Therapy therapy in therapyList)
            {
                if (therapy.Id == id)
                {
                    return therapy;
                }
            }

            return null;
        }

        public List<Therapy> GetTherapyByPatient(string patientJmbg)
        {
            List<Therapy> therapyList = ReadFromFile();
            List<Therapy> therapyListPatinet = new List<Therapy>();
            foreach (Therapy t in therapyList)
            {
                if (t.Examination.PatientCard.Patient.Jmbg.Equals(patientJmbg))
                {
                    therapyListPatinet.Add(t);
                }
            }
            return therapyListPatinet;
        }

        public List<Therapy> GetTherapyForNextSevenDaysByPatient(string patientJmbg)
        {
            List<Therapy> therapyList = ReadFromFile();
            List<Therapy> therapyListForNextSevenDays = new List<Therapy>();

            DateTime firstDay = DateTime.Now;
            DateTime seventhDay = firstDay.AddDays(6);

            foreach (Therapy t in therapyList)
            {
                if (t.Examination.PatientCard.Patient.Jmbg.Equals(patientJmbg))
                {
                    if (DateTime.Compare(t.StartDate, firstDay) >= 0 && DateTime.Compare(t.EndDate, seventhDay) <= 0)
                    {
                        therapyListForNextSevenDays.Add(t);
                    }
                }
            }

            return therapyListForNextSevenDays;
        }

        public void UpdateTherapy(Therapy therapy)
        {
            List<Therapy> therapyList = ReadFromFile();

            foreach (Therapy t in therapyList)
            {
                if (t.Id.Equals(therapy.Id))
                {
                    WriteInFile(therapyList);
                    return;
                }
            }
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
