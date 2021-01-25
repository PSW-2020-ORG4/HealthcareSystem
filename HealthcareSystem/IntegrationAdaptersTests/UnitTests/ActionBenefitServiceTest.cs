using Backend.Model;
using Backend.Model.Exceptions;
using Backend.Model.Pharmacies;
using Backend.Repository;
using Backend.Service;
using Moq;
using Xunit;

namespace IntegrationAdaptersTests.UnitTests
{
    public class ActionBenefitServiceTest
    {
        [Fact]
        public void CreateActionBenefit_Valid_VerifyCreateActionBenefit()
        {
            var mockPharmacyRepo = new Mock<IPharmacyRepo>();
            var mockActionBenefitRepo = new Mock<IActionBenefitRepository>();
            var exchange = "ex1";
            var pharmacy = new PharmacySystem { Id = 1, Name = "apoteka1", ApiKey = "api1", Url = "url1", ActionsBenefitsExchangeName = exchange, ActionsBenefitsSubscribed = true };
            var message = new ActionBenefitMessage("akcija1", "blablabla");

            mockPharmacyRepo.Setup(r => r.GetPharmacyByExchangeName(exchange)).Returns(pharmacy);
            mockActionBenefitRepo.Setup(r => r.CreateActionBenefit(It.Is<ActionBenefit>(ab => ab.PharmacyId == pharmacy.Id && ab.Message.Message == message.Message && ab.Message.Subject == message.Subject))).Verifiable();
            ActionBenefitService actionBenefitService = new ActionBenefitService(mockActionBenefitRepo.Object, mockPharmacyRepo.Object);

            actionBenefitService.CreateActionBenefit(exchange, message);

            mockActionBenefitRepo.Verify();
        }

        [Fact]
        public void CreateActionBenefit_InvalidMessage_ThrowsException()
        {
            Assert.Throws<ValidationException>(() => new ActionBenefitMessage(null, null));
        }
    }
}