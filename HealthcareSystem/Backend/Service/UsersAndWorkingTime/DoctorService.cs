using Backend.Model.Exceptions;
using Backend.Repository;
using Backend.Service;
using Model.Users;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ValidationException = Backend.Model.Exceptions.ValidationException;


namespace Service.UsersAndWorkingTime
{
    public class DoctorService : IDoctorService
    {
        private IDoctorRepository _doctorRepository;

        public DoctorService(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public Doctor RegisterDoctor(Doctor doctor)
        {
            if (!IsUsernameValid(doctor.Username) || !IsPasswordValid(doctor.Password))
            {
                throw new ValidationException("Your username or password is incorrect. Please try again.");
            }
            _doctorRepository.AddDoctor(doctor);
            return doctor;
        }

        public Doctor EditDoctor(Doctor doctor)
        {
            if (!IsUsernameValid(doctor.Username) || !IsPasswordValid(doctor.Password)) return null;
            _doctorRepository.SetDoctor(doctor);
            return doctor;
        }

        public bool IsPasswordValid(string password)
        {
            Regex regex = new Regex(@"^[a-zA-Z0-9\.\-_]{8,30}$");
            Match match = regex.Match(password);

            return match.Success;
        }

        public bool IsUsernameValid(string username)
        {
            Regex regex = new Regex(@"^[a-zA-Z0-9\.\-_]{5,13}$");
            Match match = regex.Match(username);

            return match.Success;
        }

        public Doctor SignIn(string username, string password)
        {
            return _doctorRepository.CheckUsernameAndPassword(username, password);
        }

        public List<Doctor> ViewDoctors()
        {
            try
            {
                return _doctorRepository.GetAllDoctors();
            }
            catch (Exception)
            {
                throw new NotFoundException("Doctors don't exist in database.");
            }
        }

        public Doctor GetDoctorByJmbg(string jmbg)
        {
            return _doctorRepository.GetDoctorByJmbg(jmbg);
        }

        public List<Doctor> ViewDoctorsBySpecialty(int specialtyId)
        {
            return _doctorRepository.GetDoctorsBySpecialty(specialtyId);
        }
    }
}