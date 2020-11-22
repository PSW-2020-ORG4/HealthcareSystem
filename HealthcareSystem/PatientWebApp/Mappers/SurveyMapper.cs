using Backend.Model;
using PatientWebApp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientWebApp.Mappers
{
    public class SurveyMapper
    {
        public static Survey SurveyDTOToSurvey(SurveyDTO dto)
        {
            Survey survey = new Survey();
            survey.Id = dto.Id;
            survey.DoctorJmbg = dto.DoctorJmbg;
            survey.BehaviorOfDoctor = dto.BehaviorOfDoctor;
            survey.DoctorProfessionalism = dto.DoctorProfessionalism;
            survey.GettingAdviceByDoctor = dto.GettingAdviceByDoctor;
            survey.AvailabilityOfDoctor = dto.AvailabilityOfDoctor;
            survey.BehaviorOfMedicalStaff = dto.BehaviorOfMedicalStaff;
            survey.MedicalStaffProfessionalism = dto.MedicalStaffProfessionalism;
            survey.GettingAdviceByMedicalStaff = dto.GettingAdviceByMedicalStaff;
            survey.EaseInObtainingFollowupInformationAndCare = dto.EaseInObtainingFollowupInformationAndCare;
            survey.Nursing = dto.Nursing;
            survey.Cleanliness = dto.Cleanliness;
            survey.OverallRating = dto.OverallRating;
            survey.SatisfiedWithDrugAndInstrument = dto.SatisfiedWithDrugAndInstrument;

            return survey;
        }


    }
}
