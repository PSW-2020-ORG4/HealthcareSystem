using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientService.DTO
{
    public class TherapyDTO
    {
        public int Id { get; set; }
        public string Diagnosis { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int DailyDose { get; set; }
        public int IdDrug { get; set; }
        public string DrugName { get; set; }
        public string DoctorName { get; set; }
        public string DoctorSurname { get; set; }
    }
}
