using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKorporacija.DTO
{
    class ExaminationDTO
    {
        public int Id { get; set; }
        public string TypeOfExamination { get; set; }

        public string Date { get; set; }

        public string Time { get; set; }

        public string Doctor { get; set; }

        public ExaminationDTO(int id, string typeOfExamination, string date, string time, string doctor)
        {
            this.Id = id;
            this.TypeOfExamination = typeOfExamination;
            this.Date = date;
            this.Time = time;
            this.Doctor = doctor;
        }
    }
}
