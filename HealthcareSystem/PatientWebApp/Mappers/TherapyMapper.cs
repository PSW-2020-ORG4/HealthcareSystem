using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model.PerformingExamination;
using PatientWebApp.DTOs;

namespace PatientWebApp.Mappers
{
    public class TherapyMapper
    {
        /// <summary>
        /// /addapting object type TherapyDTO to type Therapy 
        /// </summary>
        /// <param name="dto">object type TherapyDTO</param>
        /// <returns>object type Therapy</returns>
        public static Therapy TherapyDTOToTherapy(TherapyDTO dto)
        {
            Therapy therapy = new Therapy();
            therapy.Id = dto.Id;
            therapy.Diagnosis = dto.Diagnosis;
            therapy.DailyDose = dto.DailyDose;
            therapy.IdDrug = dto.IdDrug;
            therapy.IdExamination = dto.IdExamination;
            therapy.StartDate = DateTime.Parse(dto.StartDate);
            therapy.EndDate = DateTime.Parse(dto.StartDate);
            return therapy;
        }

        /// <summary>
        /// /addapting object type Therapy to type TherapyDTO
        /// </summary>
        /// <param name="therapy">object type Therapy</param>
        /// <returns>object type TherapyDTO</returns>
        public static TherapyDTO TherapyToTherapyDTO(Therapy therapy)
        {
            TherapyDTO dto = new TherapyDTO();
            dto.Id = therapy.Id;
            dto.Diagnosis = therapy.Diagnosis;
            dto.StartDate = therapy.StartDate.ToString("dd/MM/yyyy");
            dto.EndDate = therapy.EndDate.ToString("dd/MM/yyyy");
            dto.DailyDose = therapy.DailyDose;
            dto.IdDrug = therapy.IdDrug;
            dto.IdExamination = therapy.IdExamination;
            dto.PatientJmbg = therapy.Examination.PatientCard.PatientJmbg;
            
            dto.DrugName = therapy.Drug.Name;
            
             dto.DoctorName = therapy.Examination.Doctor.Name;
             dto.DoctorSurname = therapy.Examination.Doctor.Surname;
            

            return dto;
        }
    }
}
