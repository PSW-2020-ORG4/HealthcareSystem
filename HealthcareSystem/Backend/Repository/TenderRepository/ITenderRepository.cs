using System.Collections.Generic;
using Backend.Model.DTO;
using Backend.Model.Pharmacies;

namespace Backend.Repository.TenderRepository
{
    public interface ITenderRepository
    {
        IEnumerable<Tender> GetAllTenders();
        Tender GetTenderById(int id);
        IEnumerable<Tender> GetAllNotClosedTenders();
        List<TenderDrugDTO> GetDrugsForTender(int id);
        Tender GetTenderByRoutingKey(string routingKey);
        List<TenderMessageDTO> GetMessagesForTender(int id);
        List<TenderOfferDTO> GetOffersForMessage(int id);
        void DeleteMessage(int id);
        void CreateTender(Tender tender);
        void UpdateTender(Tender tender);
        void DeclineMessage(int id);
        Tender GetTenderByMessageId(int id);
    }
}
