using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleService.DTO
{
    public class UnscheduledExaminationDTO
    {
        public DateTime StartTime { get; set; }
        public int RoomId { get; set; }
        public string DoctorJmbg { get; set; }
        public string DoctorName { get; set; }
        public string DoctorSurname { get; set; }
        public string PatientJmbg { get; set; }
        public string PatientName { get; set; }
        public string PatientSurname { get; set; }
        public string ExaminationType { get; set; }

    }
}
