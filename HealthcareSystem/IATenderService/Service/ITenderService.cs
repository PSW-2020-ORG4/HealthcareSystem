using Backend.Model.DTO;
using Backend.Model.Pharmacies;
using System.Collections.Generic;

namespace IntegrationAdaptersTenderService.Service
{
    public interface ITenderService
    {
        IEnumerable<Tender> GetAllTenders();
        IEnumerable<Tender> GetAllNotClosedTenders();
        Tender GetTenderById(int id);
        List<TenderDrugDTO> GetDrugsForTender(int id);
        Tender GetTenderByRoutingKey(string routingKey);
        void CreateTender(Tender tender);
        void UpdateTender(Tender tender);
        Tender GetTenderByMessageId(int id);
    }
}
