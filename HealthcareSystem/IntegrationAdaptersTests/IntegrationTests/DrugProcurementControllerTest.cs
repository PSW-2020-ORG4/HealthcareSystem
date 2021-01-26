using Backend.Model;
using Backend.Repository.DrugInRoomRepository.MySqlDrugInRoomRepository;
using Backend.Repository.DrugRepository.MySQLDrugRepository;
using IntegrationAdaptersDrugService.Controllers;
using IntegrationAdaptersDrugService.Service;
using IntegrationAdaptersDrugService.Repository.Implementation;
using Service.DrugAndTherapy;
using System;
using Xunit;
using Backend.Model.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationAdaptersTests.IntegrationTests
{
    public class DrugProcurementControllerTest
    {
        [Fact]
        public void Order_Succesfull()
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
            DbContextInMemory testData = new DbContextInMemory();
            MyDbContext context = testData._context;
            DrugServiceController controller =
                new DrugServiceController(new DrugService(new MySqlConfirmedDrugRepository(context),
                                                              new MySqlUnconfirmedDrugRepository(context),
                                                              new MySqlDrugInRoomRepository(context)),
                                          new DrugConsumptionService(new MySqlDrugConsumptionRepository(context)));

            var quantityBefore = context.Drugs.Find(1).Quantity;
            controller.AddQuantity(new AddDrugQuantityRequest { Quantity = 1, Code = "20033" });
            var quantityAfter = context.Drugs.Find(1).Quantity;

            Assert.True(quantityAfter - quantityBefore == 1);
        }

        [Fact]
        public void Order_Succesfull_But_Drug_Not_In_Database()
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
            DbContextInMemory testData = new DbContextInMemory();
            MyDbContext context = testData._context;
            DrugServiceController controller =
                new DrugServiceController(new DrugService(new MySqlConfirmedDrugRepository(context),
                                                              new MySqlUnconfirmedDrugRepository(context),
                                                              new MySqlDrugInRoomRepository(context)),
                                          new DrugConsumptionService(new MySqlDrugConsumptionRepository(context)));

            Assert.IsType<NotFoundResult>(controller.AddQuantity(new AddDrugQuantityRequest { Quantity = 1, Code = "20000" }));
        }
    }
}
