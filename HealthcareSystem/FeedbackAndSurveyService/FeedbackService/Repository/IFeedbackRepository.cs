using FeedbackAndSurveyService.FeedbackService.Model;
using System.Collections.Generic;

namespace FeedbackAndSurveyService.FeedbackService.Repository
{
    public interface IFeedbackRepository
    {
        IEnumerable<Feedback> GetUnpublished();
        IEnumerable<Feedback> GetPublished();
        Feedback Get(int id);
        void Add(Feedback entity);
        void Update(Feedback entity);
    }
}
