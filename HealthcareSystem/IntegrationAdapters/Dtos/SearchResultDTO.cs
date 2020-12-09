using Backend.Model.Pharmacies;
using System.Collections.Generic;

namespace IntegrationAdapters.Dtos
{
    public class SearchResultDto
    {
        public PharmacySystem pharmacySystem { get; set; }
        public List<DrugDto> drugs { get; set; }
    }
}
