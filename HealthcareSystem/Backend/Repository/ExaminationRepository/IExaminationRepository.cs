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
        int AddExamination(Examination examination);
        List<Examination> GetExaminationsByDate(DateTime date);
        List<Examination> GetExaminationsByPatient(string patientJmbg);
        List<Examination> GetExaminationsByRoomAndDates(int numberOfRoom, DateTime beginDate, DateTime endDate);
        List<Examination> GetCanceledExaminationsByPatient(string patientJmbg);
        List<Examination> GetPreviousExaminationsByPatient(string patientJmbg);
        List<Examination> GetFollowingExaminationsByPatient(string patientJmbg);
        ICollection<Examination> GetExaminationsByDoctorAndDateTime(string doctorJmbg, DateTime dateTime);
        ICollection<Examination> GetExaminationsByRoomAndDateTime(int roomId, DateTime dateTime);
        ICollection<Examination> GetExaminationsByPatientAndDateTime(int patientCardId, DateTime dateTime);
        ICollection<Examination> GetFollowingExaminationsByRoom(int roomId);
        ICollection<Examination> GetExaminationsForPeriod(DateTime startDate, DateTime endDate);

    }
}
