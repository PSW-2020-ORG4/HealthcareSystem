using AutoMapper;
using IntegrationAdapters.Dtos;
using IntegrationAdapters.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationAdapters.MapperProfiles
{
    public class Pharmacy_ID1_Profile : Profile
    {
        public Pharmacy_ID1_Profile()
        {
            CreateMap<Drug, DrugDTO>();
            CreateMap<Pharmacy, PharmacyDTO>();
        }
    }
}
