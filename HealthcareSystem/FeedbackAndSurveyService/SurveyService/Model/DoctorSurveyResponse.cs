namespace FeedbackAndSurveyService.SurveyService.Model
{
    public class DoctorSurveyResponse
    {
        public Grade Behavior { get; }
        public Grade Professionalism { get; }
        public Grade GivingAdvice { get; }
        public Grade Availability { get; }

        public DoctorSurveyResponse(int behavior, int professionalism, int givingAdvice, int availability)
        {
            Behavior = new Grade(behavior);
            Professionalism = new Grade(professionalism);
            GivingAdvice = new Grade(givingAdvice);
            Availability = new Grade(availability);
        }
    }
}
