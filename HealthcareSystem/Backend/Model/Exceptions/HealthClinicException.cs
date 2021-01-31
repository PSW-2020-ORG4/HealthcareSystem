using System;

namespace Backend.Model.Exceptions
{
    public class HealthClinicException : Exception
    {
        public HealthClinicException() : base() { }
        public HealthClinicException(String message) : base(message) { }
    }
}
