using PatientService.DTO;
using PatientService.Model;
using PatientService.Model.Memento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientService.Mapper
{
    public static class TherapyMapper
    {
        public static TherapyDTO ToTherapyDTO(this Therapy therapy)
        {
            TherapyMemento memento = therapy.GetMemento();
            return new TherapyDTO()
            {
                Id = memento.Id,
                DailyDose = memento.DailyDose,
                Diagnosis = memento.Diagnosis,
                DoctorName = memento.DoctorName,
                DoctorSurname = memento.DoctorSurname,
                DrugName = memento.DrugName,
                IdDrug = memento.DrugId,
                EndDate = memento.EndDate,
                StartDate = memento.StartDate,
                ExaminationId = memento.ExaminationId
            };
        }
    }
}
