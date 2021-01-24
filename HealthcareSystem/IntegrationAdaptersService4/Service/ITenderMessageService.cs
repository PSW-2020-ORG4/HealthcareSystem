using System.Collections.Generic;
using Backend.Model.Pharmacies;

namespace IntegrationAdaptersService4.Service
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
