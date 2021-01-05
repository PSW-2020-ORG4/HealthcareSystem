using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackAndSurveyService.FeedbackService.Model
{
    public class Feedback
    {
        private int Id { get; }
        private Content Content { get; }
        private Commentator Commentator { get; }
        private bool IsPublished { get; }
        private bool IsAllowedToPublish { get; }

        public void Publish()
        {
            throw new NotImplementedException();
        }

        private void Validate()
        {

        }
    }
}
