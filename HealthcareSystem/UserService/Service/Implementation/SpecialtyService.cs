using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Model;
using UserService.Repository;

namespace UserService.Service
{
    public class SpecialtyService : ISpecialtyService
    {
        private readonly ISpecialtyRepository _specialtyRepository;

        public SpecialtyService(ISpecialtyRepository specialtyRepository)
        {
            _specialtyRepository = specialtyRepository;
        }

        public IEnumerable<Specialty> GetAll()
        {
            return _specialtyRepository.GetAll();
        }
    }
}
