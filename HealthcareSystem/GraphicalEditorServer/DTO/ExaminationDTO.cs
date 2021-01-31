using System;

namespace GraphicalEditorServer.DTO
{
    public class ExaminationDTO
    {
        public DateTime DateTime { get; set; }
        public DoctorDTO Doctor { get; set; }
        public int RoomId { get; set; }
        public int PatientCardId { get; set; }

        public ExaminationDTO() { }

        public ExaminationDTO(DateTime dateTime, DoctorDTO doctor, int roomId, int patientCardId)
        {
            this.DateTime = dateTime;
            this.Doctor = doctor;
            this.RoomId = roomId;
            this.PatientCardId = patientCardId;
        }
    }
}
