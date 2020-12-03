using Backend.Model.Users;
﻿using Backend.Model;
using Model.Users;
using PatientWebApp.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using Model.PerformingExamination;
using Backend.Service.SearchSpecification.ExaminationSearch;
using Backend.Service.SearchSpecification.TherapySearch;

namespace PatientWebAppTests.CreateObjectsForTests
{
    public class TestObjectFactory
    {
        public CreatePatientDTO GetPatientDTO()
        {
            return new CreatePatientDTO();
        }

        public CreatePatient GetPatient()
        {
            return new CreatePatient();
        }

        public CreatePatientCard GetPatientCard()
        {
            return new CreatePatientCard();
        }

        public CreateSurveyDTO GetSurveyDTO()
        {
            return new CreateSurveyDTO();
        }

        public CreateSurvey GetSurvey()
        {
            return new CreateSurvey();
        }

        public CreateExamination GetExamination()
        {
            return new CreateExamination();
        }

        public CreateExaminationSearchDTO GetExaminationSearchDTO()
        {
            return new CreateExaminationSearchDTO();
        }

        public CreateTherapy GetTherapy()
        {
            return new CreateTherapy();
        }
        public CreateTherapySearchDTO GetTherapySearchDTO()
        {
            return new CreateTherapySearchDTO();
        }

        public CreateSurveyResult GetSurveyResultAboutDoctor()
        {
            return new CreateSurveyResult();
        }

        public CreateSurveyResult GetSurveyResultAboutHospital()
        {
            return new CreateSurveyResult();
        }

        public CreateSurveyResult GetSurveyResultAboutMedicalStaff()
        {
            return new CreateSurveyResult();
        }

    }
}
