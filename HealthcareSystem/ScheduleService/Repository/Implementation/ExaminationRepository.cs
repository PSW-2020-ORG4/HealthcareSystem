using Backend.Model;
using Microsoft.EntityFrameworkCore;
using ScheduleService.CustomException;
using ScheduleService.Model;
using ScheduleService.Model.Memento;
using ScheduleService.Repository.BackendMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleService.Repository.Implementation
{
    public class ExaminationRepository : IExaminationRepository
    {
        private MyDbContext _context;
        private IRoomRepository _roomRepository;
        private IPatientRepository _patientRepository;
        private IDoctorRepository _doctorRepository;

        public ExaminationRepository(MyDbContext context,
                                     IRoomRepository roomRepository,
                                     IPatientRepository patientRepository,
                                     IDoctorRepository doctorRepository)
        {
            _context = context;
            _roomRepository = roomRepository;
            _patientRepository = patientRepository;
            _doctorRepository = doctorRepository;
        }

        public void Add(Examination examination)
        {
            try
            {
                var memento = examination.GetMemento();
                var patientCard = _context.PatientCards.Where(c => c.PatientJmbg == memento.Patient.Jmbg.Value).FirstOrDefault();
                var backendExamination = new Backend.Model.PerformingExamination.Examination()
                {
                    DoctorJmbg = memento.Doctor.Jmbg.Value,
                    DateAndTime = memento.Appointment,
                    IdRoom = memento.Room.Id,
                    IdPatientCard = patientCard.Id,
                    IsSurveyCompleted = false,
                    ExaminationStatus = memento.ExaminationStatus.ToBackendExaminationStatus(),
                    Type = memento.ExaminationType.ToBackendExaminationType()
                };
                _context.Add(backendExamination);
                _context.SaveChanges();
            }
            catch (ScheduleServiceException)
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

        public Examination Get(int id)
        {
            try
            {
                var exam = _context.Examinations.Find(id);
                if (exam is null)
                    throw new NotFoundException("Examination with id " + id + " not found.");
                return new Examination(new ExaminationMemento()
                {
                    Appointment = exam.DateAndTime,
                    Doctor = _doctorRepository.Get(exam.DoctorJmbg),
                    Patient = _patientRepository.Get(exam.PatientCard.PatientJmbg),
                    Room = _roomRepository.Get(exam.Room.Id),
                    Id = exam.Id,
                    ExaminationStatus = exam.ExaminationStatus.ToExaminationStatus(),
                    ExaminationType = exam.Type.ToExaminationType()
                });
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

        public IEnumerable<Examination> GetByExaminationStatusAndPatient(ExaminationStatus examinationStatus, string patientId)
        {
            try
            {
                var exams = _context.Examinations.Where(e => e.PatientCard.PatientJmbg.Equals(patientId)
                            && e.ExaminationStatus == examinationStatus.ToBackendExaminationStatus());
                return exams.Select(exam => new Examination(new ExaminationMemento()
                {
                    Appointment = exam.DateAndTime,
                    Doctor = _doctorRepository.Get(exam.DoctorJmbg),
                    Patient = _patientRepository.Get(exam.PatientCard.PatientJmbg),
                    Room = _roomRepository.Get(exam.Room.Id),
                    Id = exam.Id,
                    ExaminationStatus = exam.ExaminationStatus.ToExaminationStatus(),
                    ExaminationType = exam.Type.ToExaminationType()
                }));
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

        public void Update(Examination examination)
        {
            try
            {
                var memento = examination.GetMemento();
                var exam = _context.Examinations.Find(memento.Id);
                if (exam is null)
                    throw new NotFoundException("Examination with id " + memento.Id + " not found.");
                exam.ExaminationStatus = memento.ExaminationStatus.ToBackendExaminationStatus();
                _context.Update(exam);
            }
            catch (ScheduleServiceException)
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
    }
}
