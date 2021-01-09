using ScheduleService.CustomException;
using ScheduleService.DTO;
using ScheduleService.Model;
using ScheduleService.Repository;
using System;
using System.Collections.Generic;

namespace ScheduleService.Services
{
    public class ExaminationService : IExaminationService
    {
        private readonly IExaminationRepository _examinationRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IClock _clock;

        public ExaminationService(IExaminationRepository examinationRepository,
                                  IPatientRepository patientRepository,
                                  IDoctorRepository doctorRepository,
                                  IRoomRepository roomRepository,
                                  IClock clock)
        {
            _examinationRepository = examinationRepository;
            _patientRepository = patientRepository;
            _doctorRepository = doctorRepository;
            _roomRepository = roomRepository;
            _clock = clock;
        }

        public IEnumerable<Examination> GetCanceledByPatient(string jmbg)
        {
            return _examinationRepository.GetByExaminationStatusAndPatient(ExaminationStatus.Canceled, jmbg);
        }

        public IEnumerable<Examination> GetCreatedByPatient(string jmbg)
        {
            return _examinationRepository.GetByExaminationStatusAndPatient(ExaminationStatus.Created, jmbg);
        }

        public IEnumerable<Examination> GetFinishedByPatient(string jmbg)
        {
            return _examinationRepository.GetByExaminationStatusAndPatient(ExaminationStatus.Finished, jmbg);
        }

        public void Cancel(int id)
        {
            Examination examination = _examinationRepository.Get(id);
            examination.Cancel(_clock.GetTimeLimit());
            _examinationRepository.Update(examination);
        }

        public void Schedule(ScheduleExaminationDTO examinationDTO)
        {
            DateTime startTime = examinationDTO.StartTime.AddHours(-1);
            DateTime endTime = examinationDTO.StartTime.AddHours(1);
            Patient patient = _patientRepository.Get(examinationDTO.PatientJmbg, startTime, endTime);
            Doctor doctor = _doctorRepository.Get(examinationDTO.DoctorJmbg, startTime, endTime);
            Room room = _roomRepository.Get(examinationDTO.RoomId, startTime, endTime);
            Examination examination = new Examination(examinationDTO.StartTime, patient, doctor, room);

            if (!examination.IsAvailable())
                throw new ValidationException("Examination is not available.");
            if (examination.IsBefore(_clock.GetTimeLimit()))
                throw new ValidationException("The time limit for scheduling the examinaton has passed.");

            _examinationRepository.Add(examination);
        }
    }
}
