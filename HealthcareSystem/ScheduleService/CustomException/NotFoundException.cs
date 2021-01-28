namespace ScheduleService.CustomException
{
    public class NotFoundException : ScheduleServiceException
    {
        public NotFoundException() : base() { }
        public NotFoundException(string message) : base(message) { }
    }
}
