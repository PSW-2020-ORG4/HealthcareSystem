using WebPush;
using IntegrationAdapters.Dtos;

namespace IntegrationAdapters.Services
{
    public interface IPushNotificationService
    {
        public void SendNotification(PushSubscription pushSubscription, PushPayload pushPayload);
    }
}