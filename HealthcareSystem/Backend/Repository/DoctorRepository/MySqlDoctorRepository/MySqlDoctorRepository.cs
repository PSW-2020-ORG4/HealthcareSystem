using Backend.Model;
using Backend.Model.Exceptions;
using Backend.Model.Users;
using Backend.Repository.SpecialtyRepository;
using Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public List<Doctor> GetDoctorsBySpecialty(int specialtyId)
        {
            return _context.Doctors.Where(
                doctor => doctor.DoctorSpecialties.Any(specialty => specialty.SpecialtyId == specialtyId)).ToList();

        }        

        public Doctor GetDoctorByJmbg(string jmbg)
        {
            Doctor doctor;
            try
            {
                doctor = _context.Doctors.Find(jmbg);
            }
            catch (Exception)
            {
                throw new DatabaseException("The database connection is down.");
            }

            if (doctor == null)
                throw new NotFoundException("Doctor doesn't exist in database.");

            return doctor;
        }

        public void SetDoctor(Doctor doctor)
        {
            _context.Doctors.Update(doctor);
            _context.SaveChanges();
        }

        public bool CheckIfDoctorExists(string jmbg)
        {
            if (_context.Doctors.Find(jmbg) == null)
                return false;

            return true;
        }        
    }
}
