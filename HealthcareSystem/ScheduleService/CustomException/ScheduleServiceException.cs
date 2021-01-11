using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleService.CustomException
{
    public class ScheduleServiceException : Exception
    {
        public ScheduleServiceException() : base() { }
        public ScheduleServiceException(string message) : base(message) { }
    }
}
