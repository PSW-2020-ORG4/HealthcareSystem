using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Model;

namespace UserService.Service
{
    public interface IUserService
    {
        UserAccount GetByEmailAndPassword(string Email, string Password);
    }
}
