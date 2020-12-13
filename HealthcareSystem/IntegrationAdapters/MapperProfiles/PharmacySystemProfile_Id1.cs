using AutoMapper;
using IntegrationAdapters.Apis.Grpc;
using IntegrationAdapters.Dtos;

namespace IntegrationAdapters.MapperProfiles
{
    public class PharmacySystemProfile_Id1 : Profile
    {
        public PharmacySystemProfile_Id1()
        {
            CreateMap<Drug, DrugDto>();
            CreateMap<Pharmacy, PharmacyDto>();
        }
    }
}
