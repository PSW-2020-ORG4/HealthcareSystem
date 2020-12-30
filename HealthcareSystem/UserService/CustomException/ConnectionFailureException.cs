namespace UserService.CustomException
{
    public class ConnectionFailureException : UserServiceException
    {
        public ConnectionFailureException() : base() { }

        public ConnectionFailureException(string message) : base(message) { }
    }
}
