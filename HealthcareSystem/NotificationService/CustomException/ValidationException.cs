namespace NotificationService
{
    public class ValidationException : NotificationServiceException
    {
        public ValidationException() : base() { }

        public ValidationException(string message) : base(message) { }
    }
}
