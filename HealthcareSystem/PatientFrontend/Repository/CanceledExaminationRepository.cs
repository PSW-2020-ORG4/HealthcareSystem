/***********************************************************************
 * Module:  ScheduledExaminationRepository.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Repository.ScheduledExaminationRepository
 ***********************************************************************/

using Model.Doctor;
using Newtonsoft.Json;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.IO;

namespace Repository
{
    public class CanceledExaminationRepository
    {
        private string path;
        public CanceledExaminationRepository()
        {
            string fileName = "canceled_examinations.json";
            path = Path.GetFullPath(fileName);
        }
        public Model.Doctor.Examination GetExaminationById(int id)
        {
            List<Examination> canceledExaminations = ReadFromFile();
            foreach (Examination e in canceledExaminations)
            {
                if (e.IdExamination == id)
                {
                    return e;
                }
            }
            return null;
        }

        public bool DeleteRoomCanceledExaminations(int numberOfRoom)
        {
            List<Examination> canceledExaminations = ReadFromFile();
            List<Examination> examinationsForDelete = new List<Examination>();
            foreach (Examination e in canceledExaminations)
            {
                if (e.room.Number == numberOfRoom)
                {
                    examinationsForDelete.Add(e);
                }
            }
            if (examinationsForDelete.Count == 0)
                return false;
            foreach (Examination e in examinationsForDelete)
            {
                canceledExaminations.Remove(e);
            }
            WriteInFile(canceledExaminations);
            return true;
        }

        public bool DeleteDoctorCanceledExaminations(string doctorJmbg)
        {
            List<Examination> canceledExaminations = ReadFromFile();
            List<Examination> examinationsForDelete = new List<Examination>();
            foreach (Examination e in canceledExaminations)
            {
                if (e.doctor.Jmbg.Equals(doctorJmbg))
                {
                    examinationsForDelete.Add(e);
                }
            }
            if (examinationsForDelete.Count == 0)
                return false;
            foreach (Examination e in examinationsForDelete)
            {
                canceledExaminations.Remove(e);
            }
            WriteInFile(canceledExaminations);
            return true;
        }

        public bool DeletePatientCanceledExaminations(string jmbg)
        {
            List<Examination> canceledExaminations = ReadFromFile();
            List<Examination> examinationsForDelete = new List<Examination>();
            foreach (Examination e in canceledExaminations)
            {
                if (e.patientCard.patient.Jmbg.Equals(jmbg))
                {
                    examinationsForDelete.Add(e);
                }
            }
            if (examinationsForDelete.Count == 0)
                return false;
            foreach (Examination e in examinationsForDelete)
            {
                canceledExaminations.Remove(e);
            }
            WriteInFile(canceledExaminations);
            return true;
        }

        public List<Examination> GetAllExaminations()
      {
            List<Examination> canceledExaminations = ReadFromFile();
            return canceledExaminations;
        }
      
      public Examination DeleteExamination(int id)
      {
            List<Examination> canceledExaminations = ReadFromFile();
            Examination examinationForDelete = null;
            foreach (Examination e in canceledExaminations)
            {
                if (e.IdExamination == id)
                {
                    examinationForDelete = e;
                    break;
                }
            }
            if (examinationForDelete == null)
                return null;
            canceledExaminations.Remove(examinationForDelete);
            WriteInFile(canceledExaminations);
            return examinationForDelete;
        }
      
      public Model.Doctor.Examination NewExamination(Model.Doctor.Examination examination)
      {
            List<Examination> canceledExaminations = ReadFromFile();
            Examination searchExamination = GetExaminationById(examination.IdExamination);
            if (searchExamination != null)
            {
                return null;
            }
            canceledExaminations.Add(examination);
            WriteInFile(canceledExaminations);
            return examination;
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