using Backend.Model;
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
        private readonly MyDbContext _context;

        public CityRepository(MyDbContext context)
        {
            _context = context;
        }

        public City Get(int id)
        {
            try
            {
                var city = _context.Cities.Find(id);
                if (city is null)
                    throw new NotFoundException("City with the id " + id + " does not exist.");
                return new City(city.ZipCode, city.Name, city.CountryId, city.Country.Name);
            }
            catch (UserServiceException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new DataStorageException(e.Message);
            }
        }

        public IEnumerable<City> GetAll()
        {
            try
            {
                return _context.Cities.Select(
                    c => new City(c.ZipCode, c.Name, c.Country.Id, c.Country.Name));
            }
            catch (Exception e)
            {
                throw new DataStorageException(e.Message);
            }
        }

        public IEnumerable<City> GetByCountry(int countryId)
        {
            try
            {
                return _context.Cities.Where(
                    c => c.CountryId == countryId).Select(
                    c => new City(c.ZipCode, c.Name, c.Country.Id, c.Country.Name));
            }
            catch (Exception e)
            {
                throw new DataStorageException(e.Message);
            }
        }
    }
}
