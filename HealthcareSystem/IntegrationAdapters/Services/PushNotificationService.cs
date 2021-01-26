using IntegrationAdapters.Dtos;
using Newtonsoft.Json;
using System;
using WebPush;

namespace IntegrationAdapters.Services
{
    public class PushNotificationService : IPushNotificationService
    {
        private readonly VapidDetails _vapidDetails;
        private readonly WebPushClient _webPushClient = new WebPushClient();

        public PushNotificationService()
        {
            string vapidPublicKey = Startup.Configuration.GetSection("VapidKeys")["PublicKey"];
            string vapidPrivateKey = Startup.Configuration.GetSection("VapidKeys")["PrivateKey"];
            _vapidDetails = new VapidDetails("mailto:example@example.com", vapidPublicKey, vapidPrivateKey);
        }

        public void SendNotification(PushSubscription pushSubscription, PushPayload pushPayload)
        {
            if (!PushSubscriptionValid(pushSubscription) || pushPayload == null)
                return;

            string payload = JsonConvert.SerializeObject(pushPayload);
            try
            {
                _webPushClient.SendNotification(pushSubscription, payload, _vapidDetails);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private bool PushSubscriptionValid(PushSubscription pushSubscription)
        {
            if (pushSubscription.Auth == null || pushSubscription.Auth == "" ||
               pushSubscription.Endpoint == null || pushSubscription.Endpoint == "" ||
               pushSubscription.P256DH == null || pushSubscription.P256DH == "")
                return false;

            return true;
        }
    }

}