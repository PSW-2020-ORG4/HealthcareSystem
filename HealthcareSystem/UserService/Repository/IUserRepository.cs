using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Model;

namespace UserService.Repository
{
    public interface IUserRepository
    {
        UserAccount GetByEmailAndPassword(string email, string password);
    }
}
