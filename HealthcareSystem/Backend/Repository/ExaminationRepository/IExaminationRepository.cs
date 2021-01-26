using Backend.Model.PerformingExamination;
using System;
using System.Collections.Generic;

namespace Backend.Repository.ExaminationRepository
{
    public interface IExaminationRepository
    {
        Examination GetExaminationById(int id);
        List<Examination> GetAllExaminations();
        void UpdateExamination(Examination examination);
        int AddExamination(Examination examination);
        void DeleteExaminationRepository(int id);

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
        void ReScheduleAppointment(Examination examinationForSchedule, Examination examinationForReschedule, Examination shiftedExamination);

    }
}
