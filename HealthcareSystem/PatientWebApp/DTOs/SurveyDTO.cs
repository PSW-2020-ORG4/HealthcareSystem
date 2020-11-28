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


        public SurveyDTO() { }

        public SurveyDTO(int id, int behaviorOfDoctor, int doctorProfessionalism, int gettingAdviceByDoctor, int availabilityOfDoctor, 
            int behaviorOfMedicalStaff, int medicalStaffProfessionalism, int gettingAdviceByMedicalStaff, int easeInObtainingFollowupInformationAndCare, 
            int nursing, int cleanliness, int overallRating, int satisfiedWithDrugAndInstrument, string doctorJmbg)
        {
            Id = id;
            BehaviorOfDoctor = behaviorOfDoctor;
            DoctorProfessionalism = doctorProfessionalism;
            GettingAdviceByDoctor = gettingAdviceByDoctor;
            AvailabilityOfDoctor = availabilityOfDoctor;
            BehaviorOfMedicalStaff = behaviorOfMedicalStaff;
            MedicalStaffProfessionalism = medicalStaffProfessionalism;
            GettingAdviceByMedicalStaff = gettingAdviceByMedicalStaff;
            EaseInObtainingFollowupInformationAndCare = easeInObtainingFollowupInformationAndCare;
            Nursing = nursing;
            Cleanliness = cleanliness;
            OverallRating = overallRating;
            SatisfiedWithDrugAndInstrument = satisfiedWithDrugAndInstrument;
            DoctorJmbg = doctorJmbg;
        }
    }
}
