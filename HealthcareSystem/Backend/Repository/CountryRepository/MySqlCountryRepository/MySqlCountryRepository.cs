using Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Repository
{
    public class MySqlCountryRepository : ICountryRepository
    {
        private readonly MyDbContext _context;
        public MySqlCountryRepository(MyDbContext context)
        {
            _context = context;
        }
        public List<Country> GetCountries()
        {
            return _context.Countries.ToList();
        }
    }
}
