/***********************************************************************
 * Module:  FeedbackService.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Service.Notification&Survey&dFeedbackService.FeedbackService
 ***********************************************************************/

using Backend.Model.Exceptions;
using Backend.Repository;
using Backend.Service.NotificationSurveyAndFeedback;
using Model.Users;
using Repository;
using System;
using System.Collections.Generic;

namespace Service.NotificationSurveyAndFeedback
{
   public class FeedbackService : IFeedbackService
   {
        private IFeedbackRepository _feedbackRepository;
        public FeedbackService(IFeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }
        public void AddFeedback(Feedback feedback)
        {
            _feedbackRepository.AddFeedback(feedback);
        }
        public void PublishFeedback(int id)
        {
            Feedback _feedback = GetFeedbackById(id);
            _feedback.IsPublished = true;
            _feedbackRepository.UpdateFeedback(_feedback);
        }
        public List<Feedback> GetPublishedFeedbacks()
        {
            return _feedbackRepository.GetPublishedFeedbacks();
        }
        public List<Feedback> GetUnpublishedFeedbacks()
        {
            return _feedbackRepository.GetUnpublishedFeedbacks();
        }
        public Feedback GetFeedbackById(int id)
        {
            Feedback _feedback = _feedbackRepository.GetFeedbackById(id);
            if (_feedback == null)
                throw new NotFoundException("Feeback with id=" + id + "doesn't exist in database.");
            return _feedback;
        }
    }
}