using Backend.Model;
using Backend.Model.DTO;
using Backend.Repository;
using Backend.Repository.DrugInRoomRepository.MySqlDrugInRoomRepository;
using Backend.Repository.DrugRepository.MySQLDrugRepository;
using Backend.Repository.TenderRepository.MySqlTenderRepository;
using Backend.Service;
using IntegrationAdapters.Controllers;
using IntegrationAdapters.Dtos;
using Moq;
using Service.DrugAndTherapy;
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
            var tenderingService = new Mock<IRabbitMqTenderingService>();
            TenderController controller = 
                new TenderController(new TenderService(new MySqlTenderRepository(context)),
                                                       new TenderMessageService(new MySqlTenderMessageRepository(context)),
                                                       tenderingService.Object,
                                                       new DrugService(new MySqlConfirmedDrugRepository(context),
                                                                       new MySqlUnconfirmedDrugRepository(context),
                                                                       new MySqlDrugInRoomRepository(context)));

            controller.PushDrugList(new NewTenderView());

            Assert.True(context.Tenders.Find(2) == null);
        }

        [Fact]
        public void PushDrugList_Valid_List()
        {
            DbContextInMemory testData = new DbContextInMemory();
            MyDbContext context = testData._context;
            var tenderingService = new Mock<IRabbitMqTenderingService>();
            TenderController controller =
                new TenderController(new TenderService(new MySqlTenderRepository(context)),
                                                       new TenderMessageService(new MySqlTenderMessageRepository(context)),
                                                       tenderingService.Object,
                                                       new DrugService(new MySqlConfirmedDrugRepository(context),
                                                                       new MySqlUnconfirmedDrugRepository(context),
                                                                       new MySqlDrugInRoomRepository(context)));

            controller.PushDrugList(new NewTenderView() { TenderName = "tender-2",
                                                          EndDate = new DateTime(2021, 5, 5),
                                                          AddedDrugs = new List<TenderDrugDTO>() 
                                                          { 
                                                              new TenderDrugDTO() { Id = 1, Name = "Brufen", Quantity = 5 } 
                                                          }});

            Assert.True(context.Tenders.Find(2) != null);
        }

        [Fact]
        public void CloseTender_Valid_Id()
        {
            DbContextInMemory testData = new DbContextInMemory();
            MyDbContext context = testData._context;
            var tenderingService = new Mock<IRabbitMqTenderingService>();
            TenderController controller =
                new TenderController(new TenderService(new MySqlTenderRepository(context)),
                                                       new TenderMessageService(new MySqlTenderMessageRepository(context)),
                                                       tenderingService.Object,
                                                       new DrugService(new MySqlConfirmedDrugRepository(context),
                                                                       new MySqlUnconfirmedDrugRepository(context),
                                                                       new MySqlDrugInRoomRepository(context)));

            controller.CloseTender(1);
            Assert.True(context.Tenders.Find(1).IsClosed);
        }
    }
}
