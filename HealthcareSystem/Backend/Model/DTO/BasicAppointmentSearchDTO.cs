using System;
using System.Collections.Generic;

namespace Backend.Model.DTO
{
    public class BasicAppointmentSearchDTO
    {
        public int PatientCardId { get; set; }
        public string DoctorJmbg { get; set; }
        public ICollection<int> RequiredEquipmentTypes { get; set; }
        public DateTime EarliestDateTime { get; set; }
        public DateTime LatestDateTime { get; set; }
    }
}
