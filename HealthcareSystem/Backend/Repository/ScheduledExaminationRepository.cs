/***********************************************************************
 * Module:  ScheduledExaminationRepository.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Repository.ScheduledExaminationRepository
 ***********************************************************************/

using Model.Doctor;
using Model.Users;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Configuration;
using Model.Manager;

namespace Repository
{
    public class ScheduledExaminationRepository
    {
        private string path;
        private double DURATION_OF_EXAMINATION = Double.Parse(ConfigurationManager.AppSettings["duration_of_examination"]);

        public ScheduledExaminationRepository()
        {
            string fileName = "scheduled_examinations.json";
            path = Path.GetFullPath(fileName);
        }

        public List<Examination> getAllExaminations()
        {
            List<Examination> scheduledExaminations = ReadFromFile();
            return scheduledExaminations;
        }

        public int getLastId()
        {
            List<Examination> scheduledExaminations = ReadFromFile();
            if (scheduledExaminations.Count == 0)
            {
                return 0;
            }
            return scheduledExaminations[scheduledExaminations.Count - 1].IdExamination;
        }
        public Model.Doctor.Examination GetExaminationById(int id)
        {
            List<Examination> scheduledExaminations = ReadFromFile();
            foreach (Examination e in scheduledExaminations)
            {
                if (e.IdExamination == id)
                {
                    return e;
                }
            }
            return null;
        }

        public List<Examination> GetAllExaminations()
        {
            List<Examination> scheduledExaminations = ReadFromFile();
            return scheduledExaminations;
        }

        public Model.Doctor.Examination SetExamination(Model.Doctor.Examination examination)
        {
            List<Examination> scheduledExaminations = ReadFromFile();

            foreach (Examination e in scheduledExaminations)
            {
                if (e.IdExamination == examination.IdExamination)
                {
                    e.Type = examination.Type;
                    e.DateAndTime = examination.DateAndTime;
                    e.doctor = examination.doctor;
                    e.room = examination.room;
                    e.patientCard = examination.patientCard;
                    break;
                }
            }
            WriteInFile(scheduledExaminations);
            return examination;
        }

        public Examination DeleteExamination(int id)
        {
            List<Examination> scheduledExaminations = ReadFromFile();
            Examination examinationForDelete = null;
            foreach (Examination e in scheduledExaminations)
            {
                if (e.IdExamination == id)
                {
                    examinationForDelete = e;
                    break;
                }
            }
            if (examinationForDelete == null)
                return null;
            scheduledExaminations.Remove(examinationForDelete);
            WriteInFile(scheduledExaminations);
            return examinationForDelete;
        }

        public bool DeleteRoomScheduledExaminations(int numberOfRoom)
        {
            List<Examination> scheduledExaminations = ReadFromFile();
            List<Examination> examinationsForDelete = new List<Examination>();
            foreach (Examination e in scheduledExaminations)
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
                scheduledExaminations.Remove(e);
            }
            WriteInFile(scheduledExaminations);
            return true;
        }

