using System.Collections.Generic;
using UserService.Model;
using UserService.Repository;

namespace UserService.Service
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorService(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public IEnumerable<DoctorAccount> GetBySpecialty(int specialtyId)
        {
            return _doctorRepository.GetBySpecialty(specialtyId);
        }

        public IEnumerable<DoctorAccount> GetAll()
        {
            return _doctorRepository.GetAll();
        }
    }
}
