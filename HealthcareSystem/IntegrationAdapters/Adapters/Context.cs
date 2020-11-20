using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationAdapters.Adapters
{
    public class Context
    {
        public IPharmacyAdapter _iPharmacyAdapter { get; set; }

        public Context(IPharmacyAdapter iPharmacy)
        {
            _iPharmacyAdapter = iPharmacy;
        }

        public void CheckMedicine()
        {
            _iPharmacyAdapter.CheckMedicine();
        }

        public void Tender()
        {
            _iPharmacyAdapter.Tender();
        }
    }
}
