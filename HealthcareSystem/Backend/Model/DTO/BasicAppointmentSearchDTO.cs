using Backend.Model.Exceptions;
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

        public BasicAppointmentSearchDTO(int patientCardId, string doctorJmbg, ICollection<int> requiredEquipmentTypes, DateTime earliestDateTime, DateTime latestDateTime)
        {
            PatientCardId = patientCardId;
            DoctorJmbg = doctorJmbg;
            RequiredEquipmentTypes = requiredEquipmentTypes;
            EarliestDateTime = earliestDateTime;
            LatestDateTime = latestDateTime;
        }

        public void IsAppointmentValid()
        {
            if (string.IsNullOrEmpty(DoctorJmbg))
                throw new ValidationException("Doctor jmbg cannot be null or empty.");
            if (EarliestDateTime == null)
                throw new ValidationException("Earliest date cannot be null.");
            if (LatestDateTime == null)
                throw new ValidationException("Latest date cannot be null");
        }
    }
}
