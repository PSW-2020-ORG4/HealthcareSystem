using Backend.Model.Pharmacies;

namespace IntegrationAdaptersTenderService.Service.RabbitMqTenderingService
{
    public interface IRabbitMqTenderingService
    {
        public void NotifyParticipants(int tenderId);
        public void AddQueue(Tender tender);
        public void RemoveQueue(Tender tender);
    }
}
