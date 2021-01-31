using FeedbackAndSurveyService.FeedbackService.DTO;
using FeedbackAndSurveyService.FeedbackService.Model;
using System.Collections.Generic;

namespace FeedbackAndSurveyService.FeedbackService.Service
{
    public interface IFeedbackService
    {
        IEnumerable<Feedback> GetPublished();
        IEnumerable<Feedback> GetUnpublished();
        void Add(AddFeedbackDTO feedback);
        void Publish(int id);
    }
}
