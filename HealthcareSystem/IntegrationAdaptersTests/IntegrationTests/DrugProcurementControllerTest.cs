using Backend.Model;
using Backend.Model.Pharmacies;
using Backend.Repository;
using Backend.Repository.DrugInRoomRepository.MySqlDrugInRoomRepository;
using Backend.Repository.DrugRepository.MySQLDrugRepository;
using Backend.Service.Pharmacies;
using IntegrationAdapters.Adapters;
using IntegrationAdapters.Adapters.Development;
using IntegrationAdapters.Controllers;
using IntegrationAdapters.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using Service.DrugAndTherapy;
using System;
using Xunit;

namespace IntegrationAdaptersTests.IntegrationTests
{
    public class DrugProcurementControllerTest
    {
        [Fact]
        public void Order_Development_Succesfull()
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
            DbContextInMemory testData = new DbContextInMemory();
            MyDbContext context = testData._context;
            var adapterContext = new Mock<IAdapterContext>();
            adapterContext.Setup(a => a.PharmacySystemAdapter.OrderDrugs(1, 1, 1)).Returns(true);
            adapterContext.Setup(a => a.SetPharmacySystemAdapter(It.IsAny<PharmacySystem>())).Returns(new PharmacySystemAdapter_Id1());
            DrugProcurementController controller = 
                new DrugProcurementController(new DrugService(new MySqlConfirmedDrugRepository(context),
                                                              new MySqlUnconfirmedDrugRepository(context),
                                                              new MySqlDrugInRoomRepository(context)),
                                              adapterContext.Object,
                                              new PharmacyService(new MySqlPharmacyRepo(context)));
            controller.TempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());

            var quantityBefore = context.Drugs.Find(1).Quantity;
            controller.Order(new DrugProcurementDTO() { PharmacySystemId = 1, PharmacyId = 1, DrugId = 1, Quantity = 1, Code = "20033" });
            var quantityAfter = context.Drugs.Find(1).Quantity;

            Assert.True(quantityAfter - quantityBefore == 1);
        }

        [Fact]
        public void Order_Production_Successfull()
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Production");
            DbContextInMemory testData = new DbContextInMemory();
            MyDbContext context = testData._context;
            var adapterContext = new Mock<IAdapterContext>();
            adapterContext.Setup(a => a.PharmacySystemAdapter.OrderDrugs(1, 1, 1)).Returns(true);
            adapterContext.Setup(a => a.SetPharmacySystemAdapter(It.IsAny<PharmacySystem>())).Returns(new PharmacySystemAdapter_Id1());
            DrugProcurementController controller =
                new DrugProcurementController(new DrugService(new MySqlConfirmedDrugRepository(context),
                                                              new MySqlUnconfirmedDrugRepository(context),
                                                              new MySqlDrugInRoomRepository(context)),
                                              adapterContext.Object,
                                              new PharmacyService(new MySqlPharmacyRepo(context)));
            controller.TempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());

            var quantityBefore = context.Drugs.Find(1).Quantity;
            controller.Order(new DrugProcurementDTO() { PharmacySystemId = 1, PharmacyId = 1, DrugId = 1, Quantity = 1, Code = "20033" });
            var quantityAfter = context.Drugs.Find(1).Quantity;

            Assert.True(quantityAfter - quantityBefore == 1);
        }

        [Fact]
        public void Order_Succesfull_But_Drug_Not_In_Database()
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
            DbContextInMemory testData = new DbContextInMemory();
            MyDbContext context = testData._context;
            var adapterContext = new Mock<IAdapterContext>();
            adapterContext.Setup(a => a.PharmacySystemAdapter.OrderDrugs(1, 1, 1)).Returns(true);
            adapterContext.Setup(a => a.SetPharmacySystemAdapter(It.IsAny<PharmacySystem>())).Returns(new PharmacySystemAdapter_Id1());
            DrugProcurementController controller =
                new DrugProcurementController(new DrugService(new MySqlConfirmedDrugRepository(context),
                                                                                                 new MySqlUnconfirmedDrugRepository(context),
                                                                                                 new MySqlDrugInRoomRepository(context)),
                                                                                 adapterContext.Object,
                                                                                 new PharmacyService(new MySqlPharmacyRepo(context)));
            controller.TempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());

            var quantityBefore = context.Drugs.Find(1).Quantity;
            controller.Order(new DrugProcurementDTO() { PharmacySystemId = 1, PharmacyId = 1, DrugId = 1, Quantity = 1, Code = "20000" });
            var quantityAfter = context.Drugs.Find(1).Quantity;

            Assert.True(quantityAfter - quantityBefore == 0);
        }
    }
}
