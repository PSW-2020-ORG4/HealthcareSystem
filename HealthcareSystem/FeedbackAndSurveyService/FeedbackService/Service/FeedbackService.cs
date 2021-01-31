using FeedbackAndSurveyService.CustomException;
using FeedbackAndSurveyService.FeedbackService.DTO;
using FeedbackAndSurveyService.FeedbackService.Model;
using FeedbackAndSurveyService.FeedbackService.Model.Memento;
using FeedbackAndSurveyService.FeedbackService.Repository;
using System;
using System.Collections.Generic;

namespace FeedbackAndSurveyService.FeedbackService.Service
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly ICommentatorRepository _commentatorRepository;

        public FeedbackService(IFeedbackRepository feedbackRepository, ICommentatorRepository commentatorRepository)
        {
            _feedbackRepository = feedbackRepository;
            _commentatorRepository = commentatorRepository;
        }

        public IEnumerable<Feedback> GetPublished()
        {
            return _feedbackRepository.GetPublished();
        }

        public IEnumerable<Feedback> GetUnpublished()
        {
            return _feedbackRepository.GetUnpublished();
        }

        public void Publish(int id)
        {
            Feedback feedback = _feedbackRepository.Get(id);
            feedback.Publish();
            _feedbackRepository.Update(feedback);
        }

        public void Add(AddFeedbackDTO feedback)
        {
            FeedbackMemento memento = new FeedbackMemento()
            {
                Comment = feedback.Comment,
                IsAllowedToPublish = feedback.IsAllowedToPublish,
                IsPublished = false,
                DateOfCreation = DateTime.Now,
                Commentator = GetCommentator(feedback.IsAnonymous, feedback.CommentatorJmbg)
            };
            _feedbackRepository.Add(new Feedback(memento));
        }

        private Commentator GetCommentator(bool isAnonymous, string jmbg)
        {
            if (isAnonymous || String.IsNullOrWhiteSpace(jmbg))
                return null;
            try
            {
                return _commentatorRepository.Get(jmbg);
            }
            catch (NotFoundException e)
            {
                throw new ValidationException(e.Message);
            }
        }
    }
}
