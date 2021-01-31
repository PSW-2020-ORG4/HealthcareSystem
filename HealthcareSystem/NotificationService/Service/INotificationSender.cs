using System;

namespace NotificationService
{
    interface INotificationSender
    {
        void SendNotification(String notification, String recipientEmail);
    }
}
