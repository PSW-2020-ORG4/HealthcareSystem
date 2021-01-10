using PatientWebApp.DTOs.Enums;
using System;

namespace PatientWebApp.DTOs
{
    public class TherapySearchDTO
    {
        public DateTime? StartDate { get; set; }
        public LogicalOperator EndDateOperator { get; set; }
        public DateTime? EndDate { get; set; }
        public LogicalOperator DoctorSurnameOperator { get; set; }
        public string DoctorSurname { get; set; }
        public LogicalOperator DrugNameOperator { get; set; }
        public string DrugName { get; set; }
    }
}
