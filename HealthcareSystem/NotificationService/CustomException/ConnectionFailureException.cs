namespace NotificationService
{
    public class ConnectionFailureException : NotificationServiceException
    {
        public ConnectionFailureException() : base() { }

        public ConnectionFailureException(string message) : base(message) { }
    }
}
