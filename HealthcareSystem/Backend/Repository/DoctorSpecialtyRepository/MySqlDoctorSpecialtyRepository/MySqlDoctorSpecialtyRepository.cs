using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Backend.Model;
using Backend.Model.Exceptions;
using Backend.Model.Users;

namespace Backend.Repository.DoctorSpecialtyRepository.MySqlDoctorSpecialtyRepository
{
    public class MySqlDoctorSpecialtyRepository : IDoctorSpecialtyRepository
    {
        private readonly MyDbContext _context;
        public MySqlDoctorSpecialtyRepository(MyDbContext context)
        {
            _context = context;
        }
        public List<DoctorSpecialty> GetDoctorSpecialtyBySpecialtyId(int id)
        {
            try
            {
                return _context.DoctorSpecialties.Where(ds => ds.SpecialtyId == id).ToList();
            }
            catch (Exception)
            {
                throw new DatabaseException("The database connection is down.");
            }
        }

    }
}
