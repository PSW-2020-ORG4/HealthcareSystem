using Backend.Model;
using Backend.Model.Pharmacies;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IntegrationAdaptersPharmacySystemService.Repository
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

        public IEnumerable<PharmacySystem> GetAllPharmacies()
        {
            return _context.Pharmacies.ToList();
        }

        public PharmacySystem GetPharmacyById(int id)
        {
            return _context.Pharmacies.FirstOrDefault(p => p.Id == id);
        }

        public PharmacySystem GetPharmacyByIdNoTracking(int id)
        {
            return _context.Pharmacies.AsNoTracking().Where(p => p.Id == id).FirstOrDefault();
        }

        public void CreatePharmacy(PharmacySystem p)
        {
            if (p == null)
            {
                throw new ArgumentNullException(nameof(p));
            }

            _context.Pharmacies.Add(p);
            _context.SaveChanges();
        }

        public void UpdatePharmacy(PharmacySystem p)
        {
            _context.Update(p);
            _context.SaveChanges();
        }

        public IEnumerable<PharmacySystem> GetPharmaciesBySubscribed(bool subscribed)
        {
            return _context.Pharmacies.Where(p => p.ActionsBenefitsSubscribed == subscribed);
        }

    }
}
