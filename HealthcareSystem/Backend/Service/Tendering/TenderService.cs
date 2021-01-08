using System;
using System.Collections.Generic;
using System.Text;
using Backend.Model.DTO;
using Backend.Model.Pharmacies;
using Backend.Repository.TenderRepository;

namespace Backend.Service.Tendering
{
    public class TenderService : ITenderService
    {
        private readonly ITenderRepository _tenderRepository;

        public TenderService(ITenderRepository tenderRepository)
        {
            _tenderRepository = tenderRepository;
        }
        public IEnumerable<Tender> GetAllTenders()
        {
            return _tenderRepository.GetAllTenders();
        }

        public Tender GetTenderById(int id)
        {
            return _tenderRepository.GetTenderById(id);
        }

        public List<TenderDrugDTO> GetDrugsForTender(int id)
        {
            return _tenderRepository.GetDrugsForTender(id);
        }
    }
}
