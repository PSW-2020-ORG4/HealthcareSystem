namespace UserService.CustomException
{
    public class ValidationException : UserServiceException
    {
        public ValidationException() : base() { }

        public ValidationException(string message) : base(message) { }
    }
}
