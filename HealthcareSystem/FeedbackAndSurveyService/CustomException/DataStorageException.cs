using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackAndSurveyService.CustomException
{
    public class DataStorageException : FeedbackAndSurveyServiceException
    {
        public DataStorageException() : base() { }

        public DataStorageException(string message) : base(message) { }
    }
}
