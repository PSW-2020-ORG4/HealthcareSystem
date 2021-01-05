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
        private MyDbContext _context;

        public CityRepository(MyDbContext context)
        {
            _context = context;
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
