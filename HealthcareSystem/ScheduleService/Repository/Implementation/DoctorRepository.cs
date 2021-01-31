using Backend.Model;
using ScheduleService.CustomException;
using ScheduleService.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ScheduleService.Repository.Implementation
{
    public class DoctorRepository : IDoctorRepository
    {
        private MyDbContext _context;

        public DoctorRepository(MyDbContext context)
        {
            _context = context;
        }

        public Doctor Get(string id)
        {
            try
            {
                var doctor = _context.Doctors.Find(id);
                if (doctor is null)
                    throw new NotFoundException("Doctor with jmbg " + id + " not found.");
                return new Doctor(id, doctor.Name, doctor.Surname);
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

        public Doctor Get(string id, DateTime startDate, DateTime endDate)
        {
            try
            {
                var doctor = _context.Doctors.Find(id);
                if (doctor is null)
                    throw new NotFoundException("Doctor with jmbg " + id + " not found.");
                var unavailable = GetUnavailableAppointments(id, startDate, endDate);
                return new Doctor(id, doctor.Name, doctor.Surname, unavailable);
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

        public ICollection<string> GetIdsBySpecialty(int specialtyId)
        {
            try
            {
                return _context.Doctors.Where(
                    d => d.CheckIfDoctorHasSpecialty(specialtyId)).Select(d => d.Jmbg).ToList();
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
                    e => e.DoctorJmbg.Equals(id)
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
