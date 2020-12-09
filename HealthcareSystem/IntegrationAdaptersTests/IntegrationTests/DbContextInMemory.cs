using Backend.Model;
using Backend.Model.Pharmacies;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationAdaptersTests.IntegrationTests
{
    public class DbContextInMemory
    {
        private DbContextOptionsBuilder<MyDbContext> _builder = new DbContextOptionsBuilder<MyDbContext>();
        private DbContextOptions<MyDbContext> _options;
        public MyDbContext _context { get; private set; }

        public DbContextInMemory()
        {
            _builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            _options = _builder.Options;
           
        }

        public void SetActionBenefitControllerTestIntegration()
        {
            _context = new MyDbContext(_options);
            var pharmacy = new PharmacySystem { Id = 1, Name = "a1", ApiKey = "a1", Url = "u1", ActionsBenefitsExchangeName = "e1", ActionsBenefitsSubscribed = true };
            _context.Add(pharmacy);
            _context.Add(new ActionBenefit { Id = 1, PharmacyId = 1, Pharmacy = pharmacy, Subject = "s", Message = "m", IsPublic = false });
            _context.Add(new ActionBenefit { Id = 2, PharmacyId = 1, Pharmacy = pharmacy, Subject = "ss", Message = "mm", IsPublic = true });
            _context.SaveChanges();
        }

        public void SetActionBenefitServiceTestIntegration()
        {
            _context = new MyDbContext(_options);
            var pharmacy = new PharmacySystem { Id = 1, Name = "apoteka1", ApiKey = "api1", Url = "url1", ActionsBenefitsExchangeName = "exchange1", ActionsBenefitsSubscribed = true };
            _context.Add(pharmacy);
            _context.SaveChanges();
        }

        public void setDrugAvailibilityControllerTestIntegration()
        {
            _context = new MyDbContext(_options);
            _context.Add(new PharmacySystem { Id = 1, Name = "apoteka-1", ApiKey = "api-1", Url = "url-1", ActionsBenefitsExchangeName = "exchange-1", ActionsBenefitsSubscribed = true });
            _context.SaveChanges();
        }
    }
}
