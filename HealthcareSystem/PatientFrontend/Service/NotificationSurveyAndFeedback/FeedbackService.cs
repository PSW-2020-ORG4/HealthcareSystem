/***********************************************************************
 * Module:  FeedbackService.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Service.Notification&Survey&dFeedbackService.FeedbackService
 ***********************************************************************/

using Repository;
using System;
using System.Collections.Generic;

namespace Service.NotificationSurveyAndFeedback
{
   public class FeedbackService
   {
        public FeedbackRepository feedbackRepository = new FeedbackRepository();
      public Model.Users.Feedback AddFeedback(Model.Users.Feedback feedback)
      {
            return feedbackRepository.NewFeedback(feedback);
      }
      
      public List<Model.Users.Feedback> ViewFeedbackInformations()
      {
            return feedbackRepository.GetAllFeedbacks();
      }
   
   }
}