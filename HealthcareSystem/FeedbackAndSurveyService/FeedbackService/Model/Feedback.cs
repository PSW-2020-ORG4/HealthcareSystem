using FeedbackAndSurveyService.CustomException;
using FeedbackAndSurveyService.FeedbackService.Model.Memento;

namespace FeedbackAndSurveyService.FeedbackService.Model
{
    public class Feedback : IOriginator<FeedbackMemento>
    {
        private int Id { get; }
        private Content Content { get; }
        private Commentator Commentator { get; }
        private bool IsPublished { get; set; }
        private bool IsAllowedToPublish { get; }

        public Feedback(FeedbackMemento memento)
        {
            Id = memento.Id;
            Content = new Content(memento.DateOfCreation, memento.Comment);
            IsAllowedToPublish = memento.IsAllowedToPublish;
            IsPublished = memento.IsPublished;
            Commentator = memento.Commentator;
            Validate();
        }

        public FeedbackMemento GetMemento()
        {
            return new FeedbackMemento()
            {
                Id = Id,
                DateOfCreation = Content.DateOfCreation,
                Comment = Content.Comment,
                IsAllowedToPublish = IsAllowedToPublish,
                IsPublished = IsPublished,
                Commentator = Commentator
            };
        }

        public void Publish()
        {
            if (!IsAllowedToPublish)
                throw new ValidationException("Feedback isn't allowed to be published.");
            if (IsPublished)
                throw new ValidationException("Feedback is already published.");
            IsPublished = true;
        }

        private void Validate()
        {
            if (IsPublished && !IsAllowedToPublish)
                throw new ValidationException("Feedback state is not valid.");
        }
    }
}
