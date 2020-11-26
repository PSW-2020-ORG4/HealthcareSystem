using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Enums;
using Model.Manager;
using Model.PerformingExamination;
using Model.Users;
using Newtonsoft.Json;

namespace Backend.Repository.ExaminationRepository.FileExaminationRepository
{
    class FileScheduledExaminationRepository : IScheduledExaminationRepository
    {
        private string path;
        private double DURATION_OF_EXAMINATION = Double.Parse(ConfigurationManager.AppSettings["duration_of_examination"]);

        public FileScheduledExaminationRepository()
        {
            string fileName = "scheduled_examinations.json";
            path = Path.GetFullPath(fileName);
        }

        public void AddExamination(Examination examination)
        {
            List<Examination> scheduledExaminations = ReadFromFile();
            Examination searchExamination = GetExaminationById(examination.Id);
            if (searchExamination != null)
            {
                throw new ValidationException();
            }
            scheduledExaminations.Add(examination);
            WriteInFile(scheduledExaminations);
        }

        public Examination AppointmentRecommendationByDate(Doctor doctor, DateTime beginDate, DateTime endDate)
        {
            List<Examination> appointmentsForRecommendation = fillAppointments(doctor, beginDate, endDate);
            bool hasFreeAppointment = false;
            foreach (Examination e in appointmentsForRecommendation)
            {
                if (e.Room.Id == 0)
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
                        if (e.Room.Id == 0)
                        {
                            return e;
                        }
                    }
                }
            }

