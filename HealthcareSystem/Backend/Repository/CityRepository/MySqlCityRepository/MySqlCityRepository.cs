using Backend.Model;
using Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Repository
{
    public class MySqlCityRepository : ICityRepository
    {
        private readonly MyDbContext _context;
        public MySqlCityRepository(MyDbContext context)
        {
            _context = context;
        }
        public List<City> GetCities()
        {
            return _context.Cities.ToList();
        }

        public List<City> GetCitiesByCountryId(int countryId)
        {
            return _context.Cities.Where(city => city.CountryId == countryId).ToList();
        }
    }
}
