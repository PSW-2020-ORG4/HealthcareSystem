using Model.Users;
using PatientWebApp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientWebApp.Mappers
{
    public class CityMapper
    {
        public static City CityDTOToCity(CityDTO cityDTO)
        {
            City city = new City();
            city.ZipCode = cityDTO.ZipCode;
            city.Name = cityDTO.Name;
            city.CountryId = cityDTO.CountryId;
            return city;
        }

        public static CityDTO CityToCityDTO(City city)
        {
            CityDTO cityDTO = new CityDTO();
            cityDTO.ZipCode = city.ZipCode;
            cityDTO.Name = city.Name;
            cityDTO.CountryId = city.CountryId;
            return cityDTO;
        }
    }
}
