using System;

namespace NotificationService
{
    public class NotificationServiceException : Exception
    {
        public NotificationServiceException() : base() { }

        public NotificationServiceException(string message) : base(message) { }
    }
}
