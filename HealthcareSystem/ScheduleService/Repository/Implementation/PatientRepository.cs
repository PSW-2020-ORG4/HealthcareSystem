using Backend.Model;
using ScheduleService.CustomException;
using ScheduleService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleService.Repository.Implementation
{
    public class PatientRepository : IPatientRepository
    {
        private MyDbContext _context;

        public PatientRepository(MyDbContext context)
        {
            _context = context;
        }


        public Patient Get(string id)
        {
            try
            {
                var patient = _context.Patients.Find(id);
                if (patient is null)
                    throw new NotFoundException("Patient with jmbg " + id + " not found.");
                return new Patient(id, patient.Name, patient.Surname);
            }
            catch (ScheduleServiceException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new DataStorageException(e.Message);
            }
        }

        public Patient Get(string id, DateTime startDate, DateTime endDate)
        {
            try
            {
                var patient = _context.Patients.Find(id);
                if (patient is null)
                    throw new NotFoundException("Patient with jmbg " + id + " not found.");
                var unavailable = GetUnavailableAppointments(id, startDate, endDate);
                return new Patient(id, patient.Name, patient.Surname, unavailable);
            }
            catch (ScheduleServiceException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new DataStorageException(e.Message);
            }
        }

        private IEnumerable<DateTime> GetUnavailableAppointments(string id, DateTime start, DateTime end)
        {
            try
            {
                return _context.Examinations.Where(
                    e => e.PatientCard.PatientJmbg.Equals(id)
                    && e.DateAndTime <= end
                    && e.DateAndTime >= start
                    && e.ExaminationStatus != Backend.Model.Enums.ExaminationStatus.CANCELED).Select(
                    e => e.DateAndTime);
            }
            catch (Exception e)
            {
                throw new DataStorageException(e.Message);
            }
        }
    }
}
