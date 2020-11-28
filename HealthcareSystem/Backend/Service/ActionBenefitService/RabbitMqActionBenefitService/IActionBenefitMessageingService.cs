namespace Backend.Service
{
    public interface IActionBenefitMessageingService
    {
        void Subscribe(string exchangeName);
        void Unsubscribe(string exchangeName);
    }
}