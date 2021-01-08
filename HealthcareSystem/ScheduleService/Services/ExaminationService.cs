using ScheduleService.DTO;
using ScheduleService.Model;
using ScheduleService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleService.Services
{
    public class ExaminationService : IExaminationService
    {
        private readonly IExaminationRepository _examinationRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IRoomRepository _roomRepository;

        public ExaminationService(IExaminationRepository examinationRepository, IPatientRepository patientRepository,
                                  IDoctorRepository doctorRepository, IRoomRepository roomRepository)
        {
            _examinationRepository = examinationRepository;
            _patientRepository = patientRepository;
            _doctorRepository = doctorRepository;
            _roomRepository = roomRepository;
        }
        public void Cancel(int id)
        {
            Examination examination = _examinationRepository.Get(id);
            examination.Cancel();
            _examinationRepository.Update(examination);
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

        public void Schedule(ExaminationDTO examinationDTO)
        {
            Patient patient = _patientRepository.Get(examinationDTO.PatientJmbg);
            Doctor doctor = _doctorRepository.Get(examinationDTO.DoctorJmbg);
            Room room = _roomRepository.Get(examinationDTO.RoomId);
            Examination examination = new Examination(examinationDTO.StartTime, patient, doctor, room);

            _examinationRepository.Add(examination);
        }
    }
}
