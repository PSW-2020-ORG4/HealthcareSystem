﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Model;
using Backend.Model.Enums;
using Backend.Model.Exceptions;
using Model.Manager;
using Model.PerformingExamination;
using Model.Users;

namespace Backend.Repository.ExaminationRepository.MySqlExaminationRepository
{
    public class MySqlExaminationRepository : IExaminationRepository
    {
        private readonly MyDbContext _context;

        public MySqlExaminationRepository(MyDbContext context)
        {
            _context = context;
        }
        public void AddExamination(Examination examination)
        {
            _context.Examinations.Add(examination);
            _context.SaveChanges();
        }

        public List<Examination> GetAllExaminations()
        {
            throw new NotImplementedException();
        }

        public Examination GetExaminationById(int id)
        {      
            try
            {
                Examination examination = _context.Examinations.Find(id);
                if (examination == null)
                    throw new NotFoundException("Examination doesn't exist.");
                return _context.Examinations.Find(id);
            }
            catch (Exception)
            {
                throw new DatabaseException("The database connection is down.");
            }
        }

        public List<Examination> GetExaminationsByDate(DateTime date)
        {
            throw new NotImplementedException();
        }

        public List<Examination> GetExaminationsByPatient(string patientJmbg)
        {
            return _context.Examinations.Where(e => e.PatientCard.PatientJmbg == patientJmbg).ToList();
        }

        public List<Examination> GetExaminationsByRoomAndDates(int numberOfRoom, DateTime beginDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public void UpdateExamination(Examination examination)
        {
            _context.Examinations.Update(examination);
            _context.SaveChanges();
        }

        public List<Examination> GetCanceledExaminationsByPatient(string patientJmbg)
        {
            try
            {
                return _context.Examinations.Where(e => e.PatientCard.PatientJmbg == patientJmbg && e.ExaminationStatus == ExaminationStatus.CANCELED).ToList();
            }
            catch (Exception)
            {
                throw new DatabaseException("The database connection is down.");
            }
        }

        public List<Examination> GetPreviousExaminationsByPatient(string patientJmbg)
        {
            try
            {
                return _context.Examinations.Where(e => e.PatientCard.PatientJmbg == patientJmbg && e.ExaminationStatus == ExaminationStatus.FINISHED).ToList();
            }
            catch (Exception)
            {
                throw new DatabaseException("The database connection is down.");
            }
        }

        public List<Examination> GetFollowingExaminationsByPatient(string patientJmbg)
        {
            try
            {
                return _context.Examinations.Where(e => e.PatientCard.PatientJmbg == patientJmbg && e.ExaminationStatus == ExaminationStatus.CREATED).ToList();
            }
            catch (Exception)
            {
                throw new DatabaseException("The database connection is down.");
            }
        }
    }
}