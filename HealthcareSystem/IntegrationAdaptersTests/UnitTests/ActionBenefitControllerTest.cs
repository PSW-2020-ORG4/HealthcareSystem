using Backend.Model;
using Backend.Model.Pharmacies;
using Backend.Service;
using IntegrationAdapters.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace IntegrationAdaptersTests.UnitTests
{
    public class ActionBenefitControllerTest
    {
        [Fact]
        public void Index_ReturnsSingleItem()
        {
            var mockaActionBenefitService = new Mock<IActionBenefitService>();
            var pharmacy = new PharmacySystem { Id=1, Name="a1", ApiKey="a1", Url="u1", ActionsBenefitsExchangeName="e1", ActionsBenefitsSubscribed=true };
            var actionsBenefits = new List<ActionBenefit>
            {
                new ActionBenefit { Id=1, PharmacyId=1, Pharmacy=pharmacy, Subject="s", Message="m", IsPublic=true}
            };
            mockaActionBenefitService.Setup(r => r.GetAllActionsBenefits()).Returns(actionsBenefits);
            var controller = new ActionBenefitController(mockaActionBenefitService.Object);

            ViewResult result = (ViewResult)controller.Index();

            Assert.True(((List<ActionBenefit>)result.Model).Count == 1);
        }

        [Fact]
        public void MakePublic_CallActionBenefitServiceMakePublic()
        {
            var mockaActionBenefitService = new Mock<IActionBenefitService>();
            mockaActionBenefitService.Setup(ab => ab.MakePublic(It.IsAny<int>(), It.Is<bool>(isPublic => isPublic == true))).Verifiable();
            var controller = new ActionBenefitController(mockaActionBenefitService.Object);

            controller.MakePublic(5, true);

            mockaActionBenefitService.Verify();
        }

        [Fact]
        public void MakePublic_CallActionBenefitServiceMakePublic_ThrowsArgumentException()
        {
            var mockaActionBenefitService = new Mock<IActionBenefitService>();
            mockaActionBenefitService.Setup(ab => ab.MakePublic(It.IsAny<int>(), It.IsAny<bool>())).Throws(new ArgumentException());
            var controller = new ActionBenefitController(mockaActionBenefitService.Object);

            Assert.Throws<ArgumentException>(() => controller.MakePublic(5, true));
        }
    }
}
