using AutoMapper;
using Backend.Model.Pharmacies;
using IntegrationAdapters.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
