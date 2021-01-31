using AutoMapper;
using IntegrationAdapters.Apis.Grpc;
using IntegrationAdapters.Dtos;

namespace IntegrationAdapters.MapperProfiles
{
    public class PharmacySystemProfile_Id1 : Profile
    {
        public PharmacySystemProfile_Id1()
        {
            CreateMap<Drug, DrugDto>().ForMember(dest => dest.PharmacyDto, opt => opt.MapFrom(src => src.Pharmacy));
            CreateMap<Pharmacy, PharmacyDto>();
        }
    }
}
