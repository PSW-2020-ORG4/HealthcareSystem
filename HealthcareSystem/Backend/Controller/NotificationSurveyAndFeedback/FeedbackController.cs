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
        private FeedbackService _feedbackService = new FeedbackService(new MySqlFeedbackRepository(),new MySqlActivePatientRepository());
        public void AddFeedback(Feedback feedback)
        {
            _feedbackService.AddFeedback(feedback);
        }
        public void PublishFeedback(Feedback feedback)
        {
            _feedbackService.PublishFeedback(feedback);
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