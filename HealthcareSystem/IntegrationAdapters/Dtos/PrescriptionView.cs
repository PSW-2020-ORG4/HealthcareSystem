using Backend.Model.Pharmacies;
using System.Collections.Generic;

namespace IntegrationAdapters.Dtos
{
    public class PrescriptionView
    {
        public List<PatientDto> patients { get; set; }
        public IEnumerable<PharmacySystem> pharmacySystems { get; set; }
    }
}
