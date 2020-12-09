using AutoMapper;
using Backend.Model.Pharmacies;
using IntegrationAdapters.Adapters;
using Moq;
using Xunit;

namespace IntegrationAdaptersTests.UnitTests
{
    public class AdapterContextTest
    {
        [Fact]
        public void PharmacySystemAdapter_SetPharmacy1Adapter_ImplementedAdapter()
        {
            var mapper = new Mock<IMapper>();
            AdapterContext adapterContext = new AdapterContext(mapper.Object);
            PharmacySystem pharmacy = new PharmacySystem { Id = 1, Name = "apoteka-1", ApiKey = "api-1", Url = "url-1", ActionsBenefitsExchangeName = "exchange-1", ActionsBenefitsSubscribed = true, grpcHost = "localhost", grpcPort = 30051};

            adapterContext.SetPharmacySystemAdapter(pharmacy);

            Assert.True(adapterContext.GetPharmacySystemAdapter() != null);
        }

        [Fact]
        public void PharmacySystemAdapter_SetPharmacy1Adapter_UnimplementedAdapter()
        {
            var mapper = new Mock<IMapper>();
            AdapterContext adapterContext = new AdapterContext(mapper.Object);
            PharmacySystem pharmacy = new PharmacySystem { Id = 2, Name = "apoteka-2", ApiKey = "api-2", Url = "url-2", ActionsBenefitsExchangeName = "exchange-2", ActionsBenefitsSubscribed = true, grpcHost = "localhost", grpcPort = 30051 };

            adapterContext.SetPharmacySystemAdapter(pharmacy);

            Assert.True(adapterContext.GetPharmacySystemAdapter() == null);
        }
    }
}
