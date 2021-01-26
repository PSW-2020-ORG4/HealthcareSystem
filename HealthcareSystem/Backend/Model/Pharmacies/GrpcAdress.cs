using Newtonsoft.Json;
using System.Collections.Generic;

namespace Backend.Model.Pharmacies
{
    public class GrpcAdress : ValueObject
    {
        public string GrpcHost { get; private set; }
        public int GrpcPort { get; private set; } = -1;
        public GrpcAdress() { }

        [JsonConstructor]
        public GrpcAdress(string grpcHost, int grpcPort)
        {
            GrpcHost = grpcHost;
            GrpcPort = grpcPort;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return GrpcHost;
            yield return GrpcPort;
        }
    }
}
