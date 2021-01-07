using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientService.CustomException
{
    public class PatientServiceException : Exception
    {
        public PatientServiceException() : base() { }

        public PatientServiceException(string message) : base(message) { }
    }
}
