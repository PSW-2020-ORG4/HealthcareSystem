using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
