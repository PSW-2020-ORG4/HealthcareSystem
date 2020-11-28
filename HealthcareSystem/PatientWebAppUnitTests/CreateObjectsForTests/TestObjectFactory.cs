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
        public ICreateTestObject<PatientDTO> GetPatientDTO()
        {
            return new CreatePatientDTO();
        }

        public ICreateTestObject<Patient> GetPatient()
        {
            return new CreatePatient();
        }

        public ICreateTestObject<PatientCard> GetPatientCard()
        {
            return new CreatePatientCard();
        }

        public ICreateTestObject<SurveyDTO> GetSurveyDTO()
        {
            return new CreateSurveyDTO();
        }

        public ICreateTestObject<Survey> GetSurvey()
        {
            return new CreateSurvey();
        }

        public ICreateTestObject<Examination> GetExamination()
        {
            return new CreateExamination();
        }

        public ICreateTestObject<ExaminationSearchDTO> GetExaminationSearchDTO()
        {
            return new CreateExaminationSearchDTO();
        }

        public ICreateTestObject<Therapy> GetTherapy()
        {
            return new CreateTherapy();
        }
        public ICreateTestObject<TherapySearchDTO> GetTherapySearchDTO()
        {
            return new CreateTherapySearchDTO();
        }
    }
}
