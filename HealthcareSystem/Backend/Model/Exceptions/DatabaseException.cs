namespace Backend.Model.Exceptions
{
    public class DatabaseException : HealthClinicException
    {
        public DatabaseException() : base() { }
        public DatabaseException(string message) : base(message) { }
    }
}
