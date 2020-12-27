using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.CustomException
{
    public class ExternalConnectionException : UserServiceException
    {
        public ExternalConnectionException() : base() { }

        public ExternalConnectionException(string message) : base(message) { }
    }
}
