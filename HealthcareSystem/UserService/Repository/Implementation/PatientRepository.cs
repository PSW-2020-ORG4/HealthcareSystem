using Backend.Model;
using Backend.Model.Enums;
using Backend.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.CustomException;
using UserService.Model;
using UserService.Model.Memento;

namespace UserService.Repository
{
    public class PatientRepository : IPatientRepository
    {
        private readonly MyDbContext _context;

        public PatientRepository(MyDbContext context)
        {
            _context = context;
        }

        public void Add(PatientAccount entity)
        {
            try
            {
                var patient = entity.ToBackendPatient();
                _context.Patients.Add(patient);
                _context.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new ValidationException(e.Message);
            }
            catch (Exception e)
            {
                throw new DataStorageException(e.Message);
            }
        }

        public PatientAccount Get(string id)
        {
            try
            {
                var patient = _context.Patients.Find(id);
                if (patient is null)
                    throw new NotFoundException("Patient account with jmbg " + id + " does not exist.");
                var memento = patient.ToPatientAccountMemento();
                return new PatientAccount(memento);
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

        public PatientAccount Get(string id, DateTime earliestMaliciousActionDate)
        {
            try
            {
                var patient = _context.Patients.Find(id);
                if (patient is null)
                    throw new NotFoundException("Patient account with jmbg " + id + " does not exist.");
                var memento = patient.ToPatientAccountMemento();
                memento.MaliciousActions = GetMaliciousActions(id, earliestMaliciousActionDate);
                return new PatientAccount(memento);
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

        public IEnumerable<PatientAccount> GetAll()
        {
            try
            {
                var mementos = _context.Patients.Select(p => p.ToPatientAccountMemento());
                return mementos.Select(m => new PatientAccount(m));
            }
            catch (Exception e)
            {
                throw new DataStorageException(e.Message);
            }
        }

        public IEnumerable<PatientAccount> GetAll(DateTime earliestMaliciousActionDate)
        {
            try
            {
                var mementos = _context.Patients.Select(p => p.ToPatientAccountMemento()).ToList();
                mementos.ForEach(m => m.MaliciousActions = GetMaliciousActions(m.Jmbg, earliestMaliciousActionDate));
                return mementos.Select(m => new PatientAccount(m));
            }
            catch (Exception e)
            {
                throw new DataStorageException(e.Message);
            }
        }

        public void Update(PatientAccount entity)
        {
            try
            {
                var memento = entity.GetPatientMemento();
                var patient = _context.Patients.Find(memento.Jmbg);
                if (patient is null)
                    throw new NotFoundException("Patient account with jmbg " + memento.Jmbg + " does not exist.");
                patient.IsActive = memento.IsActivated;
                patient.IsBlocked = memento.IsBlocked;
                _context.Update(patient);
                _context.SaveChanges();

            }
            catch (UserServiceException)
            {
                throw;
            }
            catch (DbUpdateException e)
            {
                throw new ValidationException(e.Message);
            }
            catch (Exception e)
            {
                throw new DataStorageException(e.Message);
            }
        }

        private IEnumerable<MaliciousActionMemento> GetMaliciousActions(string id, DateTime earliest)
        {
            try
            {
                var cancellations = _context.Examinations.Where(
                    e => e.DateAndTime >= earliest && e.ExaminationStatus == ExaminationStatus.CANCELED
                    && e.PatientCard.PatientJmbg == id);
                return cancellations.Select(c => new MaliciousActionMemento()
                {
                    Id = c.Id,
                    TimeStamp = c.DateAndTime,
                    Type = MaliciousActionType.AppointmentCancellation
                });
            }
            catch (Exception e)
            {
                throw new DataStorageException(e.Message);
            }
        }
    }
}
