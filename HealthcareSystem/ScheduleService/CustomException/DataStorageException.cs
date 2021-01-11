using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleService.CustomException
{
    public class DataStorageException : ScheduleServiceException
    {
        public DataStorageException() : base() { }
        public DataStorageException(string message) : base(message) { }
    }
}
