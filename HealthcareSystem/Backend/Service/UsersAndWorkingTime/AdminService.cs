using Model.Users;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Service
{
    public class AdminService : IAdminService
    {
        private IAdminRepository _adminRepository;
        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }
        public Admin GetAdminByUsernameAndPassword(string username, string password)
        {
            return _adminRepository.GetAdminByUsernameAndPassword(username, password);
        }
    }
}
