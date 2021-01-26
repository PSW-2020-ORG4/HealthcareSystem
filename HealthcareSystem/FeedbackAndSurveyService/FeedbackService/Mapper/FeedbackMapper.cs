using FeedbackAndSurveyService.FeedbackService.DTO;
using FeedbackAndSurveyService.FeedbackService.Model;

namespace FeedbackAndSurveyService.FeedbackService.Mapper
{
    public static class FeedbackMapper
    {
        public static FeedbackDTO ToFeedbackDTO(this Feedback feedback)
        {
            var memento = feedback.GetMemento();
            FeedbackDTO mapped = new FeedbackDTO()
            {
                Id = memento.Id,
                Comment = memento.Comment,
                SendingDate = memento.DateOfCreation,
                IsAllowedToPublish = memento.IsAllowedToPublish
            };
            if (memento.Commentator is null)
                mapped.IsAnonymous = true;
            else
            {
                mapped.CommentatorJmbg = memento.Commentator.Jmbg.Value;
                mapped.CommentatorName = memento.Commentator.Name;
                mapped.CommentatorSurname = memento.Commentator.Surname;
            }
            return mapped;
        }
    }
}
