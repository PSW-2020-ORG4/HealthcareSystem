using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientWebApp.DTOs
{
    public class SurveyDTO
    {
        public int Id { get; set; }
        public int BehaviorOfDoctor { get; set; }
        public int DoctorProfessionalism { get; set; }
        public int GettingAdviceByDoctor { get; set; }
        public int AvailabilityOfDoctor { get; set; }
        public int BehaviorOfMedicalStaff { get; set; }
        public int MedicalStaffProfessionalism { get; set; }
        public int GettingAdviceByMedicalStaff { get; set; }
        public int EaseInObtainingFollowupInformationAndCare { get; set; }
        public int Nursing { get; set; }
        public int Cleanliness { get; set; }
        public int OverallRating { get; set; }
        public int SatisfiedWithDrugAndInstrument { get; set; }
        public string DoctorJmbg { get; set; }
        public string DoctorName { get; set; }
        public string DoctorSurname { get; set; }

        public SurveyDTO() { }

    }
}
