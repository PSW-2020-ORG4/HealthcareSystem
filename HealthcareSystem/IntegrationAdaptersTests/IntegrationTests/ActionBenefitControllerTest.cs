using Backend.Model;
using IntegrationAdaptersActionBenefitService.Controllers;
using IntegrationAdaptersActionBenefitService.DTO;
using IntegrationAdaptersActionBenefitService.Repository;
using IntegrationAdaptersActionBenefitService.Service;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace IntegrationAdaptersTests.IntegrationTests
{
    public class ActionBenefitControllerTest
    {
        [Fact]
        public void Index_ReturnsSingleItem()
        {
            DbContextInMemory testData = new DbContextInMemory();
            MyDbContext context = testData._context;
            ActionBenefitService actionBenefitService = new ActionBenefitService(new MySqlActionBenefitRepository(context), new MySqlPharmacyRepo(context));
            var rabbitServiceMock = new Mock<IRabbitMqActionBenefitService>();
            var controller = new ActionBenefitServiceController(actionBenefitService, rabbitServiceMock.Object);

            var result = controller.GetAll() as OkObjectResult;

            Assert.IsType<List<ActionBenefit>>(result.Value);

        }

        [Fact]
        public void Details_ReturnsActionBenfit_Id_1()
        {
            DbContextInMemory testData = new DbContextInMemory();
            MyDbContext context = testData._context;
            ActionBenefitService actionBenefitService = new ActionBenefitService(new MySqlActionBenefitRepository(context), new MySqlPharmacyRepo(context));
            var rabbitServiceMock = new Mock<IRabbitMqActionBenefitService>();
            var controller = new ActionBenefitServiceController(actionBenefitService, rabbitServiceMock.Object);

            var result = controller.Get(1) as OkObjectResult;

            Assert.Equal("m", ((ActionBenefit)result.Value).Message.Message);
        }

        [Fact]
        public void MakePublic_SetsActionBenefit_IsPublic_ToValue()
        {
            DbContextInMemory testData = new DbContextInMemory();
            MyDbContext context = testData._context;
            ActionBenefitService actionBenefitService = new ActionBenefitService(new MySqlActionBenefitRepository(context), new MySqlPharmacyRepo(context));
            var rabbitServiceMock = new Mock<IRabbitMqActionBenefitService>();
            var controller = new ActionBenefitServiceController(actionBenefitService, rabbitServiceMock.Object);

            Assert.False(actionBenefitService.GetActionBenefitById(1).IsPublic);
            controller.SetPublic(new SetPublicRequest() { Id = 1, IsPublic = true });
            Assert.True(actionBenefitService.GetActionBenefitById(1).IsPublic);
        }

        [Fact]
        public void MakePublic_SetsInvalidActionBenefit_IsPublic_ToValue_ThrowException()
        {
            DbContextInMemory testData = new DbContextInMemory();
            MyDbContext context = testData._context;
            ActionBenefitService actionBenefitService = new ActionBenefitService(new MySqlActionBenefitRepository(context), new MySqlPharmacyRepo(context));
            var rabbitServiceMock = new Mock<IRabbitMqActionBenefitService>();
            var controller = new ActionBenefitServiceController(actionBenefitService, rabbitServiceMock.Object);

            Assert.Throws<ArgumentException>(() => controller.SetPublic(new SetPublicRequest() { Id = 5, IsPublic = true }));
        }
    }
}
