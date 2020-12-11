using AutoMapper;
using Backend.Model.Pharmacies;
using IntegrationAdapters.Adapters;
using IntegrationAdapters.MapperProfiles;
using Microsoft.Extensions.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Xunit;

namespace IntegrationAdaptersTests.UnitTests
{
    public class AdapterContextTest
    {
        public AdapterContextTest()
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
        }
        [Fact]
        public void PharmacySystemAdapter_SetPharmacy1Adapter_ImplementedAdapter()
        {
            var mockFactory = new Mock<IHttpClientFactory>();
            AdapterContext adapterContext = new AdapterContext(mockFactory.Object);
            PharmacySystem pharmacy = new PharmacySystem { Id = 1, Name = "apoteka-1", ApiKey = "api-1", Url = "url-1", ActionsBenefitsExchangeName = "exchange-1", ActionsBenefitsSubscribed = true, GrpcHost = "localhost", GrpcPort = 30051 };

            adapterContext.SetPharmacySystemAdapter(pharmacy);

            Assert.True(adapterContext.PharmacySystemAdapter != null);
        }

        [Fact]
        public void PharmacySystemAdapter_SetPharmacy1Adapter_UnimplementedAdapter()
        {
            var mockFactory = new Mock<IHttpClientFactory>();
            AdapterContext adapterContext = new AdapterContext(mockFactory.Object);
            PharmacySystem pharmacy = new PharmacySystem { Id = 2, Name = "apoteka-2", ApiKey = "api-2", Url = "url-2", ActionsBenefitsExchangeName = "exchange-2", ActionsBenefitsSubscribed = true, GrpcHost = "localhost", GrpcPort = 30051 };

            adapterContext.SetPharmacySystemAdapter(pharmacy);

            Assert.True(adapterContext.PharmacySystemAdapter == null);
        }

        [Fact]
        public void PharmacySystemAdapter_SetPharmacy1Adapter_ImplementedAdapter_Production()
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Production");
            var mockFactory = new Mock<IHttpClientFactory>();
            AdapterContext adapterContext = new AdapterContext(mockFactory.Object);
            PharmacySystem pharmacy = new PharmacySystem { Id = 1, Name = "apoteka-1", ApiKey = "api-1", Url = "url-1", ActionsBenefitsExchangeName = "exchange-1", ActionsBenefitsSubscribed = true, GrpcHost = "localhost", GrpcPort = 30051 };

            adapterContext.SetPharmacySystemAdapter(pharmacy);

            Assert.True(adapterContext.PharmacySystemAdapter != null);
        }
    }
}
