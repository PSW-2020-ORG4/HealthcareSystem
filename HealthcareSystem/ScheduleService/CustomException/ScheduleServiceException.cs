using System;

namespace ScheduleService.CustomException
{
    public class ScheduleServiceException : Exception
    {
        public ScheduleServiceException() : base() { }
        public ScheduleServiceException(string message) : base(message) { }
    }
}
