using PatientService.DTO;
using PatientService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientService.Service
{
    public interface IPatientService
    {
        Patient Get(string jmbg);
        IEnumerable<Examination> GetExaminations(string jmbg);
        IEnumerable<Examination> GetExaminations(string jmbg, ExaminationSearchDTO parameters);
        IEnumerable<Therapy> GetTherapies(string jmbg);
        IEnumerable<Therapy> GetTherapies(string jmbg, TherapySearchDTO parameters);
        void UpdateMedicalInfo(string jmbg, MedicalInfoUpdateDTO medicalInfoUpdate);
        void Add(GuestPatientDTO guestPatient);
        IEnumerable<Therapy> GetTherapiesForExamination(string jmbg, int id);
    }
}
