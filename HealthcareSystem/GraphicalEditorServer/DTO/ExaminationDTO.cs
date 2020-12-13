using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphicalEditorServer.DTO
{
    public class ExaminationDTO
    {
        public DateTime DateTime { get; set; }
        public string DoctorJmbg { get; set; }
        public int RoomId{ get; set; }
        public int PatientCardId { get; set; }

        public ExaminationDTO() { }

        public ExaminationDTO(DateTime dateTime, string doctorJmbg, int roomId, int patientCardId)
        {
            this.DateTime = dateTime;
            this.DoctorJmbg = doctorJmbg;
            this.RoomId = roomId;
            this.PatientCardId = patientCardId;
        }
    }
}
