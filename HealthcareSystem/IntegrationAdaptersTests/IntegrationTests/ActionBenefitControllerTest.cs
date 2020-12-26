using Backend.Model;
using Backend.Repository;
using Backend.Service;
using IntegrationAdapters.Controllers;
using Microsoft.AspNetCore.Mvc;
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
            testData.SetActionBenefitControllerTestIntegration();
            MyDbContext context = testData._context;
            ActionBenefitService actionBenefitService = new ActionBenefitService(new MySqlActionBenefitRepository(context), new MySqlPharmacyRepo(context));
            var controller = new ActionBenefitController(actionBenefitService);

            ViewResult result = (ViewResult)controller.Index();

            Assert.IsType<List<ActionBenefit>>(result.Model);
            
        }

        [Fact]
        public void Details_ReturnsActionBenfit_Id_1()
        {
            DbContextInMemory testData = new DbContextInMemory();
            testData.SetActionBenefitControllerTestIntegration();
            MyDbContext context = testData._context;
            ActionBenefitService actionBenefitService = new ActionBenefitService(new MySqlActionBenefitRepository(context), new MySqlPharmacyRepo(context));
            var controller = new ActionBenefitController(actionBenefitService);

            ViewResult result = (ViewResult)controller.Details(1);

            Assert.Equal("m", ((ActionBenefit)result.Model).Message);
        }

        [Fact]
        public void MakePublic_SetsActionBenefit_IsPublic_ToValue()
        {
            DbContextInMemory testData = new DbContextInMemory();
            testData.SetActionBenefitControllerTestIntegration();
            MyDbContext context = testData._context;
            ActionBenefitService actionBenefitService = new ActionBenefitService(new MySqlActionBenefitRepository(context), new MySqlPharmacyRepo(context));
            var controller = new ActionBenefitController(actionBenefitService);

            Assert.False(actionBenefitService.GetActionBenefitById(1).IsPublic);
            controller.MakePublic(1, true);
            Assert.True(actionBenefitService.GetActionBenefitById(1).IsPublic);
        }

        [Fact]
        public void MakePublic_SetsInvalidActionBenefit_IsPublic_ToValue_ThrowException()
        {
            DbContextInMemory testData = new DbContextInMemory();
            testData.SetActionBenefitControllerTestIntegration();
            MyDbContext context = testData._context;
            ActionBenefitService actionBenefitService = new ActionBenefitService(new MySqlActionBenefitRepository(context), new MySqlPharmacyRepo(context));
            var controller = new ActionBenefitController(actionBenefitService);

            Assert.Throws<ArgumentException>(() => controller.MakePublic(5, true));
        }
    }
}
