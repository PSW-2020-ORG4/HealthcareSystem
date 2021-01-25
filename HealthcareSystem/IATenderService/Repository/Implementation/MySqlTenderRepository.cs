using System.Collections.Generic;
using System.Linq;
using Backend.Model;
using Backend.Model.DTO;
using Backend.Model.Pharmacies;

namespace IntegrationAdaptersTenderService.Repository.Implementation
{
    public class MySqlTenderRepository : IntegrationAdaptersTenderService.Repository.ITenderRepository
    {
        private readonly MyDbContext _context;

        public MySqlTenderRepository(MyDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Tender> GetAllTenders()
        {
            var tenders = _context.Tenders.ToList();
            foreach (var tender in tenders)
            {
                tender.Drugs = _context.TenderDrugs.Where(x => x.TenderId == tender.Id).ToList();
            }
            return tenders;
        }

        public Tender GetTenderById(int id)
        {
            var tender = _context.Tenders.First(x => x.Id == id);
            if (tender == null) return null;

            tender.Drugs = _context.TenderDrugs.Where(x => x.TenderId == tender.Id).ToList();
            return tender;
        }

        public List<TenderDrugDTO> GetDrugsForTender(int id)
        {
            var drugs = new List<TenderDrugDTO>();
            foreach (var tenderDrug in GetTenderById(id).Drugs)
            {
                var drug = _context.Drugs.FirstOrDefault(x => x.Id == tenderDrug.DrugId);
                drugs.Add(new TenderDrugDTO()
                {
                    Id = drug.Id,
                    Name = drug.Name,
                    Quantity = tenderDrug.Quantity
                });
            }

            return drugs;
        }

        public List<TenderMessageDTO> GetMessagesForTender(int id)
        {
            return _context.TenderMessages
                .Where(x => x.TenderId == id)
                .ToList()
                .Select(message => new TenderMessageDTO()
                {
                    Id = message.Id, 
                    Identification = message.Identification, 
                    ReplyRoutingKey = message.ReplyRoutingKey,
                    Offers = GetOffersForMessage(message.Id)
                })
                .ToList();
        }

        public List<TenderOfferDTO> GetOffersForMessage(int id)
        {
            return _context.TenderOffers
                .Where(x => x.TenderMessageId == id)
                .ToList()
                .Select(offer => new TenderOfferDTO() 
                    { 
                        Name = offer.Name, 
                        Code = offer.Code, 
                        Price = offer.Price, 
                        Quantity = offer.Quantity
                    })
                .ToList();
        }

        public void UpdateTender(Tender tender)
        {
            _context.Tenders.Update(tender);
            _context.SaveChanges();
        }

        public Tender GetTenderByMessageId(int id)
        {
            var tenderId = _context.TenderMessages.First(x => x.Id == id).TenderId;
            return _context.Tenders.First(x => x.Id == tenderId);
        }

        public IEnumerable<Tender> GetAllNotClosedTenders()
        {
            var tenders = _context.Tenders.Where(x => x.IsClosed == false).ToList();
            if (tenders == null)
                return new List<Tender>();
            foreach(var tender in tenders)
            {
                tender.Drugs = _context.TenderDrugs.Where(x => x.TenderId == tender.Id).ToList();
            }
            return tenders;
        }

        public Tender GetTenderByRoutingKey(string routingKey)
        {
            var tender = _context.Tenders.FirstOrDefault(x => x.RoutingKey == routingKey);
            if (tender == null)
                return null;
            tender.Drugs = _context.TenderDrugs.Where(x => x.TenderId == tender.Id).ToList();
            return tender;
        }

        public void CreateTender(Tender tender)
        {
            _context.Tenders.Add(tender);
            _context.SaveChanges();
        }
    }
}
