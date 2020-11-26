using Backend.Repository;
using IntegrationAdapters.Controllers;
using IntegrationAdaptersTests.DataFactory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Xunit;
using Moq;

namespace IntegrationAdaptersTests.UnitTests
{
    public class ApiRegisterTests
    {
        [Fact]
        public void Successfully_registers_api()
        {
            var pharmacy = CreatePharmacy.CreateValidTestObject();
            var stubRepository = new Mock<IPharmacyRepo>();
            var tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>())
            {
                ["Success"] = "Registration successful!"
            };
            var controller = new PharmacyController(stubRepository.Object)
            {
                TempData = tempData
            };

            var result = controller.ApiRegister(pharmacy);

            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public void Does_not_successfully_register_api()
        {
            var pharmacy = CreatePharmacy.CreateInvalidTestObject();
            var stubRepository = new Mock<IPharmacyRepo>();
            var tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>())
            {
                ["Success"] = "Registration successful!"
            };
            var controller = new PharmacyController(stubRepository.Object)
            {
                TempData = tempData
            };
            controller.ModelState.AddModelError("Url", "Required");

            var result = controller.ApiRegister(pharmacy);

            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
