using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Model;
using Model.Manager;
using Model.PerformingExamination;
using Model.Users;

namespace Backend.Repository.ExaminationRepository.MySqlExaminationRepository
{
    class MySqlScheduledExaminationRepository : IScheduledExaminationRepository
    {
        private readonly MyDbContext _context;

        public MySqlScheduledExaminationRepository(MyDbContext context)
        {
            _context = context;
        }
        public void AddExamination(Examination examination)
        {
            _context.Examinations.Add(examination);
            _context.SaveChanges();
        }

        public Examination AppointmentRecommendationByDate(Doctor doctor, DateTime beginDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public Examination AppointmentRecommendationByDoctor(Doctor doctor, DateTime beginDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public bool CheckDoctorAvailability(Doctor doctor, DateTime dateAndTime)
        {
            throw new NotImplementedException();
        }

        public bool CheckRoomAvailability(Room room, DateTime dateAndTime)
        {
            throw new NotImplementedException();
        }

        public void DeleteDoctorScheduledExaminations(string doctorJmbg)
        {
            throw new NotImplementedException();
        }

        public void DeleteExamination(int id)
        {      
            Examination examination = GetExaminationById(id);
            _context.Remove(examination);
            _context.SaveChanges();
        }

        public void DeletePatientScheduledExaminations(string patientJmbg)
        {
            throw new NotImplementedException();
        }

        public void DeleteRoomScheduledExaminations(int numberOfRoom)
        {
            throw new NotImplementedException();
        }

        public List<Examination> fillAppointments(Doctor doctor, DateTime beginDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public List<Examination> GetAllExaminations()
        {
            throw new NotImplementedException();
        }

        public Examination GetExaminationByDoctorDateAndTime(Doctor doctor, DateTime dateAndTime)
        {
            throw new NotImplementedException();
        }

        public Examination GetExaminationById(int id)
        {
            return _context.Examinations.Find(id);
        }

        public List<Examination> GetExaminationsByDate(DateTime date)
        {
            throw new NotImplementedException();
        }

        public List<Examination> GetExaminationsByDoctor(string doctorJmbg)
        {
            throw new NotImplementedException();
        }

        public List<Examination> GetExaminationsByDoctorAndDate(Doctor doctor, DateTime dateAndTime)
        {
            throw new NotImplementedException();
        }

        public List<Examination> GetExaminationsByPatient(string patientJmbg)
        {
            return _context.Examinations.Where(e => e.PatientCard.PatientJmbg == patientJmbg).ToList();
        }

        public List<Examination> GetExaminationsByRoom(int numberOfRoom)
        {
            throw new NotImplementedException();
        }

        public List<Examination> GetExaminationsByRoomAndDates(int numberOfRoom, DateTime beginDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public List<Examination> getFreeAppointments(Doctor doctor, DateTime beginDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public void UpdateExamination(Examination examination)
        {
            _context.Examinations.Update(examination);
            _context.SaveChanges();
        }
    }
}
