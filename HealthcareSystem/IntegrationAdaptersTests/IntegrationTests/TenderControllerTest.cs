using Backend.Model;
using Backend.Model.Pharmacies;
using IntegrationAdaptersTenderService.Controllers;
using IntegrationAdaptersTenderService.Repository.Implementation;
using IntegrationAdaptersTenderService.Service;
using IntegrationAdaptersTenderService.Service.RabbitMqTenderingService;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace IntegrationAdaptersTests.IntegrationTests
{
    public class TenderControllerTest
    {
        [Fact]
        public void PushDrugList_Invalid_List()
        {
            DbContextInMemory testData = new DbContextInMemory();
            MyDbContext context = testData._context;
            TenderServiceController controller =
                new TenderServiceController(new TenderService(new MySqlTenderRepository(context)),
                                            new TenderMessageService(new MySqlTenderMessageRepository(context)),
                                            new Mock<IRabbitMqTenderingService>().Object);

            controller.CreateTender(new TenderDTO());

            Assert.True(context.Tenders.Find(2) == null);
        }

        [Fact]
        public void PushDrugList_Valid_List()
        {
            DbContextInMemory testData = new DbContextInMemory();
            MyDbContext context = testData._context;
            TenderServiceController controller =
                new TenderServiceController(new TenderService(new MySqlTenderRepository(context)),
                                            new TenderMessageService(new MySqlTenderMessageRepository(context)),
                                            new Mock<IRabbitMqTenderingService>().Object);

            controller.CreateTender(new TenderDTO()
            {
                Name = "tender-2",
                EndDate = new DateTime(2021, 5, 5),
                Drugs = new List<TenderDrug>()
                {
                    new TenderDrug() { DrugId = 1, Quantity = 5 }
                }
            });

            Assert.True(context.Tenders.Find(2) != null);
        }

        [Fact]
        public void CloseTender_Valid_Id()
        {
            DbContextInMemory testData = new DbContextInMemory();
            MyDbContext context = testData._context;
            TenderServiceController controller =
                new TenderServiceController(new TenderService(new MySqlTenderRepository(context)),
                                            new TenderMessageService(new MySqlTenderMessageRepository(context)),
                                            new Mock<IRabbitMqTenderingService>().Object);

            controller.CloseTender(1);
            Assert.True(context.Tenders.Find(1).IsClosed);
        }
    }
}
