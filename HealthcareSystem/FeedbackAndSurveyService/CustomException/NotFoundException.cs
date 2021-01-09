using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackAndSurveyService.CustomException
{
    public class NotFoundException : FeedbackAndSurveyServiceException
    {
        public NotFoundException() : base() { }

        public NotFoundException(string message) : base(message) { }
    }
}
