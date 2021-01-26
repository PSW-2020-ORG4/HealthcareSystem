using System;

namespace FeedbackAndSurveyService.CustomException
{
    public class FeedbackAndSurveyServiceException : Exception
    {
        public FeedbackAndSurveyServiceException() : base() { }

        public FeedbackAndSurveyServiceException(string message) : base(message) { }
    }
}
