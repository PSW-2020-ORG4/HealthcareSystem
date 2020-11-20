using Backend.Service;
using PatientWebApp.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PatientWebApp.Validators
{
    public class SurveyValidator
    {
        private readonly ISurveyService _surveyService;

        public SurveyValidator(ISurveyService surveyService)
        {
            _surveyService = surveyService;
        }

        public void ValidateSurveyFields(SurveyDTO surveyDTO)
        {
            if (surveyDTO == null)
                throw new ValidationException("Survey cannot be null.");
            if (surveyDTO.DoctorJmbg == null || surveyDTO.DoctorJmbg == "")
                throw new ValidationException("The doctor does not exist!");
            if (!IsValidGrade(surveyDTO.BehaviorOfDoctor))
                throw new ValidationException("The rating is out of range.");
            if (!IsValidGrade(surveyDTO.DoctorProfessionalism))
                throw new ValidationException("The rating is out of range.");
            if (!IsValidGrade(surveyDTO.GettingAdviceByDoctor))
                throw new ValidationException("The rating is out of range.");
            if (!IsValidGrade(surveyDTO.AvailabilityOfDoctor))
                throw new ValidationException("The rating is out of range."); 
            if (!IsValidGrade(surveyDTO.BehaviorOfMedicalStaff))
                throw new ValidationException("The rating is out of range."); 
            if (!IsValidGrade(surveyDTO.MedicalStaffProfessionalism))
                throw new ValidationException("The rating is out of range."); 
            if (!IsValidGrade(surveyDTO.GettingAdviceByMedicalStaff))
                throw new ValidationException("The rating is out of range."); 
            if (!IsValidGrade(surveyDTO.EaseInObtainingFollowupInformationAndCare))
                throw new ValidationException("The rating is out of range."); 
            if (!IsValidGrade(surveyDTO.Nursing))
                throw new ValidationException("The rating is out of range."); 
            if (!IsValidGrade(surveyDTO.Cleanliness))
                throw new ValidationException("The rating is out of range."); 
            if (!IsValidGrade(surveyDTO.OverallRating))
                throw new ValidationException("The rating is out of range.");
            if (!IsValidGrade(surveyDTO.SatisfiedWithDrugAndInstrument))
                throw new ValidationException("The rating is out of range.");
        }

        private bool IsValidGrade(int grade)
        {
            if (grade < 6 && grade > 0) return true;
            return false;
        }

    }
}
