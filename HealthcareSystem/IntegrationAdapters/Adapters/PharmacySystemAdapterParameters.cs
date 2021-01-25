using Backend.Model.Pharmacies;

namespace IntegrationAdapters.Adapters
{
    public class PharmacySystemAdapterParameters
    {
        public string ApiKey { get; set; }
        public GrpcAdress GrpcAdress { get; set; }
        public string Url { get; set; }
        public string HospitalName { get; set; }
        public SftpConfig SftpConfig {get; set;}
    }
}
