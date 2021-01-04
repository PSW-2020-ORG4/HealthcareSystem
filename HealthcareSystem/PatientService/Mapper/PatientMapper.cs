using PatientService.DTO;
using PatientService.Model;
using PatientService.Model.Memento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientService.Mapper
{
    public static class PatientMapper
    {
        public static MedicalInfoDTO ToMedicalInfoDTO(this Patient patient)
        {
            PatientMemento memento = patient.GetMemento();
            return new MedicalInfoDTO()
            {
                Allergies = memento.Allergies,
                MedicalHistory = memento.MedicalHistory,
                BloodType = memento.BloodType.ToString(),
                RhFactor = memento.RhFactor.ToString(),
                InsuranceNumber = memento.InsuranceNumber
            };
        }
    }
}
