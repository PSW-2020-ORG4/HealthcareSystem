using Backend.Model.Pharmacies;
using IntegrationAdapters.Controllers;
using IntegrationAdapters.MicroserviceComunicator;
using IntegrationAdaptersTests.DataFactory;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationAdaptersTests.UnitTests
{
    public class EditPharmacyTests
    {
        [Fact]
        public async void Finds_pharmacy_to_edit()
        {
            var controller = GetPharmacyController();
            var result = await controller.Edit(1);

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void Does_not_find_pharmacy_to_edit()
        {
            var controller = GetPharmacyController();
            var result = await controller.Edit(2);

            Assert.IsType<NotFoundObjectResult>(result);
        }

        private PharmacyController GetPharmacyController()
        {
            var pharmacy = CreatePharmacy.CreateValidTestObject();
            pharmacy.Id = 1;
            PharmacySystem pharmacyNull = null;
            var pharmacyServiceMock = new Mock<IPharmacySystemService>();
            pharmacyServiceMock.Setup(p => p.Get(It.Is<int>(i => i == 1))).Returns(Task.Run(() => pharmacy));
            pharmacyServiceMock.Setup(p => p.Get(It.Is<int>(i => i != 1))).Returns(Task.Run(() => pharmacyNull));

            return new PharmacyController(pharmacyServiceMock.Object);
        }
    }
}
