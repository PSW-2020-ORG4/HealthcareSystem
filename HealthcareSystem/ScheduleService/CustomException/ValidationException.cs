using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleService.CustomException
{
    public class ValidationException : ScheduleServiceException
    {
        public ValidationException() : base() { }
        public ValidationException(string message) : base(message) { }
    }
}
