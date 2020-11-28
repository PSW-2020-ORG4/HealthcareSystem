using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientWebApp.DTOs
{
    public class TherapyDTO
    {
        public int Id { get; set; }
        public string Diagnosis { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int DailyDose { get; set; }
        public int IdDrug { get; set; }
        public string PatientJmbg { get; set; }
        public int IdExamination { get; set; }
        public string DoctorName { get; set; }
        public string DoctorSurname { get; set; }
        public string DrugName { get; set; }

        public TherapyDTO()
        {
        }

    }
}
