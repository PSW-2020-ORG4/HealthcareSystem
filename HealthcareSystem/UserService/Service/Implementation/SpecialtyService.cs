using System.Collections.Generic;
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
