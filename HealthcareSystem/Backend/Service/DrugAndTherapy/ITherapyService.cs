using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.PerformingExamination;

namespace Backend.Service.DrugAndTherapy
{
    public interface ITherapyService
    {
        void AddTherapy(Therapy therapy);
        void UpdateTherapy(Therapy therapy);
        Therapy GetTherapyById(int id);
        void DeleteTherapy(int idTherapy);
        List<Therapy> GetTherapyByPatient(string patientJmbg);
        List<Therapy> GetTherapyByDrug(int idDrug);
        List<Therapy> GetActiveTherapyByPatient(string patientJmbg);
        List<Therapy> GetTherapyForNextSevenDaysByPatient(string patientJmbg);
        void DeletePatientTherapies(string patientJmbg);
        void DeleteDrugTherapies(int id);
        List<Therapy> GetTherapiesByExaminationSearch(List<Therapy> therapies, string startDate, string endDate, string doctorSurname, string drugName);
        

    }
}
