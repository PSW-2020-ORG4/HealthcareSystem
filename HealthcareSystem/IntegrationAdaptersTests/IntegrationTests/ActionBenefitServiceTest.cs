using Backend.Model;
using Backend.Model.Exceptions;
using IntegrationAdaptersActionBenefitService.Repository;
using IntegrationAdaptersActionBenefitService.Service;
using Xunit;

namespace IntegrationAdaptersTests.IntegrationTests
{
    public class ActionBenefitServiceTest
    {
        [Fact]
        public void CreateActionBenefit_Valid_Invalid_Test()
        {
            DbContextInMemory testData = new DbContextInMemory();
            MyDbContext context = testData._context;
            var pharmacyRepo = new MySqlPharmacyRepo(context);
            var actionBenefitRepo = new MySqlActionBenefitRepository(context);
            var actionBenefitService = new ActionBenefitService(actionBenefitRepo, pharmacyRepo);
            var message = new ActionBenefitMessage("akcija1", "blablabla");

            actionBenefitService.CreateActionBenefit("exchange", message);
            Assert.Contains(context.ActionsBenefits, ab => ab.Message.Message == message.Message && ab.Message.Subject == message.Subject);

            Assert.Throws<ValidationException>(() => new ActionBenefitMessage("", ""));
        }
    }
}
