namespace ScheduleService.CustomException
{
    public class ValidationException : ScheduleServiceException
    {
        public ValidationException() : base() { }
        public ValidationException(string message) : base(message) { }
    }
}
