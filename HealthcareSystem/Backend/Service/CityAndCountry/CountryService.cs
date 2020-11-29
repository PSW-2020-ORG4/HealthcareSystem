using Backend.Model.Exceptions;
using Backend.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Service
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;
        public CountryService(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }
        public List<Country> GetCountries()
        {
            try
            {
                return _countryRepository.GetCountries();
            }
            catch (Exception)
            {
                throw new NotFoundException("There is no countries in database.");
            }
        }
    }
}
