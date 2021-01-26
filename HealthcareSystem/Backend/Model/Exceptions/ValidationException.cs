namespace Backend.Model.Exceptions
{
    public class ValidationException : HealthClinicException
    {
        public ValidationException() : base() { }
        public ValidationException(string message) : base(message) { }
    }
}
