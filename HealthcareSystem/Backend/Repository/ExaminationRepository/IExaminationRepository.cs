using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Manager;
using Model.PerformingExamination;
using Model.Users;

namespace Backend.Repository.ExaminationRepository
{
    public interface IExaminationRepository
    {
        Examination GetExaminationById(int id);
        List<Examination> GetAllExaminations();
        void UpdateExamination(Examination examination);
        void AddExamination(Examination examination);
        List<Examination> GetExaminationsByDate(DateTime date);
        List<Examination> GetExaminationsByPatient(string patientJmbg);
        List<Examination> GetExaminationsByRoomAndDates(int numberOfRoom, DateTime beginDate, DateTime endDate);       
        List<Examination> GetCanceledExaminationsByPatient(string patientJmbg);
        List<Examination> GetPreviousExaminationsByPatient(string patientJmbg);
        List<Examination> GetFollowingExaminationsByPatient(string patientJmbg);
        bool IsDoctorAvailable(string doctorJmbg, DateTime dateTime);
        bool IsPatientAvailable(int patientCardId, DateTime dateTime);
        bool IsRoomAvailable(int roomId, DateTime dateTime);
    }
}
