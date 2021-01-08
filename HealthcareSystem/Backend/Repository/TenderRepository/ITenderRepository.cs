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
        List<TenderDrugDTO> GetDrugsForTender(int id);
    }
}
