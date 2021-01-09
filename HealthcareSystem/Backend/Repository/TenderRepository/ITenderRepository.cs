using System;
using System.Collections.Generic;
using System.Text;
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
    }
}
