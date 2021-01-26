/***********************************************************************
 * Module:  Notification.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Model.Users.Notification
 ***********************************************************************/

using Model.Users;

namespace Model.NotificationSurveyAndFeedback
{
    public class Notification
    {
        public int Id { get; set; }
        public TypeOfNotification Type { get; set; }
        public string Message { get; set; }
        public string JmbgOfReceiver { get; set; }

        public Notification() { }
        public Notification(int id, TypeOfNotification type, string message, string jmbg)
        {
            this.Id = id;
            this.Type = type;
            this.Message = message;
            this.JmbgOfReceiver = jmbg;
        }
    }
}