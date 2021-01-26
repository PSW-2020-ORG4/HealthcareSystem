using PatientWebApp.DTOs;

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
