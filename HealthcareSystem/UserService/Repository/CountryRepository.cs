using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.CustomException;
using UserService.Model;

namespace UserService.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly Backend.Repository.ICountryRepository _repository;

        public CountryRepository(Backend.Repository.ICountryRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Country> GetAll()
        {
            try
            {
                return _repository.GetCountries().Select(c => new Country(c.Id, c.Name));
            }
            catch (Exception e)
            {
                throw new DataStorageException(e.Message);
            }
        }
    }
}
