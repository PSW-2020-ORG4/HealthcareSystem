using Backend.Model;
using Backend.Model.Exceptions;
using Backend.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Repository.SpecialtyRepository.MySqlSpecialtyRepository
{
    public class MySqlSpecialtyRepository : ISpecialtyRepository
    {
        private readonly MyDbContext _context;
        public MySqlSpecialtyRepository(MyDbContext context)
        {
            _context = context;
        }
        public List<Specialty> GetSpecialties()
        {
            try
            {
                return _context.Specialties.ToList();
            }
            catch (Exception)
            {
                throw new DatabaseException("The database connection is down.");
            }

        }
    }
}
