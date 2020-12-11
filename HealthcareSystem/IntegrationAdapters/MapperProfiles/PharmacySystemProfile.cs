using AutoMapper;
using Backend.Model.Pharmacies;
using IntegrationAdapters.Adapters;

namespace IntegrationAdapters.MapperProfiles
{
    public class PharmacySystemProfile : Profile
    {
        public PharmacySystemProfile()
        {
            CreateMap<PharmacySystem, PharmacySystemAdapterParameters>();
        }
    }
}
