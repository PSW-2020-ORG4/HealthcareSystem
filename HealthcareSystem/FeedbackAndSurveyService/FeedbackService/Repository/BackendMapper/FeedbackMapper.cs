using FeedbackAndSurveyService.FeedbackService.Model.Memento;
using Model.NotificationSurveyAndFeedback;
using System;

namespace FeedbackAndSurveyService.FeedbackService.Repository
{
    static class FeedbackMapper
    {
        internal static Model.Feedback ToFeedback(this Feedback feedback)
        {
            FeedbackMemento memento = new FeedbackMemento()
            {
                Id = feedback.Id,
                DateOfCreation = feedback.SendingDate,
                Comment = feedback.Comment,
                IsAllowedToPublish = feedback.IsAllowedToPublish,
                IsPublished = feedback.IsPublished
            };
            if (!String.IsNullOrWhiteSpace(feedback.CommentatorJmbg))
            {
                memento.Commentator = new Model.Commentator(feedback.CommentatorJmbg,
                                                            feedback.Commentator.Name,
                                                            feedback.Commentator.Surname);
            }
            return new Model.Feedback(memento);
        }

        internal static Feedback ToBackendFeedback(this Model.Feedback feedback)
        {
            FeedbackMemento memento = feedback.GetMemento();
            Feedback mapped = new Feedback()
            {
                Id = memento.Id,
                Comment = memento.Comment,
                SendingDate = memento.DateOfCreation,
                IsAllowedToPublish = memento.IsAllowedToPublish,
                IsPublished = memento.IsPublished
            };
            if (memento.Commentator != null)
                mapped.CommentatorJmbg = memento.Commentator.Jmbg.Value;
            return mapped;
        }
    }
}
