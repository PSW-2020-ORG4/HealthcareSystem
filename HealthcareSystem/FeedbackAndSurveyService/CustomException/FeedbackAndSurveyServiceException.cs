using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackAndSurveyService.CustomException
{
    public class FeedbackAndSurveyServiceException : Exception
    {
        public FeedbackAndSurveyServiceException() : base() { }

        public FeedbackAndSurveyServiceException(string message) : base(message) { }
    }
}
