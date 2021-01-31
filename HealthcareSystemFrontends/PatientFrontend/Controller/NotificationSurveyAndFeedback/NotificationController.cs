/***********************************************************************
 * Module:  NotificationController.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Controller.NotificationController
 ***********************************************************************/

using Service.NotificationSurveyAnddFeedback;
using System;
using System.Collections.Generic;

namespace Controller.NotificationSurveyAndFeedback
{
   public class NotificationController
   {

      public NotificationService notificationService = new NotificationService();
      public void SendNotification(Model.Users.Notification notification)
      {
            notificationService.SendNotification(notification);
      }
      
      public List<Model.Users.Notification> ViewNotificationByJmbg(string jmbg)
      {
            return notificationService.ViewNotificationByJmbg(jmbg); 
      }
      
      public void DeleteNotification(int id)
      {
            notificationService.DeleteNotification(id);
      }
   
      public int getLastId()
        {
            return notificationService.getLastId();
        }
   
   }
}