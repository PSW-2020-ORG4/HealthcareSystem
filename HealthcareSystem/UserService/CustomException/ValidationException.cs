using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.CustomException
{
    public class ValidationException : UserServiceException
    {
        public ValidationException() : base() { }

        public ValidationException(string message) : base(message) { }
    }
}
