using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleService.DTO
{
    public class ExaminationDTO
    {
        public DateTime StartTime { get; set; }
        public string DoctorJmbg { get; set; }
        public string PatientJmbg { get; set; }
        public int RoomId { get; set; }
    }
}
