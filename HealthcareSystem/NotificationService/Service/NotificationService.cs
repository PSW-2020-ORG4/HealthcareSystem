namespace NotificationService
{
    class NotificationService : INotificationService
    {
        private readonly ITemplateRepository _templateRepository;
        private readonly INotificationSender _notificationSender;

        public NotificationService(ITemplateRepository templateRepository, INotificationSender notificationSender)
        {
            _templateRepository = templateRepository;
            _notificationSender = notificationSender;
        }

        public void SendActivationRequest(ActivationRequest activationRequest)
        {
            Template activationTemplate = _templateRepository.Get("ActivationTemplate.html");
            string notification = activationTemplate.Apply(activationRequest.ToDictionary());
            _notificationSender.SendNotification(notification, activationRequest.Email.Value);
        }
    }
}
