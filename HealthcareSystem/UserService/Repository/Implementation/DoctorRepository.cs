using Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.CustomException;
using UserService.Model;
using UserService.Model.Memento;

namespace UserService.Repository
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly MyDbContext _context;

        public DoctorRepository(MyDbContext context)
        {
            _context = context;
        }

        public IEnumerable<DoctorAccount> GetBySpecialty(int specialtyId)
        {
            try
            {
                return _context.Doctors.Where(
                    d => d.DoctorSpecialties.Any(s => s.SpecialtyId == specialtyId)).Select(
                    d => d.ToDoctorAccount());
            }
            catch (Exception e)
            {
                throw new DataStorageException(e.Message);
            }
        }

        public IEnumerable<DoctorAccount> GetAll()
        {
            try
            {
                return _context.Doctors.Select(d => d.ToDoctorAccount());
            }
            catch (Exception e)
            {
                throw new DataStorageException(e.Message);
            }
        }
    }
}
