namespace PatientService.CustomException
{
    public class ValidationException : PatientServiceException
    {
        public ValidationException() : base() { }

        public ValidationException(string message) : base(message) { }
    }
}
