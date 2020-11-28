using Backend.Model;
using Backend.Model.Pharmacies;
using Backend.Repository;
using Backend.Service;
using Moq;
using System;
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
            var pharmacy = new Pharmacy { Id = 1, Name = "apoteka1", ApiKey = "api1", Url = "url1", ActionsBenefitsExchangeName = exchange, ActionsBenefitsSubscribed = true };
            var message = new ActionBenefitMessage { Subject = "akcija1", Message = "blablabla" };

            mockPharmacyRepo.Setup(r => r.GetPharmacyByExchangeName(exchange)).Returns(pharmacy);
            mockActionBenefitRepo.Setup(r => r.CreateActionBenefit(It.Is<ActionBenefit>(ab => ab.PharmacyId == pharmacy.Id && ab.Message == message.Message && ab.Subject == message.Subject))).Verifiable();
            ActionBenefitService actionBenefitService = new ActionBenefitService(mockActionBenefitRepo.Object, mockPharmacyRepo.Object);

            actionBenefitService.CreateActionBenefit(exchange, message);

            mockActionBenefitRepo.Verify();
        }

        [Fact]
        public void CreateActionBenefit_InvalidMessage_ThrowsException()
        {
            var mockPharmacyRepo = new Mock<IPharmacyRepo>();
            var mockActionBenefitRepo = new Mock<IActionBenefitRepository>();
            var exchange = "ex1";
            var pharmacy = new Pharmacy { Id = 1, Name = "apoteka1", ApiKey = "api1", Url = "url1", ActionsBenefitsExchangeName = exchange, ActionsBenefitsSubscribed = true };
            var message = new ActionBenefitMessage { Subject = null, Message = null };

            mockPharmacyRepo.Setup(r => r.GetPharmacyByExchangeName(exchange)).Returns(pharmacy);
            ActionBenefitService actionBenefitService = new ActionBenefitService(mockActionBenefitRepo.Object, mockPharmacyRepo.Object);

            Assert.Throws<ArgumentException>(() => actionBenefitService.CreateActionBenefit(exchange, message));
        }
    }
}