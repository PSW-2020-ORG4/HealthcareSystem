using AutoMapper;
using IntegrationAdapters.Dtos;
using IntegrationAdapters.Protos;

namespace IntegrationAdapters.MapperProfiles
{
    public class PharmacyId1Profile : Profile
    {
        public PharmacyId1Profile()
        {
            CreateMap<Drug, DrugDto>();
            CreateMap<Pharmacy, PharmacyDto>();
        }
    }
}
