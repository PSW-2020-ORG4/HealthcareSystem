using Backend.Model.Pharmacies;
using Backend.Repository;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Service
{
    public class TenderMessageService : ITenderMessageService
    {
        private readonly ITenderMessageRepository _tenderMessageRepository;

        public TenderMessageService(ITenderMessageRepository tenderMessageRepository)
        {
            _tenderMessageRepository = tenderMessageRepository;
        }
        public void CreateTenderMessage(TenderMessage tm)
        {
            _tenderMessageRepository.CreateTenderMessage(tm);
        }

        public TenderMessage GetAcceptedByTenderId(int id)
        {
            return _tenderMessageRepository.GetAcceptedByTenderId(id);
        }

        public List<TenderMessage> GetAllByTender(int id)
        {
            return _tenderMessageRepository.GetAllByTender(id).ToList();
        }

        public TenderMessage GetById(int id)
        {
            return _tenderMessageRepository.GetById(id);
        }

        public void UpdateTenderMessage(TenderMessage tm)
        {
            _tenderMessageRepository.UpdateTenderMessage(tm);
        }
    }
}
