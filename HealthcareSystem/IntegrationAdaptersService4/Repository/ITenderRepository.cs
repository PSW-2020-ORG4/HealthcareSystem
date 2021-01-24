using System.Collections.Generic;
using Backend.Model.DTO;
using Backend.Model.Pharmacies;

namespace IntegrationAdaptersService4.Repository
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
        void CreateTender(Tender tender);
        void UpdateTender(Tender tender);
        Tender GetTenderByMessageId(int id);
    }
}
