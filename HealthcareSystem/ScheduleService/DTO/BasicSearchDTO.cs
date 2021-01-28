using System;
using System.Collections.Generic;

namespace ScheduleService.DTO
{
    public class BasicSearchDTO
    {
        public string PatientJmbg { get; set; }
        public string DoctorJmbg { get; set; }
        public ICollection<int> RequiredEquipmentTypes { get; set; }
        public DateTime EarliestDateTime { get; set; }
        public DateTime LatestDateTime { get; set; }

        public BasicSearchDTO()
        {
            RequiredEquipmentTypes = new List<int>();
        }
    }
}
