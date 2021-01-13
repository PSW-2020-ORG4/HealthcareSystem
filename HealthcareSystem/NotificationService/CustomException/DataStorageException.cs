namespace NotificationService
{
    public class DataStorageException : NotificationServiceException
    {
        public DataStorageException() : base() { }

        public DataStorageException(string message) : base(message) { }
    }
}
