using Backend.Model;
using Backend.Model.Exceptions;
using Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Repository
{
    public class MySqlDoctorRepository : IDoctorRepository
    {
        private readonly MyDbContext _context;

        public MySqlDoctorRepository(MyDbContext context)
        {
            _context = context;
        }

        public void AddDoctor(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            _context.SaveChanges();
        }

        public Doctor CheckUsernameAndPassword(string username, string password)
        {
            Doctor _doctor = _context.Doctors.Find(username);
            if (_doctor == null || !_doctor.Password.Equals(password))
                throw new NotFoundException("Doctor with username=" + username + " doesn't exist in database.");
            return _doctor;
        }

        public List<Doctor> GetAllDoctors()
        {
            return _context.Doctors.ToList();
        }

        public Doctor GetDoctorByJmbg(string jmbg)
        {
            return _context.Doctors.Find(jmbg);
        }

        public void SetDoctor(Doctor doctor)
        {
            _context.Doctors.Update(doctor);
            _context.SaveChanges();
        }
    }
}
