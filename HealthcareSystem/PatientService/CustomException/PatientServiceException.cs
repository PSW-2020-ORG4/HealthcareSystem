using System;

namespace PatientService.CustomException
{
    public class PatientServiceException : Exception
    {
        public PatientServiceException() : base() { }

        public PatientServiceException(string message) : base(message) { }
    }
}
