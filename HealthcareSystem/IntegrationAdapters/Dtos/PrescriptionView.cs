using Backend.Model.Pharmacies;
using Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationAdapters.Dtos
{
    public class PrescriptionView
    {
        public List<Patient> patients { get; set; }
        public IEnumerable<PharmacySystem> pharmacySystems { get; set; }
    }
}
