using Backend.Model.Pharmacies;
using System.Collections.Generic;

namespace Backend.Service
{
    public interface ITenderMessageService
    {
        TenderMessage GetById(int id);
        TenderMessage GetAcceptedByTenderId(int id);
        List<TenderMessage> GetAll();
        List<TenderMessage> GetAllByTender(int id);
        void CreateTenderMessage(TenderMessage tm);
        void UpdateTenderMessage(TenderMessage tm);
    }
}
