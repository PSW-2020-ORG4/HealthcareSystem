using PatientWebApp.DTOs.Enums;
using System;

namespace PatientWebApp.DTOs
{
    public class ExaminationSearchDTO
    {
        public DateTime? StartDate { get; set; }
        public LogicalOperator EndDateOperator { get; set; }
        public DateTime? EndDate { get; set; }
        public LogicalOperator DoctorSurnameOperator { get; set; }
        public string DoctorSurname { get; set; }
        public LogicalOperator AnamnesisOperator { get; set; }
        public string Anamnesis { get; set; }
    }
}
