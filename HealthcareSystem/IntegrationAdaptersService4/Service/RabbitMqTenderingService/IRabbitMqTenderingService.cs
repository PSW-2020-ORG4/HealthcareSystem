using Backend.Model.Pharmacies;

namespace IntegrationAdaptersService4.Service.RabbitMqTenderingService
{
    public interface IRabbitMqTenderingService
    {
        public void NotifyParticipants(int tenderId);
        public void AddQueue(Tender tender);
        public void RemoveQueue(Tender tender);
    }
}
