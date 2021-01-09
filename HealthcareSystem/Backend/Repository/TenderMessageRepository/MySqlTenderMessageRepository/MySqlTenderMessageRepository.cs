using Backend.Model;
using Backend.Model.Pharmacies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backend.Repository
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

        public IEnumerable<TenderMessage> GetAll()
        {
            return _context.TenderMessages.ToList();
        }

        public IEnumerable<TenderMessage> GetAllByTender(int id)
        {
            return _context.TenderMessages.Where(tm => tm.TenderId == id).ToList();
        }
    }
}
