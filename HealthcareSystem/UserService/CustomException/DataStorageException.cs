namespace UserService.CustomException
{
    public class DataStorageException : UserServiceException
    {
        public DataStorageException() : base() { }

        public DataStorageException(string message) : base(message) { }
    }
}
