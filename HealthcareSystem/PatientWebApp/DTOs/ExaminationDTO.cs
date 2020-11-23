using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model.PerformingExamination;

namespace PatientWebApp.DTOs
{
    public class ExaminationDTO
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string DateAndTime { get; set; }
        public string DoctorJmbg { get; set; }
        public string DoctorName { get; set; }
        public string DoctorSurname { get; set; }
        public int IdRoom { get; set; }
        public string Anamnesis { get; set; }

        public ExaminationDTO()
        {
        }

    }
}
