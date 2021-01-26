// using Backend.Model;
// using Backend.Model.Pharmacies;
// using Backend.Repository;
// using Backend.Service.Pharmacies;
// using IntegrationAdapters.Adapters;
// using IntegrationAdapters.Controllers;
// using IntegrationAdapters.Dtos;
// using Microsoft.AspNetCore.Mvc;
// using Moq;
// using System.Collections.Generic;
// using Xunit;

// namespace IntegrationAdaptersTests.IntegrationTests
// {
//     public class DrugAvailabilityControllerTest
//     {
//         [Fact]
//         public void Search_ForDrug_Found_And_NotFound()
//         {
//             DbContextInMemory testData = new DbContextInMemory();
//             MyDbContext context = testData._context;

//             IPharmacyService pharmacyService = new PharmacyService(new MySqlPharmacyRepo(context));
//             var adapterContext = new Mock<IAdapterContext>();
//             adapterContext.Setup(c => c.PharmacySystemAdapter.DrugAvailibility(It.Is<string>(name => name == "droga"))).Returns(
//                 new List<DrugDto>() {
//                     new DrugDto() { Id = 1, Name = "droga", Quantity = 5, PharmacyDto = new PharmacyDto {Id = 1, Name = "lokacija-1" } },
//                     new DrugDto() { Id = 4, Name = "drogaricin", Quantity = 5, PharmacyDto = new PharmacyDto {Id = 3, Name = "lokacija-3" } }
//                 }
//                 );
//             adapterContext.Setup(c => c.PharmacySystemAdapter.DrugAvailibility(It.Is<string>(name => name != "droga"))).Returns(new List<DrugDto>());
//             adapterContext.Setup(c => c.SetPharmacySystemAdapter(It.IsAny<PharmacySystem>())).Returns(new Mock<IPharmacySystemAdapter>().Object);
//             adapterContext.Setup(c => c.RemoveAdapter()).Verifiable();
//             DrugAvailabilityController controller = new DrugAvailabilityController(adapterContext.Object, pharmacyService);

//             ViewResult result = (ViewResult)controller.Search("droga");
//             adapterContext.Verify(c => c.RemoveAdapter());
//             Assert.NotEmpty((IEnumerable<SearchResultDto>)result.Model);

//             result = (ViewResult)controller.Search("nesto");
//             adapterContext.Verify(c => c.RemoveAdapter());
//             Assert.Empty((IEnumerable<SearchResultDto>)result.Model);


//         }
//     }
// }
