namespace Backend.Model.Exceptions
{
    public class BadRequestException : HealthClinicException
    {
        public BadRequestException() : base() { }
        public BadRequestException(string message) : base(message) { }
    }
}
