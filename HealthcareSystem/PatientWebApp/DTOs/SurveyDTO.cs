using System;

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
        public int EaseInObtainingFollowUpInformation { get; set; }
        public int Nursing { get; set; }
        public int Cleanliness { get; set; }
        public int OverallRating { get; set; }
        public int SatisfiedWithDrugAndInstrument { get; set; }
        public int ExaminationId { get; set; }


        public SurveyDTO() { }

        public SurveyDTO(int id, int behaviorOfDoctor, int doctorProfessionalism, int gettingAdviceByDoctor, int availabilityOfDoctor, 
            int behaviorOfMedicalStaff, int medicalStaffProfessionalism, int gettingAdviceByMedicalStaff, int easeInObtainingFollowUpInformation, 
            int nursing, int cleanliness, int overallRating, int satisfiedWithDrugAndInstrument, int examinationId)
        {
            Id = id;
            BehaviorOfDoctor = behaviorOfDoctor;
            DoctorProfessionalism = doctorProfessionalism;
            GettingAdviceByDoctor = gettingAdviceByDoctor;
            AvailabilityOfDoctor = availabilityOfDoctor;
            BehaviorOfMedicalStaff = behaviorOfMedicalStaff;
            MedicalStaffProfessionalism = medicalStaffProfessionalism;
            GettingAdviceByMedicalStaff = gettingAdviceByMedicalStaff;
            EaseInObtainingFollowUpInformation = easeInObtainingFollowUpInformation;
            Nursing = nursing;
            Cleanliness = cleanliness;
            OverallRating = overallRating;
            SatisfiedWithDrugAndInstrument = satisfiedWithDrugAndInstrument;
            ExaminationId = examinationId;
        }
    }
}
