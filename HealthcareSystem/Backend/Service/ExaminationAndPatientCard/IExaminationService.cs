using Backend.Model.PerformingExamination;
using System;
using System.Collections.Generic;

namespace Backend.Service.ExaminationAndPatientCard
{
    public interface IExaminationService
    {
        Examination GetExaminationById(int id);
        void AddExamination(Examination examination);
        List<Examination> GetAllExaminations();
        List<Examination> GetCanceledExaminationsByPatient(string patientJmbg);
        List<Examination> GetPreviousExaminationsByPatient(string patientJmbg);
        List<Examination> GetFollowingExaminationsByPatient(string patientJmbg);
        List<Examination> GetExaminationsByDate(DateTime date);
        List<Examination> GetExaminationsByPatient(string patientJmbg);
        void CancelExamination(int id);
        ICollection<Examination> GetExaminationsForPeriod(DateTime startDate, DateTime endDate);
        ICollection<Examination> GetExaminationsForPeriodAndRoom(DateTime startDate, DateTime endDate, int roomId);

    }
}
