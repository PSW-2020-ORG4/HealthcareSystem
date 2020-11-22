using Backend.Model.Exceptions;
using Backend.Repository;
using Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Service
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;
        public CityService(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }
        public List<City> GetCities()
        {
            return _cityRepository.GetCities();
        }

        public List<City> GetCitiesByCountryId(int countryId)
        {
            try
            {
                return _cityRepository.GetCitiesByCountryId(countryId);
            }
            catch (Exception)
            {
                throw new NotFoundException("Country with id=" + countryId + " still has no cities.");
            }
        }
    }
}