            return null;
        }

        public Examination AppointmentRecommendationByDoctor(Doctor doctor, DateTime beginDate, DateTime endDate)
        {
            List<Examination> appointmentsForRecommendation = fillAppointments(doctor, beginDate, endDate);
            foreach (Examination e in appointmentsForRecommendation)
            {
                if (e.Room.Id == 0)
                {
                    return e;
                }
            }
            return null;
        }

        public bool CheckDoctorAvailability(Doctor doctor, DateTime dateAndTime)
        {
            List<Examination> scheduledExaminations = ReadFromFile();
            foreach (Examination e in scheduledExaminations)
            {
                if (DateTime.Compare(e.DateAndTime, dateAndTime) == 0 && e.Doctor.Jmbg.Equals(doctor.Jmbg))
                {
                    return false;
                }
            }
            return true;
        }

        public bool CheckRoomAvailability(Room room, DateTime dateAndTime)
        {
            List<Examination> scheduledExaminations = ReadFromFile();
            foreach (Examination e in scheduledExaminations)
            {
                if (DateTime.Compare(e.DateAndTime, dateAndTime) == 0 && e.Room.Id == room.Id)
                {
                    return false;
                }
            }
            return true;
        }

        public void DeleteDoctorScheduledExaminations(string doctorJmbg)
        {
            List<Examination> scheduledExaminations = ReadFromFile();
            List<Examination> examinationsForDelete = new List<Examination>();
            foreach (Examination e in scheduledExaminations)
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
                scheduledExaminations.Remove(e);
            }
            WriteInFile(scheduledExaminations);
        }

        public void DeleteExamination(int id)
        {
            List<Examination> scheduledExaminations = ReadFromFile();
            Examination examinationForDelete = null;
            foreach (Examination e in scheduledExaminations)
            {
                if (e.Id == id)
                {
                    examinationForDelete = e;
                    break;
                }
            }
            if (examinationForDelete == null)
                throw new ValidationException();

            scheduledExaminations.Remove(examinationForDelete);
            WriteInFile(scheduledExaminations);
        }

        public void DeletePatientScheduledExaminations(string patientJmbg)
        {
            List<Examination> scheduledExaminations = ReadFromFile();
            List<Examination> examinationsForDelete = new List<Examination>();
            foreach (Examination e in scheduledExaminations)
            {
                if (e.PatientCard.Patient.Jmbg.Equals(patientJmbg))
                {
                    examinationsForDelete.Add(e);
                }
            }
            if (examinationsForDelete.Count == 0)
                throw new ValidationException();
            foreach (Examination e in examinationsForDelete)
            {
                scheduledExaminations.Remove(e);
            }
            WriteInFile(scheduledExaminations);
        }

        public void DeleteRoomScheduledExaminations(int numberOfRoom)
        {
            List<Examination> scheduledExaminations = ReadFromFile();
            List<Examination> examinationsForDelete = new List<Examination>();
            foreach (Examination e in scheduledExaminations)
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
                scheduledExaminations.Remove(e);
            }
            WriteInFile(scheduledExaminations);
        }

        public List<Examination> fillAppointments(Doctor doctor, DateTime beginDate, DateTime endDate)
        {
            List<Examination> appointmentsForFill = getFreeAppointments(doctor, beginDate, endDate);
            foreach (Examination e in appointmentsForFill)
            {
                Examination searchExamination = GetExaminationByDoctorDateAndTime(doctor, e.DateAndTime);
                if (searchExamination != null)
                {
                    e.Id = searchExamination.Id;
                    e.Type = searchExamination.Type;
                    e.PatientCard = searchExamination.PatientCard;
                    e.Room = searchExamination.Room;
                }
            }
            return appointmentsForFill;
        }

        public List<Examination> GetAllExaminations()
        {
            List<Examination> scheduledExaminations = ReadFromFile();
            return scheduledExaminations;
        }

        public Examination GetExaminationByDoctorDateAndTime(Doctor doctor, DateTime dateAndTime)
        {
            List<Examination> scheduledExaminations = ReadFromFile();
            foreach (Examination e in scheduledExaminations)
            {
                if (DateTime.Compare(dateAndTime, e.DateAndTime) == 0 && e.Doctor.Jmbg.Equals(doctor.Jmbg))
                {
                    return e;
                }
            }
            return null;
        }

        public Examination GetExaminationById(int id)
        {
            List<Examination> scheduledExaminations = ReadFromFile();
            foreach (Examination e in scheduledExaminations)
            {
                if (e.Id == id)
                {
                    return e;
                }
            }
            return null;
        }

        public List<Examination> GetExaminationsByDate(DateTime date)
        {
            List<Examination> result = new List<Examination>();
            List<Examination> scheduledExaminations = ReadFromFile();
            foreach (Examination e in scheduledExaminations)
            {
                if (e.DateAndTime.ToShortDateString().Equals(date.ToShortDateString()))
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
                if (e.Doctor.Jmbg.Equals(doctorJmbg))
                {
                    result.Add(e);
                }
            }
            return result;
        }

        public List<Examination> GetExaminationsByDoctorAndDate(Doctor doctor, DateTime dateAndTime)
        {
            List<Examination> result = new List<Examination>();
            List<Examination> scheduledExaminations = ReadFromFile();
            foreach (Examination e in scheduledExaminations)
            {
                if (e.DateAndTime.ToString().Equals(dateAndTime.ToString()) && e.Doctor.Jmbg.Equals(doctor.Jmbg))
                {
                    result.Add(e);
                }
            }
            return result;
        }

        public List<Examination> GetExaminationsByPatient(string patientJmbg)
        {
            List<Examination> result = new List<Examination>();
            List<Examination> scheduledExaminations = ReadFromFile();
            foreach (Examination e in scheduledExaminations)
            {
                if (e.PatientCard.Patient.Jmbg.Equals(patientJmbg))
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
                if (e.Room.Id == numberOfRoom)
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
                if (e.Room.Id == numberOfRoom && compareBeginDate <= 0 && compareEndDate >= 0)
                {
                    result.Add(e);
                }
            }
            return result;
        }

        public List<Examination> getFreeAppointments(Doctor doctor, DateTime beginDate, DateTime endDate)
        {
            List<Examination> result = new List<Examination>();
            int lastId = 0;
            double hours;
            string anamnesis = "";

            for (DateTime date = beginDate; DateTime.Compare(date, endDate) <= 0; date = date.AddDays(1))
            {
                hours = 0;
                for (int i = 0; i < 16; i++)
                {
                    result.Add(new Examination(++lastId, new TypeOfExamination(), date.AddHours(hours), anamnesis, new Doctor(doctor), new Room(), new PatientCard()));
                    hours += DURATION_OF_EXAMINATION;
                }
            }
            return result;
        }

        public void UpdateExamination(Examination examination)
        {
            List<Examination> scheduledExaminations = ReadFromFile();

            foreach (Examination e in scheduledExaminations)
            {
                if (e.Id == examination.Id)
                {
                    e.Type = examination.Type;
                    e.DateAndTime = examination.DateAndTime;
                    e.Doctor = examination.Doctor;
                    e.Room = examination.Room;
                    e.PatientCard = examination.PatientCard;
                    break;
                }
            }
            WriteInFile(scheduledExaminations);
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
