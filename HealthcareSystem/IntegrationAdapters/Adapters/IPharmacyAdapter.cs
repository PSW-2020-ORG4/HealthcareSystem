using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationAdapters.Adapters
{
    public interface IPharmacyAdapter
    {
        public void CheckMedicine();
        public void Tender();
    }
}
