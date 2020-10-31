/***********************************************************************
 * Module:  FeedbackService.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Service.Notification&Survey&dFeedbackService.FeedbackService
 ***********************************************************************/

using Model.Users;
using Repository;
using System;
using System.Collections.Generic;

namespace Service.NotificationSurveyAndFeedback
{
   public class FeedbackService
   {
        private FeedbackRepository feedbackRepository = new FeedbackRepository();
        public void NewFeedback(Feedback feedback)
        {
            feedbackRepository.NewFeedback(feedback);
        }
        public void PublishFeedback(int id)
        {
            feedbackRepository.PublishFeedback(id);
        }
        public List<Feedback> GetPublishedFeedbacks()
        {
            return feedbackRepository.GetPublishedFeedbacks();
        }
        public List<Feedback> GetUnpublishedFeedbacks()
        {
            return feedbackRepository.GetUnpublishedFeedbacks();
        }

    }
}