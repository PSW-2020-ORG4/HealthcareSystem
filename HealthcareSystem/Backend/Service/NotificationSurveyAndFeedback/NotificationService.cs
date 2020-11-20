/***********************************************************************
 * Module:  NotificationService.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Service.NotificationService
 ***********************************************************************/

using Model.NotificationSurveyAndFeedback;
using Model.Users;
using Repository;
using System;
using System.Collections.Generic;

namespace Service.NotificationSurveyAnddFeedback
{
   public class NotificationService
   {
        private NotificationRepository notificationRepository = new NotificationRepository();
      public void SendNotification(Notification notification)
      {
            notificationRepository.NewNotification(notification);
      }
      
      public List<Notification> ViewNotificationByJmbg(string jmbg)
      {
            return notificationRepository.GetNotificationsByJmbg(jmbg);
      }
      
      public void DeleteNotification(int id)
      {
            notificationRepository.DeleteNotification(id);
      }

        public int getLastId()
        {
            return notificationRepository.getLastId();
        }
    }
}