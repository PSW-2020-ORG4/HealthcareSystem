using Backend.Model.Users;
﻿using Backend.Model;
using Model.Users;
using PatientWebApp.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using Model.PerformingExamination;

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

        public ICreateTestObject<ExaminationDTO> GetExaminationDTO()
        {
            return new CreateExaminationDTO();
        }

        public ICreateTestObject<Examination> GetExamination()
        {
            return new CreateExamination();
        }
    }
}
