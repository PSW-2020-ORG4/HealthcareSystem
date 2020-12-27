namespace UserService.Notifications
{
    public interface INotificationService
    {
        void SendActivationRequest(ActivationRequest activationRequest);
    }
}
