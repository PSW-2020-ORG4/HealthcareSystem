using Backend.Model;
using Backend.Model.Pharmacies;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IntegrationAdaptersActionBenefitService.Repository
{
    public class MySqlPharmacyRepo : IPharmacyRepo
    {
        private readonly MyDbContext _context;

        public MySqlPharmacyRepo(MyDbContext context)
        {
            _context = context;
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
