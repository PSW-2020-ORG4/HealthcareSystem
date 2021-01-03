using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.CustomException;
using UserService.Model;

namespace UserService.Repository
{
    public class CityRepository : ICityRepository
    {
        private Backend.Repository.ICityRepository _repository;

        public CityRepository(Backend.Repository.ICityRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<City> GetByCountry(int countryId)
        {
            try
            {
                return _repository.GetCitiesByCountryId(countryId).Select(
                    c => new City(c.ZipCode, c.Name, c.Country.Id, c.Country.Name));
            }
            catch (Exception e)
            {
                throw new DataStorageException(e.Message);
            }
        }
    }
}
