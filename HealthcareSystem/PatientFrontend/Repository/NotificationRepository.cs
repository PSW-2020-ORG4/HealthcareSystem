/***********************************************************************
 * Module:  NotificationRepository.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Repository.NotificationRepository
 ***********************************************************************/

using Model.Users;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Repository
{
   public class NotificationRepository
   {

        private string path;

        public NotificationRepository()
        {
            string fileName = "notification.json";
            path = Path.GetFullPath(fileName);
        }

        public int getLastId()
        {
            List<Notification> notifications = ReadFromFile();
            if(notifications.Count == 0)
            {
                return 0;
            }
            return notifications[notifications.Count - 1].Id;
        }
        public void DeleteNotification(int id)
        {
            List<Notification> notifications = ReadFromFile();
            Notification notificationForDelete = null;
            foreach(Notification n in notifications)
            {
                if (n.Id == id)
                {
                    notificationForDelete = n;
                    break;
                }
            }
            notifications.Remove(notificationForDelete);
            WriteInFile(notifications);
        }
      
      public List<Notification> GetNotificationsByJmbg(string jmbg)
      {
            List<Notification> result = new List<Notification>();
            List<Notification> notifications = ReadFromFile();
            foreach(Notification n in notifications)
            {
                if (n.JmbgOfReceiver.Equals(jmbg))
                {
                    result.Add(n);
                }
            }
            return result;
      }
      
      public Model.Users.Notification NewNotification(Model.Users.Notification notification)
      {
            List<Notification> notifications = ReadFromFile();
            notifications.Add(notification);
            WriteInFile(notifications);
            return notification;
      }
  
        private List<Notification> ReadFromFile()
        {
            List<Notification> notifications;
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                notifications = JsonConvert.DeserializeObject<List<Notification>>(json);
            }
            else
            {
                notifications = new List<Notification>();
                WriteInFile(notifications);
            }
            return notifications;
        }

        private void WriteInFile(List<Notification> notifications)
        {
            string json = JsonConvert.SerializeObject(notifications);
            File.WriteAllText(path, json);
        }

   
   }
}