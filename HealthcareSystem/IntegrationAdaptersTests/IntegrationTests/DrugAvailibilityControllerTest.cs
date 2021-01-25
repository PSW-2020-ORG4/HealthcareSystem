using Backend.Model;
using Backend.Model.Pharmacies;
using IntegrationAdapters.Adapters;
using IntegrationAdapters.Controllers;
using IntegrationAdapters.Dtos;
using IntegrationAdapters.MicroserviceComunicator;
using IntegrationAdaptersPharmacySystemService.Repository;
using IntegrationAdaptersPharmacySystemService.Service;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationAdaptersTests.IntegrationTests
{
    public class DrugAvailabilityControllerTest
    {
        [Fact]
        public async void Search_ForDrug_Found_And_NotFound()
        {
            DbContextInMemory testData = new DbContextInMemory();
            MyDbContext context = testData._context;
            var p = context.Pharmacies;

            IPharmacyService pharmacyService = new PharmacyService(new MySqlPharmacyRepo(context));
            var pharmacyServiceMock = new Mock<IPharmacySystemService>();
            pharmacyServiceMock.Setup(s => s.GetAll()).Returns(Task.Run(() => (pharmacyService.GetAllPharmacies()).ToList()));
            var adapterContext = new Mock<IAdapterContext>();
            adapterContext.Setup(c => c.PharmacySystemAdapter.DrugAvailibility(It.Is<string>(name => name == "droga"))).Returns(
                new List<DrugDto>() {
                     new DrugDto() { Id = 1, Name = "droga", Quantity = 5, PharmacyDto = new PharmacyDto {Id = 1, Name = "lokacija-1" } },
                     new DrugDto() { Id = 4, Name = "drogaricin", Quantity = 5, PharmacyDto = new PharmacyDto {Id = 3, Name = "lokacija-3" } }
                }
                );
            adapterContext.Setup(c => c.PharmacySystemAdapter.DrugAvailibility(It.Is<string>(name => name != "droga"))).Returns(new List<DrugDto>());
            adapterContext.Setup(c => c.SetPharmacySystemAdapter(It.IsAny<PharmacySystem>())).Returns(new Mock<IPharmacySystemAdapter>().Object);
            adapterContext.Setup(c => c.RemoveAdapter()).Verifiable();
            DrugAvailabilityController controller = new DrugAvailabilityController(adapterContext.Object, pharmacyServiceMock.Object);

            ViewResult result = await controller.Search("droga") as ViewResult;
            adapterContext.Verify(c => c.RemoveAdapter());
            Assert.NotEmpty((IEnumerable<SearchResultDto>)result.Model);

            result = await controller.Search("nesto") as ViewResult;
            adapterContext.Verify(c => c.RemoveAdapter());
            Assert.Empty((IEnumerable<SearchResultDto>)result.Model);


        }
    }
}
