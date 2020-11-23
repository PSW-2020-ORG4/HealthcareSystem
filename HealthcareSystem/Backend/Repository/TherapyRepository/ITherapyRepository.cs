using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.PerformingExamination;

namespace Backend.Repository.TherapyRepository
{
    public interface ITherapyRepository
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
        void DeleteDrugTherapies(int idDrug);
        List<Therapy> GetTherapyByPatientSearch(Therapy therapy);


    }
}
