using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Model;
using UserService.Repository;

namespace UserService.Service
{
    public class GeographicalService : IGeographicalService
    {
        private ICityRepository _cityRepository;
        private ICountryRepository _countryRepository;
        public IEnumerable<City> GetAllCities()
        {
            return _cityRepository.GetAll();
        }

        public IEnumerable<Country> GetAllCountries()
        {
            return _countryRepository.GetAll();
        }
    }
}
