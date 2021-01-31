using System.Collections.Generic;
using UserService.Model;
using UserService.Repository.CRUD;

namespace UserService.Repository
{
    public interface ICityRepository : IRead<City, int>
    {
        IEnumerable<City> GetByCountry(int countryId);

    }
}
