using FeedbackAndSurveyService.SurveyService.DTO;

namespace FeedbackAndSurveyService.SurveyService.Model
{
    public class SurveyResponse
    {
        public SurveyPermission UsedPermission;
        public MedicalStaffSurveyResponse MedicalStaffSurveyResponse;
        public DoctorSurveyResponse DoctorSurveyResponse;
        public HospitalSurveyResponse HospitalSurveyResponse;

        public SurveyResponse(SurveyPermission permission, SurveyResponseDTO response)
        {
            UsedPermission = permission;
            MedicalStaffSurveyResponse = new MedicalStaffSurveyResponse(response.BehaviorOfMedicalStaff,
                                                                        response.MedicalStaffProfessionalism,
                                                                        response.GettingAdviceByMedicalStaff,
                                                                        response.EaseInObtainingFollowUpInformation);
            DoctorSurveyResponse = new DoctorSurveyResponse(response.BehaviorOfDoctor,
                                                            response.DoctorProfessionalism,
                                                            response.GettingAdviceByDoctor,
                                                            response.AvailabilityOfDoctor);
            HospitalSurveyResponse = new HospitalSurveyResponse(response.Nursing,
                                                                response.Cleanliness,
                                                                response.OverallRating,
                                                                response.SatisfiedWithDrugAndInstrument);
        }
    }
}
