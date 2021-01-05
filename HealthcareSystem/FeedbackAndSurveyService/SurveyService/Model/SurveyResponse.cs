﻿using FeedbackAndSurveyService.SurveyService.DTO;
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
