using Backend.Model;
using Backend.Model.Pharmacies;
using Backend.Repository;
using Backend.Service;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace IntegrationAdaptersTests.UnitTests
{
    public class ActionBenefitServiceTest
    {
        [Fact]
        public void Save_action_benefit_to_db()
        {
            var builder = new DbContextOptionsBuilder<MyDbContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            var options = builder.Options;

            using (var context = new MyDbContext(options))
            {
                var pharmacies = new List<Pharmacy>
                {
                    new Pharmacy { Name="apoteka1", ApiKey="api1", ActionsBenefitsExchangeName="ex1", ActionsBenefitsSubscribed=true },
                    new Pharmacy { Name="apoteka2", ApiKey="api2", ActionsBenefitsExchangeName="ex2", ActionsBenefitsSubscribed=true }
                };
                context.AddRange(pharmacies);
                context.SaveChanges();
            }

            using (var context = new MyDbContext(options))
            {
                var pharmacyRepo = new MySqlPharmacyRepo(context);
                var actionBenefitRepo = new MySqlActionBenefitRepository(context);
                var actionBenefitService = new ActionBenefitService(actionBenefitRepo, pharmacyRepo);

                actionBenefitService.CreateActionBenefit("ex1", new ActionBenefitMessage { Subject="akcija1", Message="blablabla"});

                Assert.Contains(context.ActionsBenefits, a => a.Pharmacy.ActionsBenefitsExchangeName == "ex1");
            }

        }

    }
}
