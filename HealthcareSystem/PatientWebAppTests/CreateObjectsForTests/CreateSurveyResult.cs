using Backend.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatientWebAppTests.CreateObjectsForTests
{
    class CreateSurveyResult : ICreateTestObject<SurveyResult>
    {
        public SurveyResult CreateInvalidTestObject()
        {
            return new SurveyResult("Rated item", 7, 1, 0, -3, 4, 0);
        }

        public SurveyResult CreateValidTestObject()
        {
            return new SurveyResult("Rated item", 4, 1, 2, 5, 4, 3);
        }
    }
}
