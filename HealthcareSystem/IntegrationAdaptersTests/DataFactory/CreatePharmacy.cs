using Backend.Model.Pharmacies;

namespace IntegrationAdaptersTests.DataFactory
{
    public class CreatePharmacy
    {
        public static Pharmacy CreateValidTestObject()
        {
            return new Pharmacy()
            {
                Name = "Successful Pharmacy",
                ApiKey = "successfulapikey",
                Url = "http://successful"
            };
        }

        public static Pharmacy CreateInvalidTestObject()
        {
            return new Pharmacy()
            {
                Name = "Unsuccessful Pharmacy",
                ApiKey = "unsuccessfulapikey"
            };
        }
    }
}
