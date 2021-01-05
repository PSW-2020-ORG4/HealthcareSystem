using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackAndSurveyService.CustomException
{
    public class ActionNotPermittedException : FeedbackAndSurveyServiceException
    {
        public ActionNotPermittedException() : base() { }

        public ActionNotPermittedException(string message) : base(message) { }
    }
}
