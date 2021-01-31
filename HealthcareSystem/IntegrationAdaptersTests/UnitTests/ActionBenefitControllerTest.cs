using Backend.Model;
using Backend.Model.Pharmacies;
using IntegrationAdapters.Controllers;
using IntegrationAdapters.MicroserviceComunicator;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationAdaptersTests.UnitTests
{
    public class ActionBenefitControllerTest
    {
        [Fact]
        public async void Index_ReturnsSingleItem()
        {
            var mockaActionBenefitService = new Mock<IActionBenefitService>();
            var pharmacy = new PharmacySystem { Id = 1, Name = "a1", ApiKey = "a1", Url = "u1", ActionsBenefitsExchangeName = "e1", ActionsBenefitsSubscribed = true };
            var actionsBenefits = new List<ActionBenefit>
             {
                 new ActionBenefit { Id=1, PharmacyId=1, Pharmacy=pharmacy, Message = new ActionBenefitMessage("s", "m"), IsPublic=true}
             };
            mockaActionBenefitService.Setup(r => r.GetAll()).Returns(Task.Run(() => actionsBenefits));
            var controller = new ActionBenefitController(mockaActionBenefitService.Object);

            ViewResult result = (ViewResult)await controller.Index();

            Assert.True(((List<ActionBenefit>)result.Model).Count == 1);
        }

        [Fact]
        public async void MakePublic_CallActionBenefitServiceMakePublic()
        {
            var mockaActionBenefitService = new Mock<IActionBenefitService>();
            mockaActionBenefitService.Setup(ab => ab.SetPublic(It.IsAny<int>(), It.Is<bool>(isPublic => isPublic == true))).Verifiable();
            var controller = new ActionBenefitController(mockaActionBenefitService.Object);

            await controller.MakePublic(5, true);

            mockaActionBenefitService.Verify();
        }

        [Fact]
        public async void MakePublic_CallActionBenefitServiceMakePublic_ThrowsArgumentException()
        {
            var mockaActionBenefitService = new Mock<IActionBenefitService>();
            mockaActionBenefitService.Setup(ab => ab.SetPublic(It.IsAny<int>(), It.IsAny<bool>())).Throws(new ArgumentException());
            var controller = new ActionBenefitController(mockaActionBenefitService.Object);

            await Assert.ThrowsAsync<ArgumentException>(() => controller.MakePublic(5, true));
        }
    }
}
