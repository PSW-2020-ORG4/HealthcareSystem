using Backend.Model.Pharmacies;
using System.Collections.Generic;

namespace IntegrationAdaptersTenderService.Service
{
    public interface ITenderMessageService
    {
        TenderMessage GetById(int id);
        TenderMessage GetAcceptedByTenderId(int id);
        List<TenderMessage> GetAllByTender(int id);
        void CreateTenderMessage(TenderMessage tm);
        void UpdateTenderMessage(TenderMessage tm);
    }
}
