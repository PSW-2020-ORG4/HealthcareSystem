using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackAndSurveyService.SurveyService.Model
{
    public class MedicalStaffSurveyResponse
    {
        private Grade Behavior { get; }
        private Grade Professionalism { get; }
        private Grade GivingAdvice { get; }
        private Grade FollowUp { get; }
    }
}
