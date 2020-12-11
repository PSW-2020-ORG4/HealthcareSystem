using Backend.Model.Pharmacies;

namespace IntegrationAdaptersTests.DataFactory
{
    public class CreatePharmacy
    {
        public static PharmacySystem CreateValidTestObject()
        {
            return new PharmacySystem()
            {
                Name = "Successful Pharmacy",
                ApiKey = "successfulapikey",
                Url = "http://successful"
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
