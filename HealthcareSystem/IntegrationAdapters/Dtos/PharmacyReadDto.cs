using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationAdapters.Dtos
{
    public class PharmacyReadDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ApiKey { get; set; }

        public String ActionsBenefitsExchangeName { get; set; }

        public bool ActionsBenefitsSubscribed { get; set; }
    }
}
