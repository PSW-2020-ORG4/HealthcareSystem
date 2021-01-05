using FeedbackAndSurveyService.FeedbackService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackAndSurveyService.FeedbackService.Repository
{
    public interface ICommentatorRepository
    {
        Commentator Get(string id);
    }
}
