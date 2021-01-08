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
    public class SpecialtyRepository : ISpecialtyRepository
    {
        private readonly MyDbContext _context;

        public SpecialtyRepository(MyDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Specialty> GetAll()
        {
            try
            {
                return _context.Specialties.Select(s => new Specialty(s.Id, s.Name));
            }
            catch (Exception e)
            {
                throw new DataStorageException(e.Message);
            }
        }
    }
}
