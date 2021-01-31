namespace FeedbackAndSurveyService.CustomException
{
    public class NotFoundException : FeedbackAndSurveyServiceException
    {
        public NotFoundException() : base() { }

        public NotFoundException(string message) : base(message) { }
    }
}
