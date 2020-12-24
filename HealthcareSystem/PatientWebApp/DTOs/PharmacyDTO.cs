using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientWebApp.DTOs
{
    public class PharmacyDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ApiKey { get; set; }
        public string Url { get; set; }
        public string ActionsBenefitsExchangeName { get; set; }
        public bool ActionsBenefitsSubscribed { get; set; } = false;
        public string GrpcHost { get; set; }
        public int GrpcPort { get; set; }

        public PharmacyDTO() { }
    }
}
