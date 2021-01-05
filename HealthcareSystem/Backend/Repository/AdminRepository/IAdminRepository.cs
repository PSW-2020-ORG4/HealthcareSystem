using Model.Users;

namespace Repository
{
    public interface IAdminRepository
    {
        public Admin GetAdminByUsernameAndPassword(string username, string password);
    }
}

