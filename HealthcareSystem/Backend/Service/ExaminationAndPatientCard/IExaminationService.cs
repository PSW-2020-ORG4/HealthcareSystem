using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Service.SearchSpecification;
using Backend.Service.SearchSpecification.ExaminationSearch;
using Model.Manager;
using Model.PerformingExamination;
using Model.Users;

namespace Backend.Service.ExaminationAndPatientCard
{
    public interface IExaminationService
    {
        Examination GetExaminationById(int id);
        List<Examination> GetAllExaminations();
        List<Examination> GetCanceledExaminationsByPatient(string patientJmbg);
        List<Examination> GetPreviousExaminationsByPatient(string patientJmbg);
        List<Examination> GetFollowingExaminationsByPatient(string patientJmbg);
        List<Examination> GetExaminationsByDate(DateTime date);
        List<Examination> GetExaminationsByPatient(string patientJmbg);        
        List<Examination> AdvancedSearch(ExaminationSearchDTO parameters);
        void AddExamination(Examination examination);
        void CompleteSurveyAboutExamination(int id);
        void CancelExamination(int id);

    }
}
