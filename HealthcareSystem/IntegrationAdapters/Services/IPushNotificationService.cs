using IntegrationAdapters.Dtos;
using WebPush;

namespace IntegrationAdapters.Services
{
    public interface IPushNotificationService
    {
        public void SendNotification(PushSubscription pushSubscription, PushPayload pushPayload);
    }
}