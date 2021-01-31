using Backend.Model;

namespace PatientWebAppTests.CreateObjectsForTests
{
    public class CreateSurveyResult
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
