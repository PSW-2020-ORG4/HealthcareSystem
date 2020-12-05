using Backend.Model;
using Backend.Model.Enums;
using Backend.Model.Exceptions;
using Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
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
            _context.Patients.Add(patient);
            _context.SaveChanges();

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
            try
            {
                Patient patient = _context.Patients.Find(jmbg);
                if (patient == null)
                    throw new DatabaseException("Patient doesn't exist in database.");
                return _context.Patients.Find(jmbg);
            }
            catch (Exception)
            {
                throw new DatabaseException("The database connection is down.");
            }
        }
        public void UpdatePatient(Patient patient)
        {
            _context.Patients.Update(patient);
            _context.SaveChanges();
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
    }
}
