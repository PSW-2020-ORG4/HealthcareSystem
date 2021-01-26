namespace FeedbackAndSurveyService.CustomException
{
    public class ActionNotPermittedException : FeedbackAndSurveyServiceException
    {
        public ActionNotPermittedException() : base() { }

        public ActionNotPermittedException(string message) : base(message) { }
    }
}
