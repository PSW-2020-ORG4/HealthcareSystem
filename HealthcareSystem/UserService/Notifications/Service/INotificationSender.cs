using System;

namespace UserService.Notifications
{
    interface INotificationSender
    {
        void SendNotification(String notification, String recipientEmail);
    }
}
