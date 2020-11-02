/***********************************************************************
 * Module:  FeedbackController.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Controller.Notification&Survey&FeedbackController.FeedbackController
 ***********************************************************************/

using Backend.Repository;
using Model.Users;
using Repository;
using Service.NotificationSurveyAndFeedback;
using System;
using System.Collections.Generic;

namespace Controller.NotificationSurveyAndFeedback
{
   public class FeedbackController
   {
        private FeedbackService _feedbackService = new FeedbackService(new FileFeedbackRepository(),new FileActivePatientRepository());
        public void AddFeedback(Feedback feedback)
        {
            _feedbackService.AddFeedback(feedback);
        }
        public void PublishFeedback(int id)
        {
            _feedbackService.PublishFeedback(id);
        }
        public List<Feedback> GetPublishedFeedbacks()
        {
            return _feedbackService.GetPublishedFeedbacks();
        }
        public List<Feedback> GetUnpublishedFeedbacks()
        {
            return _feedbackService.GetUnpublishedFeedbacks();
        }


    }
}