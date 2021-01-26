namespace Backend.Model.Exceptions
{
    public class NotFoundException : HealthClinicException
    {
        public NotFoundException() : base() { }
        public NotFoundException(string message) : base(message) { }
    }
}