        public bool DeleteDoctorScheduledExaminations(string doctorJmbg)
        {
            List<Examination> scheduledExaminations = ReadFromFile();
            List<Examination> examinationsForDelete = new List<Examination>();
            foreach (Examination e in scheduledExaminations)
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
                scheduledExaminations.Remove(e);
            }
            WriteInFile(scheduledExaminations);
            return true;
        }

        public bool DeletePatientScheduledExaminations(string patientJmbg)
        {
            List<Examination> scheduledExaminations = ReadFromFile();
            List<Examination> examinationsForDelete = new List<Examination>();
            foreach (Examination e in scheduledExaminations)
            {
                if (e.patientCard.Patient.Jmbg.Equals(patientJmbg))
                {
                    examinationsForDelete.Add(e);
                }
            }
            if (examinationsForDelete.Count == 0)
                return false;
            foreach(Examination e in examinationsForDelete)
            {
                scheduledExaminations.Remove(e);
            }
            WriteInFile(scheduledExaminations);
            return true;
        }

        public Examination NewExamination(Examination examination)
      {
            List<Examination> scheduledExaminations = ReadFromFile();
            Examination searchExamination = GetExaminationById(examination.IdExamination);
            if(searchExamination != null)
            {
                return null;
            }
            scheduledExaminations.Add(examination);
            WriteInFile(scheduledExaminations);
            return examination;
      }
      
      public List<Examination> GetExaminationsByDate(DateTime date)
      {
            List<Examination> result = new List<Examination>();
            List<Examination> scheduledExaminations = ReadFromFile();
            foreach(Examination e in scheduledExaminations)
            {
                if (e.DateAndTime.ToShortDateString().Equals(date.ToShortDateString()))
                {
                    result.Add(e);
                }
            }
            return result;
      }
      
      public Examination GetExaminationByDoctorDateAndTime(Model.Users.Doctor doctor, DateTime dateAndTime)
      {
            List<Examination> scheduledExaminations = ReadFromFile();
            foreach (Examination e in scheduledExaminations)
            {
                if (DateTime.Compare(dateAndTime, e.DateAndTime) == 0  && e.doctor.Jmbg.Equals(doctor.Jmbg))
                {
                    return e;
                }
            }
            return null;
        }
      
      public List<Examination> GetExaminationsByPatient(string patientJmbg)
      {
            List<Examination> result = new List<Examination>();
            List<Examination> scheduledExaminations = ReadFromFile();
            foreach (Examination e in scheduledExaminations)
            {
                if (e.patientCard.Patient.Jmbg.Equals(patientJmbg))
                {
                    result.Add(e);
                }
            }
            return result;
        }

    public List<Examination> GetExaminationsByDoctor(string doctorJmbg)
        {
            List<Examination> result = new List<Examination>();
            List<Examination> scheduledExaminations = ReadFromFile();
            foreach (Examination e in scheduledExaminations)
            {
                if (e.doctor.Jmbg.Equals(doctorJmbg))
                {
                    result.Add(e);
                }
            }
            return result;
        }

        public List<Examination> GetExaminationsByRoom(int numberOfRoom)
        {
            List<Examination> result = new List<Examination>();
            List<Examination> scheduledExaminations = ReadFromFile();
            foreach (Examination e in scheduledExaminations)
            {
                if (e.room.Number == numberOfRoom)
                {
                    result.Add(e);
                }
            }
            return result;
        }      
      public List<Examination> GetExaminationsByRoomAndDates(int numberOfRoom, DateTime beginDate, DateTime endDate)
      {
            List<Examination> result = new List<Examination>();
            List<Examination> scheduledExaminations = ReadFromFile();
            foreach (Examination e in scheduledExaminations)
            {
                int compareBeginDate = DateTime.Compare(beginDate, e.DateAndTime);
                int compareEndDate = DateTime.Compare(endDate, e.DateAndTime);
                if (e.room.Number == numberOfRoom && compareBeginDate <= 0 && compareEndDate >= 0)
                {
                    result.Add(e);
                }
            }
            return result;
        }
      public bool CheckDoctorAvailability(Model.Users.Doctor doctor, DateTime dateAndTime)
      {
            List<Examination> scheduledExaminations = ReadFromFile();
            foreach (Examination e in scheduledExaminations)
            {
                if (DateTime.Compare(e.DateAndTime, dateAndTime) == 0  && e.doctor.Jmbg.Equals(doctor.Jmbg))
                {
                    return false;
                }
            }
            return true;
        }
      
      public bool CheckRoomAvailability(Model.Manager.Room room, DateTime dateAndTime)
      {
            List<Examination> scheduledExaminations = ReadFromFile();
            foreach (Examination e in scheduledExaminations)
            {
                if (DateTime.Compare(e.DateAndTime, dateAndTime) == 0 && e.room.Number == room.Number)
                {
                    return false;
                }
            }
            return true;
        }

        private List<Examination> getFreeAppointments(Doctor doctor, DateTime beginDate, DateTime endDate)
        {
            List<Examination> result = new List<Examination>();
            int lastId = getLastId();
            double hours;

            for (DateTime date = beginDate; DateTime.Compare(date, endDate) <= 0; date = date.AddDays(1))
            {
                hours = 0;
                for (int i = 0; i < 16; i++)
                {
                    result.Add(new Examination(++lastId, new TypeOfExamination(), date.AddHours(hours), new Doctor(doctor), new Room(), new PatientCard()));
                    hours += DURATION_OF_EXAMINATION;
                }
            }
            return result;
        }

        public List<Examination> fillAppointments(Doctor doctor, DateTime beginDate, DateTime endDate)
        {
            List<Examination> appointmentsForFill = getFreeAppointments(doctor, beginDate, endDate);
            foreach (Examination e in appointmentsForFill)
            {
                Examination searchExamination = GetExaminationByDoctorDateAndTime(doctor, e.DateAndTime);
                if (searchExamination != null)
                {
                    e.IdExamination = searchExamination.IdExamination;
                    e.Type = searchExamination.Type;
                    e.patientCard = searchExamination.patientCard;
                    e.room = searchExamination.room;
                }
            }
            return appointmentsForFill;
        }

        public Examination AppointmentRecommendationByDoctor(Doctor doctor,DateTime beginDate,DateTime endDate)
        {
            List<Examination> appointmentsForRecommendation = fillAppointments(doctor, beginDate, endDate);
            foreach (Examination e in appointmentsForRecommendation)
            {
                if (e.room.Number == 0)
                {
                    return e;
                }
            }
            return null;
        }

        public Examination AppointmentRecommendationByDate(Doctor doctor, DateTime beginDate, DateTime endDate)
        {
            List<Examination> appointmentsForRecommendation = fillAppointments(doctor, beginDate, endDate);
            bool hasFreeAppointment = false;
            foreach (Examination e in appointmentsForRecommendation)
            {
                if (e.room.Number == 0)
                {
                    hasFreeAppointment = true;
                    return e;
                }
            }
            if (!hasFreeAppointment)
            {
                for (int i = 0; i < 5; i++)
                {
                    beginDate = beginDate.AddDays(-1);
                    endDate = endDate.AddDays(1);
                    appointmentsForRecommendation = fillAppointments(doctor, beginDate, endDate);
                    foreach (Examination e in appointmentsForRecommendation)
                    {
                        if (e.room.Number == 0)
                        {
                            return e;
                        }
                    }
                }
            }

            return null;
        }

        public List<Examination> GetExaminationsByDoctorAndDate(Model.Users.Doctor doctor, DateTime dateAndTime)
        {
            List<Examination> result = new List<Examination>();
            List<Examination> scheduledExaminations = ReadFromFile();
            foreach (Examination e in scheduledExaminations)
            {
                if (e.DateAndTime.ToString().Equals(dateAndTime.ToString()) && e.doctor.Jmbg.Equals(doctor.Jmbg))
                {
                    result.Add(e);
                }
            }
            return result;
        }

        private List<Examination> ReadFromFile()
        {
            List<Examination> scheduledExaminations;
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                scheduledExaminations = JsonConvert.DeserializeObject<List<Examination>>(json);
            }
            else
            {
                scheduledExaminations = new List<Examination>();
                WriteInFile(scheduledExaminations);
            }
            return scheduledExaminations;
        }

        private void WriteInFile(List<Examination> scheduledExaminations)
        {
            string json = JsonConvert.SerializeObject(scheduledExaminations);
            File.WriteAllText(path, json);
        }

    }
}