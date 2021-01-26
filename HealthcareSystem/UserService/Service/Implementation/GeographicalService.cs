using System.Collections.Generic;
using UserService.Model;
using UserService.Repository;

namespace UserService.Service
{
    public class GeographicalService : IGeographicalService
    {
        private readonly ICityRepository _cityRepository;
        private readonly ICountryRepository _countryRepository;

        public GeographicalService(ICityRepository cityRepository, ICountryRepository countryRepository)
        {
            _cityRepository = cityRepository;
            _countryRepository = countryRepository;
        }

        public IEnumerable<Country> GetAllCountries()
        {
            return _countryRepository.GetAll();
        }

        public IEnumerable<City> GetCitiesByCountry(int countryId)
        {
            return _cityRepository.GetByCountry(countryId);
        }
    }
}
