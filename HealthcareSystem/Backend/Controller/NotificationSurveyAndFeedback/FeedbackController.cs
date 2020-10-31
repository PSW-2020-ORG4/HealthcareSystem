/***********************************************************************
 * Module:  FeedbackController.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Controller.Notification&Survey&FeedbackController.FeedbackController
 ***********************************************************************/

using Model.Users;
using Service.NotificationSurveyAndFeedback;
using System;
using System.Collections.Generic;

namespace Controller.NotificationSurveyAndFeedback
{
   public class FeedbackController
   {

        private FeedbackService feedbackService = new FeedbackService();
        public void NewFeedback(Feedback feedback)
        {
            feedbackService.NewFeedback(feedback);
        }
        public void PublishFeedback(int id)
        {
            feedbackService.PublishFeedback(id);
        }
        public List<Feedback> GetPublishedFeedbacks()
        {
            return feedbackService.GetPublishedFeedbacks();
        }
        public List<Feedback> GetUnpublishedFeedbacks()
        {
            return feedbackService.GetUnpublishedFeedbacks();
        }


    }
}