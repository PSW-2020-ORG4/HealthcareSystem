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

        public void IsAppointmentValid()
        {
            if (string.IsNullOrEmpty(DoctorJmbg))
                throw new ValidationException("Doctor jmbg cannot be null or empty.");
            if (DateTime.Compare(EarliestDateTime,DateTime.Now) <= 0)
                throw new ValidationException("Earliest date must be greater than " + DateTime.Now.ToShortDateString() + ".");
            if (DateTime.Compare(LatestDateTime, DateTime.Now) <= 0)
                throw new ValidationException("Latest date must be greater than " + DateTime.Now.ToShortDateString() + ".");
        }
    }
}
