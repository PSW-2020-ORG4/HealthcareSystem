namespace EventSourcingService.DTO
{
    public class SuccessfulAndUnsuccessfulSchedulingDTO
    {
        public int NumberOfSuccessfulScheduling { get; set; }
        public int NumberOfUnsuccessfulScheduling { get; set; }
        public int NumberOfScheduling { get; set; }
    }
}
