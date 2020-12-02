﻿using Backend.Service;
using PatientWebApp.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Backend.Model.Exceptions;
using ValidationException = Backend.Model.Exceptions.ValidationException;

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
            if (EmptyCheck(surveyDTO))
                throw new ValidationException("Survey cannot be null.");
            if (!IsValidateGrades(surveyDTO))
                throw new ValidationException("The rating is out of range.");
        }

        private bool IsValidateGrades(SurveyDTO surveyDTO)
        {
            if (!IsValidGrade(surveyDTO.BehaviorOfDoctor))
                return false;
            if (!IsValidGrade(surveyDTO.DoctorProfessionalism))
                return false;
            if (!IsValidGrade(surveyDTO.GettingAdviceByDoctor))
                return false;
            if (!IsValidGrade(surveyDTO.AvailabilityOfDoctor))
                return false;
            if (!IsValidGrade(surveyDTO.BehaviorOfMedicalStaff))
                return false;
            if (!IsValidGrade(surveyDTO.MedicalStaffProfessionalism))
                return false;
            if (!IsValidGrade(surveyDTO.GettingAdviceByMedicalStaff))
                return false;
            if (!IsValidGrade(surveyDTO.EaseInObtainingFollowUpInformation))
                return false;
            if (!IsValidGrade(surveyDTO.Nursing))
                return false;
            if (!IsValidGrade(surveyDTO.Cleanliness))
                return false;
            if (!IsValidGrade(surveyDTO.OverallRating))
                return false;
            if (!IsValidGrade(surveyDTO.SatisfiedWithDrugAndInstrument))
                return false;
            return true;
        }

        private bool IsValidGrade(int grade)
        {
            if (grade < 6 && grade > 0) return true;
            return false;
        }

        private bool EmptyCheck(SurveyDTO surveyDTO)
        {
            if (surveyDTO == null) return true;
            return false;
        }
        
    }
}
