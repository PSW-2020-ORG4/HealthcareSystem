using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientService.CustomException
{
    public class ValidationException : PatientServiceException
    {
        public ValidationException() : base() { }

        public ValidationException(string message) : base(message) { }
    }
}
