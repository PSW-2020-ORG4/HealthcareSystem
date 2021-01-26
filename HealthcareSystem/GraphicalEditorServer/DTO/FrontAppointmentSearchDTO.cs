
using Backend.Model.Enums;
using System;
using System.Collections.Generic;

namespace GraphicalEditor.DTO
{
    public class FrontAppointmentSearchDTO
    {
        public int PatientCardId { get; set; }
        public string DoctorJmbg { get; set; }
        public ICollection<int> RequiredEquipmentTypes { get; set; }
        public DateTime EarliestDateTime { get; set; }
        public DateTime LatestDateTime { get; set; }
        public SearchPriority Priority { get; set; }
        public int SpecialtyId { get; set; }

        public FrontAppointmentSearchDTO() { }
        public FrontAppointmentSearchDTO(int patientCardId, string doctorJmbg,
                                                 ICollection<int> requiredEquipmentTypes,
                                                 DateTime earliestDateTime,
                                                 DateTime latestDateTime,
                                                 SearchPriority priority,
                                                 int specialtyId)
        {
            PatientCardId = patientCardId;
            DoctorJmbg = doctorJmbg;
            RequiredEquipmentTypes = requiredEquipmentTypes;
            EarliestDateTime = earliestDateTime;
            LatestDateTime = latestDateTime;
            Priority = priority;
            SpecialtyId = specialtyId;
        }
    }
}
