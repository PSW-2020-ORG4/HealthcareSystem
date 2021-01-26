using UserService.Model;

namespace UserService.Repository
{
    public interface IUserRepository
    {
        UserAccount GetByEmailAndPassword(string email, string password);
    }
}
