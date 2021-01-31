using EventSourcingService.Model.Enum;

namespace EventSourcingService.DTO.PatientSchedulingEventDTO
{
    public class StepClosureStatisticDTO
    {
        public int NumberOfClosuresOnDateStep { get; set; }
        public int NumberOfClosuresOnSpecialtyStep { get; set; }
        public int NumberOfClosuresOnDoctorStep { get; set; }
        public int NumberOfClosuresOnAppointmentStep { get; set; }
        public int TotalNumberOfClosures { get; set; }
        public EventStep MostClosedStep { get; set; }
    }
}
