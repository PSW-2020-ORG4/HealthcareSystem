using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Model;
using UserService.Repository;

namespace UserService.Service.Implementation
{
    public class UserServiceClass : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserServiceClass(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserAccount GetByEmailAndPassword(string email, string password)
        {
            return _userRepository.GetByEmailAndPassword(email, password);
        }
    }
}
