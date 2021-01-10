using PatientWebApp.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatientWebAppTests.CreateObjectsForTests
{
    public class CreateSurveyDTO
    {
        public SurveyDTO CreateInvalidTestObject()
        {
            return new SurveyDTO();
        }

        public SurveyDTO CreateValidTestObject()
        {
            return new SurveyDTO();
        }
    }
}
