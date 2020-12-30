using System;

namespace UserService.CustomException
{
    public class UserServiceException : Exception
    {
        public UserServiceException() : base() { }

        public UserServiceException(string message) : base(message) { }
    }
}
