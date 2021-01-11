using System;
using System.Collections.Generic;
using Backend.Model.DTO;
using Backend.Model.Pharmacies;
using Backend.Repository.TenderRepository;

namespace Backend.Service
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

        public IEnumerable<Tender> GetAllNotClosedTenders()
        {
            return _tenderRepository.GetAllNotClosedTenders();
        }

        public Tender GetTenderByRoutingKey(string routingKey)
        {
            return _tenderRepository.GetTenderByRoutingKey(routingKey);
        }

        public List<TenderMessageDTO> GetMessagesForTender(int id)
        {
            return _tenderRepository.GetMessagesForTender(id);
        }

        public List<TenderOfferDTO> GetOffersForMessage(int id)
        {
            return _tenderRepository.GetOffersForMessage(id);
        }

        public void DeleteMessage(int id)
        {
            _tenderRepository.DeleteMessage(id);
        }

        public void UpdateTender(Tender tender)
        {
            _tenderRepository.UpdateTender(tender);
        }

        public void DeclineMessage(int id)
        {
            _tenderRepository.DeclineMessage(id);
        }

        public Tender GetTenderByMessageId(int id)
        {
            return _tenderRepository.GetTenderByMessageId(id);
        }

        public void CreateTender(Tender tender)
        {
            tender.QueueName = string.Join("", tender.Name.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries)) + "-queue";
            tender.RoutingKey = Guid.NewGuid().ToString();
            _tenderRepository.CreateTender(tender);
        }
    }
}
