using UserService.Model;
using UserService.Repository.CRUD;

namespace UserService.Repository
{
    public interface ICountryRepository : IReadCollection<Country>
    {
    }
}
