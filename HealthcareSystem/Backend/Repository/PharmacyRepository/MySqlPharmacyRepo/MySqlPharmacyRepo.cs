using Backend.Model;
using Backend.Model.Pharmacies;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Repository
{
    public class MySqlPharmacyRepo : IPharmacyRepo
    {
        private readonly MyDbContext _context;

        public MySqlPharmacyRepo(MyDbContext context)
        {
            _context = context;
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public IEnumerable<Pharmacy> GetAllPharmacies()
        {
            return _context.Pharmacies.ToList();
        }

        public Pharmacy GetPharmacyById(int id)
        {
            return _context.Pharmacies.FirstOrDefault(p => p.Id == id);
        }

        public void CreatePharmacy(Pharmacy p)
        {
            if (p == null)
            {
                throw new ArgumentNullException(nameof(p));
            }

            _context.Pharmacies.Add(p);
            _context.SaveChanges();
        }

        public void UpdatePharmacy(Pharmacy p)
        {
            _context.Update(p);
            _context.SaveChanges();
        }

        public void DeletePharmacy(Pharmacy p)
        {
            if (p == null)
            {
                throw new ArgumentNullException(nameof(p));
            }
            _context.Pharmacies.Remove(p);
        }

        public Pharmacy GetPharmacyByExchangeName(string exchangeName)
        {
            return _context.Pharmacies.SingleOrDefault(p => p.ActionsBenefitsExchangeName == exchangeName);
        }

        public IEnumerable<Pharmacy> GetPharmaciesBySubscribed(bool subscribed)
        {
            return _context.Pharmacies.Where(p => p.ActionsBenefitsSubscribed == subscribed);
        }
    }
}
