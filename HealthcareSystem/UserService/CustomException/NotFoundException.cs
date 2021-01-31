namespace UserService.CustomException
{
    public class NotFoundException : UserServiceException
    {
        public NotFoundException() : base() { }

        public NotFoundException(string message) : base(message) { }
    }
}
