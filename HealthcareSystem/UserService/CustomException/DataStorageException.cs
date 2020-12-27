using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.CustomException
{
    public class DataStorageException : UserServiceException
    {
        public DataStorageException() : base() { }

        public DataStorageException(string message) : base(message) { }
    }
}
