using System;

namespace PatientWebApp.DTOs
{
    public class ScheduledExaminationDTO
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public int RoomId { get; set; }
        public string DoctorJmbg { get; set; }
        public string DoctorName { get; set; }
        public string DoctorSurname { get; set; }
        public string PatientJmbg { get; set; }
        public string PatientName { get; set; }
        public string PatientSurname { get; set; }
        public string ExaminationStatus { get; set; }
        public string ExaminationType { get; set; }
    }
}
