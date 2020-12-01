using Model.Doctor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class ExaminationDTO
    {

        public int IdExamination { get; set; }
        public string Type { get; set; }
        public string DateAndTime { get; set; }
        public string doctor { get; set; }
        public string room { get; set; }
        public string patientCard { get; set; }

        public ExaminationDTO() { }

        public ExaminationDTO(ExaminationDTO e) {
            this.IdExamination = e.IdExamination;
            this.Type = e.Type;
            this.DateAndTime = e.DateAndTime;
            this.doctor = e.doctor;
            this.room = e.room;
            this.patientCard = e.patientCard;
             
        }
    }
}
