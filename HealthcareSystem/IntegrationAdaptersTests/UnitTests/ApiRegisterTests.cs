using Backend.Service.Pharmacies;
using IntegrationAdapters.Controllers;
using IntegrationAdaptersTests.DataFactory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Xunit;
using Moq;
using Backend.Service;

namespace IntegrationAdaptersTests.UnitTests
{
    public class ApiRegisterTests
    {
        [Fact]
        public void Successfully_registers_api()
        {
            var pharmacy = CreatePharmacy.CreateValidTestObject();
            var controller = GetPharmacyController();

            var result = controller.ApiRegister(pharmacy);

            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public void Does_not_successfully_register_api()
        {
            var pharmacy = CreatePharmacy.CreateInvalidTestObject();
            var controller = GetPharmacyController();
            controller.ModelState.AddModelError("Url", "Required");

            var result = controller.ApiRegister(pharmacy);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        private PharmacyController GetPharmacyController()
        {
            var mockService = new Mock<IPharmacyService>();
            var tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>())
            {
                ["Success"] = "Registration successful!"
            };

            var mockRabbit = new Mock<IRabbitMqActionBenefitService>();
            return new PharmacyController(mockService.Object, mockRabbit.Object)
            {
                TempData = tempData
            };
        }
    }
}
