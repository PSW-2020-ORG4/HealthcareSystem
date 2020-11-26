using Backend.Model;
using Backend.Model.Pharmacies;
using Backend.Repository;
using Backend.Service;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace IntegrationAdaptersTests.IntegrationTests
{
    public class ActionBenefitServiceTest
    {
        [Fact]
        public void Create_valid_action_benefit()
        {
            var builder = new DbContextOptionsBuilder<MyDbContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            var options = builder.Options;

            var exchange = "ex1";
            var message = new ActionBenefitMessage { Subject = "akcija1", Message = "blablabla" };
            var pharmacy = new Pharmacy { Id = 1, Name = "apoteka1", ApiKey = "api1", Url = "url1", ActionsBenefitsExchangeName = exchange, ActionsBenefitsSubscribed = true };

            using (var context = new MyDbContext(options))
            {
                context.Add(pharmacy);
                context.SaveChanges();
            }

            using (var context = new MyDbContext(options))
            {
                var pharmacyRepo = new MySqlPharmacyRepo(context);
                var actionBenefitRepo = new MySqlActionBenefitRepository(context);
                var actionBenefitService = new ActionBenefitService(actionBenefitRepo, pharmacyRepo);

                actionBenefitService.CreateActionBenefit(exchange, message);

                Assert.Contains(context.ActionsBenefits, ab => ab.PharmacyId == pharmacy.Id && ab.Message == message.Message && ab.Subject == message.Subject);
            }
        }

        [Fact]
        public void Create_invalid_action_benefit()
        {
            var builder = new DbContextOptionsBuilder<MyDbContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            var options = builder.Options;

            var exchange = "ex1";
            var message = new ActionBenefitMessage { Subject = "", Message = "" };
            var pharmacy = new Pharmacy { Id = 1, Name = "apoteka1", ApiKey = "api1", Url = "url1", ActionsBenefitsExchangeName = exchange, ActionsBenefitsSubscribed = true };

            using (var context = new MyDbContext(options))
            {
                context.Add(pharmacy);
                context.SaveChanges();
            }

            using (var context = new MyDbContext(options))
            {
                var pharmacyRepo = new MySqlPharmacyRepo(context);
                var actionBenefitRepo = new MySqlActionBenefitRepository(context);
                var actionBenefitService = new ActionBenefitService(actionBenefitRepo, pharmacyRepo);

                Assert.Throws<ArgumentException>(() => actionBenefitService.CreateActionBenefit(exchange, message));
            }
        }
    }
}
