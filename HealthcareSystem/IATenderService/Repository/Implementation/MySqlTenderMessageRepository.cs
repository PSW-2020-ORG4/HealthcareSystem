using Backend.Model;
using Backend.Model.Pharmacies;
using System.Collections.Generic;
using System.Linq;

namespace IntegrationAdaptersTenderService.Repository.Implementation
{
    public class MySqlTenderMessageRepository : ITenderMessageRepository
    {
        private readonly MyDbContext _context;

        public MySqlTenderMessageRepository(MyDbContext context)
        {
            _context = context;
        }

        public void CreateTenderMessage(TenderMessage tm)
        {
            _context.TenderMessages.Add(tm);
            _context.SaveChanges();
        }

        public TenderMessage GetAcceptedByTenderId(int id)
        {
            return _context.TenderMessages.FirstOrDefault(x => x.TenderId == id && x.IsAccepted == true);
        }

        public IEnumerable<TenderMessage> GetAll()
        {
            return _context.TenderMessages.ToList();
        }

        public IEnumerable<TenderMessage> GetAllByTender(int id)
        {
            return _context.TenderMessages.Where(tm => tm.TenderId == id).ToList();
        }

        public TenderMessage GetById(int id)
        {
            return _context.TenderMessages.FirstOrDefault(tm => tm.Id == id);
        }

        public void UpdateTenderMessage(TenderMessage tm)
        {
            _context.TenderMessages.Update(tm);
            _context.SaveChanges();
        }
    }
}
