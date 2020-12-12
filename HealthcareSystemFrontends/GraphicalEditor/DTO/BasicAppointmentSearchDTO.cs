using System;
using System.Collections.Generic;

namespace GraphicalEditor.DTO
{
    public class BasicAppointmentSearchDTO
    {
        public int PatientCardId { get; set; }
        public string DoctorJmbg { get; set; }
        public ICollection<int> RequiredEquipmentTypes { get; set; }
        public DateTime EarliestDateTime { get; set; }
        public DateTime LatestDateTime { get; set; }

        public BasicAppointmentSearchDTO() { }

        public BasicAppointmentSearchDTO(int patientCardId, string doctorJmbg, ICollection<int> requiredEquipmentTypes, DateTime earliestDateTime, DateTime latestDateTime)
        {
            PatientCardId = patientCardId;
            DoctorJmbg = doctorJmbg;
            RequiredEquipmentTypes = requiredEquipmentTypes;
            EarliestDateTime = earliestDateTime;
            LatestDateTime = latestDateTime;
        }
    }
}
