using Backend.Model;
using Backend.Model.Exceptions;
using Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class MySqlAdminRepository : IAdminRepository
    {
        private readonly MyDbContext _context;
        public MySqlAdminRepository(MyDbContext context)
        {
            _context = context;
        }
        public Admin GetAdminByUsernameAndPassword(string username, string password)
        {
            Admin admin;
            try
            {
                admin = _context.Admins.Where(a => a.Username == username && a.Password == password).FirstOrDefault();
            }
            catch (Exception)
            {
                throw new DatabaseException("The database connection is down.");
            }
            if (admin == null)
                throw new NotFoundException("Admin doesn't exist in database.");
            return admin;
        }
    }
}
