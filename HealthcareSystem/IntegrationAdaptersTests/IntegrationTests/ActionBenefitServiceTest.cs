using Backend.Model;
using Backend.Repository;
using Backend.Service;
using System;
using Xunit;

namespace IntegrationAdaptersTests.IntegrationTests
{
    public class ActionBenefitServiceTest
    {
        [Fact]
        public void CreateActionBenefit_Valid_Invalid_Test()
        {
            DbContextInMemory testData = new DbContextInMemory();
            testData.SetActionBenefitServiceTestIntegration();
            MyDbContext context = testData._context;
            var pharmacyRepo = new MySqlPharmacyRepo(context);
            var actionBenefitRepo = new MySqlActionBenefitRepository(context);
            var actionBenefitService = new ActionBenefitService(actionBenefitRepo, pharmacyRepo);
            var message = new ActionBenefitMessage { Subject = "akcija1", Message = "blablabla" };
            
            actionBenefitService.CreateActionBenefit("exchange1", message);
            Assert.Contains(context.ActionsBenefits, ab => ab.Message == message.Message && ab.Subject == message.Subject);

            message = new ActionBenefitMessage { Subject = "", Message = "" };
            Assert.Throws<ArgumentException>(() => actionBenefitService.CreateActionBenefit("exchange1", message));
        }
    }
}
