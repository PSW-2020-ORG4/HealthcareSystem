using Backend.Model.Pharmacies;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Repository
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
