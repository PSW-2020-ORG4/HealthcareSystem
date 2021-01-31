namespace FeedbackAndSurveyService.CustomException
{
    public class ValidationException : FeedbackAndSurveyServiceException
    {
        public ValidationException() : base() { }

        public ValidationException(string message) : base(message) { }
    }
}
