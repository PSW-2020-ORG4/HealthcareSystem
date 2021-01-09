using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientService.DTO
{
    public class ExaminationDTO
    {
        public int Id { get; set; }
        public DateTime DateAndTime { get; set; }
        public string DoctorJmbg { get; set; }
        public string DoctorName { get; set; }
        public string DoctorSurname { get; set; }
        public string Anamnesis { get; set; }
    }
}
