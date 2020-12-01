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
      public Model.Users.Feedback AddFeedback(Feedback feedback)
      {
            return feedbackService.AddFeedback(feedback);
      }
      
      public List<Model.Users.Feedback> ViewFeedbackInformations()
      {
            return feedbackService.ViewFeedbackInformations();
      }
   
   
   }
}