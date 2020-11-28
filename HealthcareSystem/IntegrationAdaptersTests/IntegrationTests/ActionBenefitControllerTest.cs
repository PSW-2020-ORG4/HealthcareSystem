using Backend.Model;
using Backend.Model.Pharmacies;
using Backend.Repository;
using Backend.Service;
using IntegrationAdapters.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace IntegrationAdaptersTests.IntegrationTests
{
    public class ActionBenefitControllerTest
    {
        [Fact]
        public void Index_ReturnsSingleItem()
        {
            var builder = new DbContextOptionsBuilder<MyDbContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            var options = builder.Options;

            var pharmacy = new Pharmacy { Id = 1, Name = "a1", ApiKey = "a1", Url = "u1", ActionsBenefitsExchangeName = "e1", ActionsBenefitsSubscribed = true };

            using (var context = new MyDbContext(options))
            {
                context.Add(pharmacy);
                context.Add(new ActionBenefit { Id = 1, PharmacyId = 1, Pharmacy = pharmacy, Subject = "s", Message = "m", IsPublic = true });
                context.SaveChanges();
            }

            using (var context = new MyDbContext(options))
            {
                ActionBenefitService actionBenefitService = new ActionBenefitService(new MySqlActionBenefitRepository(context), new MySqlPharmacyRepo(context));
                var controller = new ActionBenefitController(actionBenefitService);

                ViewResult result = (ViewResult)controller.Index();

                Assert.True(((List<ActionBenefit>)result.Model).Count == 1);
            }
        } 
    }
}
