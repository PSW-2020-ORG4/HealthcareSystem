﻿using Backend.Model;
using Backend.Model.Enums;
using Backend.Model.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
                patient.IsGuest = false;
                var existing = _context.Patients.Find(patient.Jmbg);
                if (existing != null && !existing.IsGuest)
                    throw new ValidationException("Patient with jmbg " + patient.Jmbg + "is already registered.");
                if (existing != null && existing.IsGuest)
                    _context.Remove(existing);
                _context.Patients.Add(patient);
                _context.SaveChanges();
                if (existing is null)
                {
                    _context.PatientCards.Add(new PatientCard()
                    {
                        PatientJmbg = patient.Jmbg,
                        Alergies = "",
                        BloodType = BloodType.UNKNOWN,
                        MedicalHistory = "",
                        RhFactor = RhFactorType.UNKNOWN,
                        Lbo = "",
                        HasInsurance = false
                    });
                    _context.SaveChanges();
                }
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

        public PatientAccount Get(string id)
        {
            try
            {
                var patient = _context.Patients.Find(id);
                if (patient is null)
                    throw new NotFoundException("Patient account with jmbg " + id + " does not exist.");
                if (patient.IsGuest)
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
                if (patient.IsGuest)
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
                var mementos = _context.Patients.Where(p => !p.IsGuest).Select(p => p.ToPatientAccountMemento());
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
                var mementos = _context.Patients.Where(p => !p.IsGuest).Select(
                    p => p.ToPatientAccountMemento()).ToList();
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
                if (patient.IsGuest)
                    throw new NotFoundException("Patient account with jmbg " + memento.Jmbg + " does not exist.");
                patient.IsActive = memento.IsActivated;
                patient.IsBlocked = memento.IsBlocked;
                patient.ImageName = memento.ImageName;
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
