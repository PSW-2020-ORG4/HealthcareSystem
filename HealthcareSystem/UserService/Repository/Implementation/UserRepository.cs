using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Model;

namespace UserService.Repository
{
    public class UserRepository : IUserRepository
    {
        public UserAccount GetByEmailAndPassword(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
