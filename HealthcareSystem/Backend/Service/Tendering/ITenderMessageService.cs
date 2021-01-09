using Backend.Model.Pharmacies;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Service
{
    public interface ITenderMessageService
    {
        List<TenderMessage> GetAll();
        List<TenderMessage> GetAllByTender(int id);
        void CreateTenderMessage(TenderMessage tm);
    }
}
