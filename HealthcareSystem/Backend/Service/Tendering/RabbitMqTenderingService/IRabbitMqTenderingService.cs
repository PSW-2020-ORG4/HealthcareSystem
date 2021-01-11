using Backend.Model.Pharmacies;

namespace Backend.Service
{
    public interface IRabbitMqTenderingService
    {
        public void NotifyParticipants(int tenderId);
        public void AddQueue(Tender tender);
        public void RemoveQueue(Tender tender);
    }
}
