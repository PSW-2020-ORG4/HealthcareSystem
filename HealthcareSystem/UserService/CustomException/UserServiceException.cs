using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.CustomException
{
    public class UserServiceException : Exception
    {
        public UserServiceException() : base() { }

        public UserServiceException(string message) : base(message) { }
    }
}
