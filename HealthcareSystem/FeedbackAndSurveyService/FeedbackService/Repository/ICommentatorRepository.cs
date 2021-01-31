using FeedbackAndSurveyService.FeedbackService.Model;

namespace FeedbackAndSurveyService.FeedbackService.Repository
{
    public interface ICommentatorRepository
    {
        Commentator Get(string id);
    }
}
