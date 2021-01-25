using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientWebApp.DTOs
{
    public class AddStepSchedulingEventDTO
    {
        public int StartSchedulingEventId { get; set; }
        public int UserAge { get; set; }
        public int UserGender { get; set; }
        public int EventStep { get; set; }
        public int ClickEvent { get; set; }
    }
}
