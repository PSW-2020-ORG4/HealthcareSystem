using IntegrationAdapters.Controllers;
using IntegrationAdaptersTests.DataFactory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Xunit;
using Moq;
using IntegrationAdapters.MicroserviceComunicator;
using IntegrationAdapters.Dtos;

namespace IntegrationAdaptersTests.UnitTests
{
    public class ApiRegisterTests
    {
        [Fact]
        public async void Successfully_registers_api()
        {
            var pharmacy = CreatePharmacy.CreateValidTestObject();
            var controller = GetPharmacyController();

            var result = await controller.ApiRegister(new PharmacySystemDTO { Name = pharmacy.Name, ApiKey = pharmacy.ApiKey, Url = pharmacy.Url });

            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public async void Does_not_successfully_register_api()
        {
            var pharmacy = CreatePharmacy.CreateInvalidTestObject();
            var controller = GetPharmacyController();
            controller.ModelState.AddModelError("Url", "Required");

            var result = await controller.ApiRegister(new PharmacySystemDTO { Name = pharmacy.Name, ApiKey = pharmacy.ApiKey, Url = pharmacy.Url });

            Assert.IsType<BadRequestObjectResult>(result);
        }

        private PharmacyController GetPharmacyController()
        {
            var mockService = new Mock<IPharmacySystemService>();
            var tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>())
            {
                ["Success"] = "Registration successful!"
            };

            return new PharmacyController(mockService.Object)
            {
                TempData = tempData
            };
        }
    }
}
