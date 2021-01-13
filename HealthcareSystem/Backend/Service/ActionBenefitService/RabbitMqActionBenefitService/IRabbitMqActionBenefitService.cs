namespace Backend.Service
{
    public interface IRabbitMqActionBenefitService
    {
        void Subscribe(string exchangeName);
        void Unsubscribe(string exchangeName);
        void SubscriptionEdit(string exOld, bool subOld, string exNew, bool subNew);
    }
}