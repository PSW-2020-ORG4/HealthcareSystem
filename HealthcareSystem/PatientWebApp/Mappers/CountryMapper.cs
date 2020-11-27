using Backend;
using PatientWebApp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientWebApp.Mappers
{
    public class CountryMapper
    {
        public static Country CountryDTOToCountry(CountryDTO countryDTO)
        {
            Country country = new Country();
            country.Id = countryDTO.Id;
            country.Name = countryDTO.Name;
            return country;
        }

        public static CountryDTO CountryToCountryDTO(Country country)
        {
            CountryDTO countryDTO = new CountryDTO();
            countryDTO.Id = country.Id;
            countryDTO.Name = country.Name;
            return countryDTO;
        }
    }
}
