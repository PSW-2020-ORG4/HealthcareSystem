using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientService.Model.Memento
{
    public class TherapyMemento : IMemento
    {
        public int Id { get; set; }
        public string DoctorJmbg { get; set; }
        public string DoctorName { get; set; }
        public string DoctorSurname { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int DrugId { get; set; }
        public string DrugName { get; set; }
        public int DailyDose { get; set; }
        public string Diagnosis { get; set; }
        public int ExaminationId { get; set; }
    }
}
