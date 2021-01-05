using FeedbackAndSurveyService.FeedbackService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackAndSurveyService.FeedbackService.Repository
{
    public interface IFeedbackRepository
    {
        IEnumerable<Feedback> GetUnpublished();
        IEnumerable<Feedback> GetPublished();
        Feedback Get(int id);
    }
}
