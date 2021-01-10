using System.Collections.Generic;
using Backend.Model.DTO;
using Backend.Model.Pharmacies;

namespace Backend.Service.Tendering
{
    public interface ITenderService
    {
        IEnumerable<Tender> GetAllTenders();
        IEnumerable<Tender> GetAllNotClosedTenders();
        Tender GetTenderById(int id);
        List<TenderDrugDTO> GetDrugsForTender(int id);
        Tender GetTenderByRoutingKey(string routingKey);
        List<TenderMessageDTO> GetMessagesForTender(int id);
        List<TenderOfferDTO> GetOffersForMessage(int id);
        void DeleteMessage(int id);
        void UpdateTender(Tender tender);
        void DeclineMessage(int id);
        Tender GetTenderByMessageId(int id);
    }
}
