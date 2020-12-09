using Backend.Model.Pharmacies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationAdapters.Dtos
{
    public class SearchResultDTO
    {
        public PharmacySystem pharmacySystem { get; set; }
        public List<DrugDTO> drugs { get; set; }
    }
}
