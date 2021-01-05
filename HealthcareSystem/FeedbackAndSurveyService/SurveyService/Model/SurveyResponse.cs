using FeedbackAndSurveyService.SurveyService.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackAndSurveyService.SurveyService.Model
{
    public class SurveyResponse
    {
        private SurveyPermission UsedPermission;
        private MedicalStaffSurveyResponse MedicalStaffSurveyResponse;
        private DoctorSurveyResponse DoctorSurveyResponse;
        private HospitalSurveyResponse HospitalSurveyResponse;    
        
        public SurveyResponse(SurveyPermission permission, SurveyResponseDTO reponse)
        {

        }
    }
}
