using Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using UserService.CustomException;
using UserService.Model;

namespace UserService.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly MyDbContext _context;

        public CountryRepository(MyDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Country> GetAll()
        {
            try
            {
                return _context.Countries.Select(c => new Country(c.Id, c.Name));
            }
            catch (Exception e)
            {
                throw new DataStorageException(e.Message);
            }
        }
    }
}
