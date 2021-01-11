using System;

namespace PatientService.DTO
{
    public class TherapySearchDTO
    {
        public DateTime? StartDate { get; set; }
        public int EndDateOperator { get; set; }
        public DateTime? EndDate { get; set; }
        public int DoctorSurnameOperator { get; set; }
        public string DoctorSurname { get; set; }
        public int DrugNameOperator { get; set; }
        public string DrugName { get; set; }
    }

}
