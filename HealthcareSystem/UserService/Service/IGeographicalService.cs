using System.Collections.Generic;
using UserService.Model;

namespace UserService.Service
{
    public interface IGeographicalService
    {
        IEnumerable<Country> GetAllCountries();
        IEnumerable<City> GetCitiesByCountry(int countryId);
    }
}
