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
            if (feedback == null)
                throw new BadRequestException("Please, write a comment to send feedback.");
            _feedbackRepository.AddFeedback(feedback);
        }
        public void PublishFeedback(Feedback feedback)
        {
            if (!feedback.IsAllowedToPublish)
                throw new BadRequestException("Feedback is not allowed to publish.");
            _feedbackRepository.PublishFeedback(feedback);
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
            return _feedbackRepository.GetFeedbackById(id);
        }
    }
}