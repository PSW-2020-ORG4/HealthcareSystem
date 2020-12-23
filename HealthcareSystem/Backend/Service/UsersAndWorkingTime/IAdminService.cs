using Model.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Service
{
    public interface IAdminService
    {
        public Admin GetAdminByUsernameAndPassword(string username, string password);
    }
}
