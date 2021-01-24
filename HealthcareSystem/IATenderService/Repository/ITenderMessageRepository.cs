using System.Collections.Generic;
using Backend.Model.Pharmacies;

namespace IntegrationAdaptersTenderService.Repository
{
    public interface ITenderMessageRepository
    {
        TenderMessage GetById(int id);
        IEnumerable<TenderMessage> GetAll();
        IEnumerable<TenderMessage> GetAllByTender(int id);
        void CreateTenderMessage(TenderMessage tm);
        void UpdateTenderMessage(TenderMessage tm);
        TenderMessage GetAcceptedByTenderId(int id);
    }
}
