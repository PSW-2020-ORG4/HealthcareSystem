using Backend.Model.Pharmacies;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Repository
{
    public interface ITenderMessageRepository
    {
        IEnumerable<TenderMessage> GetAll();
        IEnumerable<TenderMessage> GetAllByTender(int id);
        void CreateTenderMessage(TenderMessage tm);
    }
}
