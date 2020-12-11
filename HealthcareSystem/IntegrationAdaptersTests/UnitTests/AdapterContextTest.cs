using AutoMapper;
using Backend.Model.Pharmacies;
using IntegrationAdapters.Adapters;
using IntegrationAdapters.MapperProfiles;
using Microsoft.Extensions.Http;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace IntegrationAdaptersTests.UnitTests
{
    public class AdapterContextTest
    {
        private readonly IMapper _mapper;

        public AdapterContextTest()
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
            List<Profile> profiles = new List<Profile>()
            {
                new PharmacyId1Profile(),
                new PharmacySystemProfile()
            };
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfiles(profiles));
            _mapper = new Mapper(mapperConfig);
        }
        [Fact]
        public void PharmacySystemAdapter_SetPharmacy1Adapter_ImplementedAdapter()
        {
            AdapterContext adapterContext = new AdapterContext(_mapper);
            PharmacySystem pharmacy = new PharmacySystem { Id = 1, Name = "apoteka-1", ApiKey = "api-1", Url = "url-1", ActionsBenefitsExchangeName = "exchange-1", ActionsBenefitsSubscribed = true, GrpcHost = "localhost", GrpcPort = 30051 };

            adapterContext.SetPharmacySystemAdapter(pharmacy);

            Assert.True(adapterContext.GetPharmacySystemAdapter() != null);
        }

        [Fact]
        public void PharmacySystemAdapter_SetPharmacy1Adapter_UnimplementedAdapter()
        {
            var mapper = new Mock<IMapper>();
            AdapterContext adapterContext = new AdapterContext(_mapper);
            PharmacySystem pharmacy = new PharmacySystem { Id = 2, Name = "apoteka-2", ApiKey = "api-2", Url = "url-2", ActionsBenefitsExchangeName = "exchange-2", ActionsBenefitsSubscribed = true, GrpcHost = "localhost", GrpcPort = 30051 };

            adapterContext.SetPharmacySystemAdapter(pharmacy);

            Assert.True(adapterContext.GetPharmacySystemAdapter() == null);
        }

        [Fact]
        public void PharmacySystemAdapter_SetPharmacy1Adapter_ImplementedAdapter_Production()
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Production");
            AdapterContext adapterContext = new AdapterContext(_mapper);
            PharmacySystem pharmacy = new PharmacySystem { Id = 1, Name = "apoteka-1", ApiKey = "api-1", Url = "url-1", ActionsBenefitsExchangeName = "exchange-1", ActionsBenefitsSubscribed = true, GrpcHost = "localhost", GrpcPort = 30051 };

            adapterContext.SetPharmacySystemAdapter(pharmacy);

            Assert.True(adapterContext.GetPharmacySystemAdapter() != null);
        }
    }
}
