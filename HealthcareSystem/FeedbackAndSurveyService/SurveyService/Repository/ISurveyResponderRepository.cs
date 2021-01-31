using FeedbackAndSurveyService.SurveyService.Model;

namespace FeedbackAndSurveyService.SurveyService.Repository
{
    public interface ISurveyResponderRepository
    {
        SurveyResponder Get(string id);
        void Update(SurveyResponder entity);
    }
}
