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
        private readonly Backend.Repository.SpecialtyRepository.ISpecialtyRepository _repository;

        public SpecialtyRepository(Backend.Repository.SpecialtyRepository.ISpecialtyRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Specialty> GetAll()
        {
            try
            {
                return _repository.GetSpecialties().Select(s => new Specialty(s.Id, s.Name));
            }
            catch (Exception e)
            {
                throw new DataStorageException(e.Message);
            }
        }
    }
}
