using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjekatZdravoKorporacija.ModelDTO
{
    class ExaminationDTO
    {
        public int Id { get; set; }
        public string Doctor { get; set; }

        public string Patient { get; set; }

        public string Room { get; set; }

        public string TypeOfExamination { get; set; }

        public string Time { get; set; }
        public string Date { get; set; }

        public ExaminationDTO(int id, string doctor,string patient,string room,string type,string date,string time)
        {
            this.Id = id;
            this.Doctor = doctor;
            this.Patient = patient;
            this.Room = room;
            this.TypeOfExamination = type;
            this.Time = time;
            this.Date = date;
        }
    }
}
