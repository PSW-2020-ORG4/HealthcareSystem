using AutoMapper;
using Backend.Model.Pharmacies;
using IntegrationAdapters.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationAdapters.Profiles
{
    public class PharmaciesProfile : Profile
    {
        public PharmaciesProfile()
        {
            CreateMap<Pharmacy, PharmacyReadDto>();
            CreateMap<PharmacyCreateDto, Pharmacy>();
            CreateMap<PharmacyUpdateDto, Pharmacy>();
        }
    }
}
