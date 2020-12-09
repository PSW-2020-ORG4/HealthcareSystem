﻿using Backend.Model;
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

        public IEnumerable<PharmacySystem> GetAllPharmacies()
        {
            return _context.Pharmacies.ToList();
        }

        public PharmacySystem GetPharmacyById(int id)
        {
            return _context.Pharmacies.FirstOrDefault(p => p.Id == id);
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

        public void DeletePharmacy(PharmacySystem p)
        {
            if (p == null)
            {
                throw new ArgumentNullException(nameof(p));
            }
            _context.Pharmacies.Remove(p);
        }

        public PharmacySystem GetPharmacyByExchangeName(string exchangeName)
        {
            return _context.Pharmacies.SingleOrDefault(p => p.ActionsBenefitsExchangeName == exchangeName);
        }

        public IEnumerable<PharmacySystem> GetPharmaciesBySubscribed(bool subscribed)
        {
            return _context.Pharmacies.Where(p => p.ActionsBenefitsSubscribed == subscribed);
        }
    }
}
