using EventSourcingService.Model.Enum;

namespace EventSourcingService.DTO.PatientSchedulingEventDTO
{
    public class StepPreviousStatisticDTO
    {
        public int NumberOfPreviousOnSpecialtyStep { get; set; }
        public int NumberOfPrevoiusOnDoctorStep { get; set; }
        public int NumberOfPreviousOnAppointmentStep { get; set; }
        public int TotalNumberOfPrevious { get; set; }
        public EventStep MostReturnedStep { get; set; }
    }
}
