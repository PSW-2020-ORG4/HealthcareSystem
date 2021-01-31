namespace FeedbackAndSurveyService.CustomException
{
    public class DataStorageException : FeedbackAndSurveyServiceException
    {
        public DataStorageException() : base() { }

        public DataStorageException(string message) : base(message) { }
    }
}
