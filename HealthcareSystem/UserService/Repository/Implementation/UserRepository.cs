using Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.CustomException;
using UserService.Model;
using UserService.Model.Memento;

namespace UserService.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly MyDbContext _context;

        public UserRepository(MyDbContext context)
        {
            _context = context;
        }

        public UserAccount GetByEmailAndPassword(string email, string password)
        {
            try
            {
                UserAccount user = null;
                user = GetPatient(email, password);
                if (user != null)
                    return user;
                user = GetDoctor(email, password);
                if (user != null)
                    return user;
                user = GetAdmin(email, password);
                if (user != null)
                    return user;
                throw new NotFoundException("User with selected email and password not found.");
            }
            catch (UserServiceException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new DataStorageException(e.Message);
            }
        }

        private PatientAccount GetPatient(string email, string password)
        {
            var patients = _context.Patients.Where(p => p.Email == email && p.Password == password);
            if (patients.Count() == 0)
                return null;
            else
            {
                var found = patients.First();
                if (found.IsGuest)
                    return null;
                return new PatientAccount(found.ToPatientAccountMemento());
            }
        }

        private DoctorAccount GetDoctor(string email, string password)
        {
            var doctors = _context.Doctors.Where(p => p.Email == email && p.Password == password);
            if (doctors.Count() == 0)
                return null;
            else
                return doctors.First().ToDoctorAccount();
        }

        private UserAccount GetAdmin(string email, string password)
        {
            var admins = _context.Admins.Where(p => p.Email == email && p.Password == password);
            if (admins.Count() == 0)
                return null;
            else
                return admins.First().ToUserAccount();
        }
    }
}
