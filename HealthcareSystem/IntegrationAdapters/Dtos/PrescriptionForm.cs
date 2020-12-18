using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationAdapters.Dtos
{
    public class PrescriptionForm
    {
        public string Patient { get; set; }
        public int PharmacySystem { get; set; }
        public int Drug { get; set; } = 0;
    }
}
