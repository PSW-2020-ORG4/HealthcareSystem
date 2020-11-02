/***********************************************************************
 * Module:  FeedbackService.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Service.Notification&Survey&dFeedbackService.FeedbackService
 ***********************************************************************/

using Backend.Model.Exceptions;
using Backend.Repository;
using Model.Users;
using Repository;
using System;
using System.Collections.Generic;

namespace Service.NotificationSurveyAndFeedback
{
   public class FeedbackService
   {
        private IFeedbackRepository _feedbackRepository;
        private IActivePatientRepository _activePatientRepository;

        public FeedbackService(IFeedbackRepository feedbackRepository, IActivePatientRepository activePatientRepository)
        {
            _feedbackRepository = feedbackRepository;
            _activePatientRepository = activePatientRepository;
        }
        public void AddFeedback(Feedback feedback)
        {
            if (feedback == null)
                throw new BadRequestException("Please, write a comment to send feedback.");
            if (_activePatientRepository.GetPatientByJmbg(feedback.Commentator.Jmbg) == null)
                throw new BadRequestException("An error occurred while loading your data. There is a possibility that your profile has been deleted.");
            _feedbackRepository.AddFeedback(feedback);
        }
        public void PublishFeedback(int id)
        {
            _feedbackRepository.PublishFeedback(id);
        }
        public List<Feedback> GetPublishedFeedbacks()
        {
            return _feedbackRepository.GetPublishedFeedbacks();
        }
        public List<Feedback> GetUnpublishedFeedbacks()
        {
            return _feedbackRepository.GetUnpublishedFeedbacks();
        }

    }
}