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

        public MedicalStaffSurveyResponse(int behavior, int professionalism, int givingAdvice, int followUp)
        {
            Behavior = new Grade(behavior);
            Professionalism = new Grade(professionalism);
            GivingAdvice = new Grade(givingAdvice);
            FollowUp = new Grade(followUp);
        }
    }
}
