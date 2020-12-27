using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.CustomException
{
    public class DatabaseException : UserServiceException
    {
        public DatabaseException() : base() { }

        public DatabaseException(string message) : base(message) { }
    }
}
