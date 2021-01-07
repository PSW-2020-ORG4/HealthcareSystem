using FeedbackAndSurveyService.FeedbackService.DTO;
using FeedbackAndSurveyService.FeedbackService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
