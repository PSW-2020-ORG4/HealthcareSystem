using UserService.Model;

namespace UserService.Service
{
    public interface IUserService
    {
        UserAccount GetByEmailAndPassword(string email, string password);
    }
}
