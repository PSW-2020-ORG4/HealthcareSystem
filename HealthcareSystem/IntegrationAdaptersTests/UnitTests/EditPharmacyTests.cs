using Backend.Repository;
using Backend.Service.Pharmacies;
using IntegrationAdapters.Controllers;
using IntegrationAdaptersTests.DataFactory;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace IntegrationAdaptersTests.UnitTests
{
    public class EditPharmacyTests
    {
        [Fact]
        public void Finds_pharmacy_to_edit()
        {
            var controller = GetPharmacyController();
            var result = controller.Edit(1);

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Does_not_find_pharmacy_to_edit()
        {
            var controller = GetPharmacyController();
            var result = controller.Edit(2);

            Assert.IsType<NotFoundObjectResult>(result);
        }

        private PharmacyController GetPharmacyController()
        {
            var stubRepository = new Mock<IPharmacyRepo>();
            var mockRabbit = new Mock<Backend.Service.RabbitMqActionBenefitBackgroundService>();
            var pharmacy = CreatePharmacy.CreateValidTestObject();
            pharmacy.Id = 1;

            stubRepository.Setup(m => m.GetPharmacyById(pharmacy.Id)).Returns(pharmacy);
            return new PharmacyController(new PharmacyService(stubRepository.Object), mockRabbit.Object);
        }
    }
}
