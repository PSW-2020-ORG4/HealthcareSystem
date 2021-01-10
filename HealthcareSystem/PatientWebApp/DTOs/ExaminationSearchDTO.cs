using System;

namespace PatientService.DTO
{
    public class ExaminationSearchDTO
    {
        public DateTime? StartDate { get; set; }
        public int EndDateOperator { get; set; }
        public DateTime? EndDate { get; set; }
        public int DoctorSurnameOperator { get; set; }
        public string DoctorSurname { get; set; }
        public int AnamnesisOperator { get; set; }
        public string Anamnesis { get; set; }
    }
}
