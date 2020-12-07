using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model.Enums;
using Model.PerformingExamination;
using Model.Users;
using PatientWebApp.DTOs;

namespace PatientWebApp.Mappers
{
    public class ExaminationMapper
    {
        /// <summary>
        /// /addapting object type ExaminationDTO to type Examination 
        /// </summary>
        /// <param name="dto">object type ExaminationDTO</param>
        /// <returns>object type Examination</returns>
        public static Examination ExaminationDTOToExamination(ExaminationDTO dto)
        {
            Examination examination = new Examination();
            examination.Id = dto.Id;
            examination.Type = (TypeOfExamination)Enum.Parse(typeof(TypeOfExamination), dto.Type, true);
            examination.DateAndTime = DateTime.Parse(dto.DateAndTime);
            examination.Anamnesis = dto.Anamnesis;
            examination.DoctorJmbg = dto.DoctorJmbg;
            examination.IdRoom = dto.IdRoom;
            examination.IdPatientCard = dto.PatientCardId;
            examination.IsSurveyCompleted = dto.IsSurveyCompleted;
            examination.ExaminationStatus = dto.ExaminationStatus;
            return examination;
        }

        /// <summary>
        /// /addapting object type Examination to type ExaminationDTO
        /// </summary>
        /// <param name="examination">object type Examination</param>
        /// <returns>object type ExaminationDTO</returns>
        public static ExaminationDTO ExaminationToExaminationDTO(Examination examination)
        {
            ExaminationDTO dto = new ExaminationDTO();
            dto.Id = examination.Id;
            dto.Type = examination.Type.ToString();
            dto.DateAndTime = examination.DateAndTime.ToString();
            dto.DoctorJmbg = examination.DoctorJmbg;
            dto.PatientCardId = examination.IdPatientCard;
            if (examination.PatientCard == null)
            {
                dto.PatientJmbg = "";
            }
            else
            {
                dto.PatientJmbg = examination.PatientCard.PatientJmbg;
            }
            if (examination.Doctor == null)
            {
                dto.DoctorName = "";
                dto.DoctorSurname = "";
            }
            else
            {
                dto.DoctorName = examination.Doctor.Name;
                dto.DoctorSurname = examination.Doctor.Surname;
            }
            dto.IdRoom = examination.IdRoom;
            dto.Anamnesis = examination.Anamnesis;
            dto.IsSurveyCompleted = examination.IsSurveyCompleted;
            dto.ExaminationStatus = examination.ExaminationStatus;

            return dto;
        }

    }
}
