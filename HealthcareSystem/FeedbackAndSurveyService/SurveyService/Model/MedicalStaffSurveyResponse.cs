using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackAndSurveyService.SurveyService.Model
{
    public class MedicalStaffSurveyResponse
    {
        public Grade Behavior { get; }
        public Grade Professionalism { get; }
        public Grade GivingAdvice { get; }
        public Grade FollowUp { get; }

        public MedicalStaffSurveyResponse(int behavior, int professionalism, int givingAdvice, int followUp)
        {
            Behavior = new Grade(behavior);
            Professionalism = new Grade(professionalism);
            GivingAdvice = new Grade(givingAdvice);
            FollowUp = new Grade(followUp);
        }
    }
}
