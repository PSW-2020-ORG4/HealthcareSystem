using System;

namespace PatientWebApp.DTOs
{
    public class AddEndSchedulingEventDTO
    {
        public DateTime StartSchedulingEventTime { get; set; }
        public int UserAge { get; set; }
        public int UserGender { get; set; }
        public int ReasonForEndOfAppointment { get; set; }
    }
}
