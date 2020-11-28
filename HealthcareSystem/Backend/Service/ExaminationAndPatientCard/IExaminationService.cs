using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Manager;
using Model.PerformingExamination;
using Model.Users;

namespace Backend.Service.ExaminationAndPatientCard
{
    public interface IExaminationService
    {
        Examination GetExaminationById(int id);
        List<Examination> GetAllExaminations();
        void UpdateExamination(Examination examination);
        void DeleteExamination(int id);
        void DeleteRoomScheduledExaminations(int numberOfRoom);
        void DeleteDoctorScheduledExaminations(string doctorJmbg);
        void DeletePatientScheduledExaminations(string patientJmbg);
        void AddExamination(Examination examination);
        List<Examination> GetExaminationsByDate(DateTime date);
        Examination GetExaminationByDoctorDateAndTime(Doctor doctor, DateTime dateAndTime);
        List<Examination> GetExaminationsByPatient(string patientJmbg);
        List<Examination> GetExaminationsByDoctor(string doctorJmbg);
        List<Examination> GetExaminationsByRoom(int numberOfRoom);
        List<Examination> GetExaminationsByRoomAndDates(int numberOfRoom, DateTime beginDate, DateTime endDate);
        bool CheckDoctorAvailability(Doctor doctor, DateTime dateAndTime);
        bool CheckRoomAvailability(Room room, DateTime dateAndTime);
        List<Examination> getFreeAppointments(Doctor doctor, DateTime beginDate, DateTime endDate);
        List<Examination> fillAppointments(Doctor doctor, DateTime beginDate, DateTime endDate);
        Examination AppointmentRecommendationByDoctor(Doctor doctor, DateTime beginDate, DateTime endDate);
        Examination AppointmentRecommendationByDate(Doctor doctor, DateTime beginDate, DateTime endDate);
        List<Examination> GetExaminationsByDoctorAndDate(Doctor doctor, DateTime dateAndTime);
        List<Examination> GetExaminationsByPatientSearch(List<Examination> examinations, string startDate, string endDate, string doctorSurname, string anamesis);
       
    }
}
