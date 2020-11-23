using Backend.Model;
using Model.Users;
using PatientWebApp.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

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

        internal ICreateTestObject<SurveyDTO> GetSurveyDTO()
        {
            return new CreateSurveyDTO();
        }

        public ICreateTestObject<Survey> GetSurvey()
        {
            return new CreateSurvey();
        }

    }
}
