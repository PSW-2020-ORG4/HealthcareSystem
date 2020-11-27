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

        public MyDbContext GetContext()
        {
            var builder = new DbContextOptionsBuilder<MyDbContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            var options = builder.Options;
            var pharmacy = new Pharmacy { Id = 1, Name = "a1", ApiKey = "a1", Url = "u1", ActionsBenefitsExchangeName = "e1", ActionsBenefitsSubscribed = true };
            var context = new MyDbContext(options);
            context.Add(pharmacy);
            context.Add(new ActionBenefit { Id = 1, PharmacyId = 1, Pharmacy = pharmacy, Subject = "s", Message = "m", IsPublic = false });
            context.Add(new ActionBenefit { Id = 2, PharmacyId = 1, Pharmacy = pharmacy, Subject = "ss", Message = "mm", IsPublic = true });
            context.SaveChanges();

            return context;
        }

        [Fact]
        public void Index_ReturnsSingleItem()
        {
            using (var context = GetContext())
            {
                ActionBenefitService actionBenefitService = new ActionBenefitService(new MySqlActionBenefitRepository(context), new MySqlPharmacyRepo(context));
                var controller = new ActionBenefitController(actionBenefitService);

                ViewResult result = (ViewResult)controller.Index();

                Assert.IsType<List<ActionBenefit>>(result.Model);
            }
        }

        [Fact]
        public void Details_ReturnsActionBenfit_Id_1()
        {
            using (var context = GetContext())
            {
                ActionBenefitService actionBenefitService = new ActionBenefitService(new MySqlActionBenefitRepository(context), new MySqlPharmacyRepo(context));
                var controller = new ActionBenefitController(actionBenefitService);

                ViewResult result = (ViewResult)controller.Details(1);

                Assert.Equal("m", ((ActionBenefit)result.Model).Message);
            }
        }

        [Fact]
        public void MakePublic_SetsActionBenefit_IsPublic_ToValue()
        {
            using (var context = GetContext())
            {
                ActionBenefitService actionBenefitService = new ActionBenefitService(new MySqlActionBenefitRepository(context), new MySqlPharmacyRepo(context));
                var controller = new ActionBenefitController(actionBenefitService);

                Assert.False(actionBenefitService.GetActionBenefitById(1).IsPublic);
                controller.MakePublic(1, true);
                Assert.True(actionBenefitService.GetActionBenefitById(1).IsPublic);
            }

        }

        [Fact]
        public void MakePublic_SetsInvalidActionBenefit_IsPublic_ToValue_ThrowException()
        {
            using (var context = GetContext())
            {
                ActionBenefitService actionBenefitService = new ActionBenefitService(new MySqlActionBenefitRepository(context), new MySqlPharmacyRepo(context));
                var controller = new ActionBenefitController(actionBenefitService);

                Assert.Throws<ArgumentException>(() => controller.MakePublic(5, true));
            }

        }
    }
}
