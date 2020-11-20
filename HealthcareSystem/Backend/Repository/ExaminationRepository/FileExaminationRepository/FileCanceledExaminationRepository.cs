using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.PerformingExamination;
using Newtonsoft.Json;

namespace Backend.Repository.ExaminationRepository.FileExaminationRepository
{
    class FileCanceledExaminationRepository : ICanceledExaminationRepository
    {
        private string path;
        public FileCanceledExaminationRepository()
        {
            string fileName = "canceled_examinations.json";
            path = Path.GetFullPath(fileName);
        }
        public void AddExamination(Examination examination)
        {
            List<Examination> canceledExaminations = ReadFromFile();
            Examination searchExamination = GetExaminationById(examination.Id);
            if (searchExamination != null)
            {
                throw new ValidationException();
            }
            canceledExaminations.Add(examination);
            WriteInFile(canceledExaminations);
        }

        public void DeleteDoctorCanceledExaminations(string doctorJmbg)
        {
            List<Examination> canceledExaminations = ReadFromFile();
            List<Examination> examinationsForDelete = new List<Examination>();
            foreach (Examination e in canceledExaminations)
            {
                if (e.Doctor.Jmbg.Equals(doctorJmbg))
                {
                    examinationsForDelete.Add(e);
                }
            }
            if (examinationsForDelete.Count == 0)
                throw new ValidationException();
            foreach (Examination e in examinationsForDelete)
            {
                canceledExaminations.Remove(e);
            }
            WriteInFile(canceledExaminations);
        }

        public void DeleteExamination(int id)
        {
            List<Examination> canceledExaminations = ReadFromFile();
            Examination examinationForDelete = null;
            foreach (Examination e in canceledExaminations)
            {
                if (e.Id == id)
                {
                    examinationForDelete = e;
                    break;
                }
            }

            if (examinationForDelete == null)
                throw new ValidationException();

            canceledExaminations.Remove(examinationForDelete);
            WriteInFile(canceledExaminations);
        }

        public void DeletePatientCanceledExaminations(string jmbg)
        {
            List<Examination> canceledExaminations = ReadFromFile();
            List<Examination> examinationsForDelete = new List<Examination>();
            foreach (Examination e in canceledExaminations)
            {
                if (e.PatientCard.Patient.Jmbg.Equals(jmbg))
                {
                    examinationsForDelete.Add(e);
                }
            }
            if (examinationsForDelete.Count == 0)
                throw new ValidationException();
            foreach (Examination e in examinationsForDelete)
            {
                canceledExaminations.Remove(e);
            }
            WriteInFile(canceledExaminations);
        }

        public void DeleteRoomCanceledExaminations(int numberOfRoom)
        {
            List<Examination> canceledExaminations = ReadFromFile();
            List<Examination> examinationsForDelete = new List<Examination>();
            foreach (Examination e in canceledExaminations)
            {
                if (e.Room.Id == numberOfRoom)
                {
                    examinationsForDelete.Add(e);
                }
            }
            if (examinationsForDelete.Count == 0)
                throw new ValidationException();
            foreach (Examination e in examinationsForDelete)
            {
                canceledExaminations.Remove(e);
            }
            WriteInFile(canceledExaminations);
        }

        public List<Examination> GetAllExaminations()
        {
            List<Examination> canceledExaminations = ReadFromFile();
            return canceledExaminations;
        }

        public Examination GetExaminationById(int id)
        {
            List<Examination> canceledExaminations = ReadFromFile();
            foreach (Examination e in canceledExaminations)
            {
                if (e.Id == id)
                {
                    return e;
                }
            }
            return null;
        }

        private List<Examination> ReadFromFile()
        {
            List<Examination> canceledExaminations;
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                canceledExaminations = JsonConvert.DeserializeObject<List<Examination>>(json);
            }
            else
            {
                canceledExaminations = new List<Examination>();
                WriteInFile(canceledExaminations);
            }
            return canceledExaminations;
        }

        private void WriteInFile(List<Examination> canceledExaminations)
        {
            string json = JsonConvert.SerializeObject(canceledExaminations);
            File.WriteAllText(path, json);
        }
    }
}
