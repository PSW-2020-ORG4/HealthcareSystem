using Backend.Model.Pharmacies;

namespace IntegrationAdaptersTests.DataFactory
{
    public class CreatePharmacy
    {
        public static PharmacySystem CreateValidTestObject()
        {
            return new PharmacySystem()
            {
                Id = 1,
                Name = "Successful Pharmacy",
                ApiKey = "successfulapikey",
                Url = "http://successful",
                ActionsBenefitsExchangeName = "exchange",
                ActionsBenefitsSubscribed = false,
                GrpcAdress = new GrpcAdress("localhost", 9090)
            };
        }

        public static PharmacySystem CreateInvalidTestObject()
        {
            return new PharmacySystem()
            {
                Name = "Unsuccessful Pharmacy",
                ApiKey = "unsuccessfulapikey"
            };
        }
    }
}
