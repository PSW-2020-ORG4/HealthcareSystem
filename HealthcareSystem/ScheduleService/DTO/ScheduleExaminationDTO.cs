using System;

namespace ScheduleService.DTO
{
    public class ScheduleExaminationDTO
    {
        public DateTime StartTime { get; set; }
        public string DoctorJmbg { get; set; }
        public string PatientJmbg { get; set; }
        public int RoomId { get; set; }
    }
}
