using Backend.Model;
using Backend.Model.Enums;
using Backend.Model.Exceptions;
using Backend.Model.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Repository
{
    public class MySqlActivePatientRepository : IActivePatientRepository
    {
        private readonly MyDbContext _context;
        public MySqlActivePatientRepository(MyDbContext context)
        {
            _context = context;
        }
        public void AddPatient(Patient patient)
        {
            try
            {
                _context.Patients.Add(patient);
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw new BadRequestException("Username or jmbg already exists.");
            }
            catch (Exception)
            {
                throw new DatabaseException("The database connection is down.");
            }

        }
        public Patient CheckUsernameAndPassword(string username, string password)
        {
            Patient _patient = _context.Patients.Find(username);
            if (_patient == null || !_patient.Password.Equals(password))
            {
                return null;
            }
            return _patient;
        }
        public void DeletePatient(string jmbg)
        {
            Patient patient = GetPatientByJmbg(jmbg);
            _context.Remove(patient);
            _context.SaveChanges();
        }
        public List<Patient> GetAllPatients()
        {
            return _context.Patients.ToList();
        }

        public Patient GetPatientByJmbg(string jmbg)
        {
            Patient patient;
            try
            {
                patient = _context.Patients.Find(jmbg);
            }
            catch (Exception)
            {
                throw new DatabaseException("The database connection is down.");
            }
            if (patient == null)
                throw new NotFoundException("Patient doesn't exist in database.");
            return patient;
        }

        public void UpdatePatient(Patient patient)
        {
            try
            {
                _context.Patients.Update(patient);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw new DatabaseException("The database connection is down.");
            }
        }
        public int GetNumberOfCanceledExaminations(string jmbg)
        {
            try
            {
                return _context.Examinations.Count(e => e.PatientCard.PatientJmbg == jmbg &&
                e.ExaminationStatus == ExaminationStatus.CANCELED && e.DateAndTime >= DateTime.Now.AddMonths(-1));
            }
            catch (Exception)
            {
                throw new DatabaseException("The database connection is down.");
            }
        }
        public Patient GetPatientByUsernameAndPassword(string username, string password)
        {
            Patient patient;
            try
            {
                patient = _context.Patients.Where(p => p.Username == username && p.Password == password).FirstOrDefault();
            }
            catch (Exception)
            {
                throw new DatabaseException("The database connection is down.");
            }

            if (patient == null)
                throw new NotFoundException("Patient doesn't exist in database.");

            if (patient.IsBlocked)
                throw new BadRequestException("Patient is blocked.");

            if (!patient.IsActive)
                throw new BadRequestException("Patient didn't activate account.");

            return patient;
        }

        public Patient GetPatientByPatientCardId(int patientCardId)
        {
            Patient patient;
            try
            {
                patient = _context.Patients.Where(p => p.PatientCard.Id == patientCardId).FirstOrDefault();
            }
            catch (Exception)
            {
                throw new DatabaseException("The database connection is down.");
            }

            if (patient == null)
                throw new NotFoundException("Patient doesn't exist in database.");

            if (patient.IsBlocked)
                throw new BadRequestException("Patient is blocked.");

            return patient;
        }
    }
}
