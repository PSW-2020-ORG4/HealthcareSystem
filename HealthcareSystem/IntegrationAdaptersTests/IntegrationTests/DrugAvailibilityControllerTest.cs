﻿using Backend.Model;
using Backend.Repository;
using Backend.Service.Pharmacies;
using IntegrationAdapters.Adapters;
using IntegrationAdapters.Controllers;
using IntegrationAdapters.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace IntegrationAdaptersTests.IntegrationTests
{
    public class DrugAvailabilityControllerTest
    {
        [Fact]
        public void Search_ForDrug_Found_And_NotFound()
        {
            DbContextInMemory testData = new DbContextInMemory();
            testData.setDrugAvailibilityControllerTestIntegration();
            MyDbContext context = testData._context;

            IPharmacyService pharmacyService = new PharmacyService(new MySqlPharmacyRepo(context));
            var adapterContext = new Mock<IAdapterContext>();
            adapterContext.Setup(c => c.GetPharmacySystemAdapter().DrugAvailibility(It.Is<string>(name => name == "droga"))).Returns(
                new List<DrugDto>() {
                    new DrugDto() { Id = 1, Name = "droga", Quantity = 5, Pharmacy = new PharmacyDto {Id = 1, Name = "lokacija-1" } },
                    new DrugDto() { Id = 4, Name = "drogaricin", Quantity = 5, Pharmacy = new PharmacyDto {Id = 3, Name = "lokacija-3" } }
                }
                );
            DrugAvailabilityController controller = new DrugAvailabilityController(adapterContext.Object, pharmacyService);

            ViewResult result = (ViewResult)controller.Search("droga");
            Assert.NotEmpty((IEnumerable<SearchResultDto>)result.Model);

            result = (ViewResult)controller.Search("nesto");
            Assert.Empty((IEnumerable<SearchResultDto>)result.Model);
        }
    }
}
