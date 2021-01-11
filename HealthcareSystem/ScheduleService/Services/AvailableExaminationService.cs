using ScheduleService.DTO;
using ScheduleService.Model;
using ScheduleService.Model.DomainServices;
using ScheduleService.Repository;
using ScheduleService.Services.AdvancedSearchStrategy;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ScheduleService.Services
{
    public class AvailableExaminationService : IAvailableExaminationService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly AvailableExaminationGenerator _availableExaminationGenerator;
        private readonly IClock _clock;

        public AvailableExaminationService(IPatientRepository patientRepository,
                                           IDoctorRepository doctorRepository,
                                           IRoomRepository roomRepository,
                                           IClock clock)
        {
            _patientRepository = patientRepository;
            _doctorRepository = doctorRepository;
            _roomRepository = roomRepository;
            _clock = clock;
            _availableExaminationGenerator = new AvailableExaminationGenerator();
        }
        public IEnumerable<Examination> BasicSearch(BasicSearchDTO basicSearchDTO)
        {
            return _availableExaminationGenerator.Generate(GetExaminationGeneratorDTO(basicSearchDTO));
        }

        public IEnumerable<Examination> AdvancedSearch(AdvancedSearchDTO advancedSearchDTO)
        {
            IEnumerable<Examination> examinations = BasicSearch(advancedSearchDTO.InitialParameters);

            if (examinations.Any())
                return examinations;

            IAdvancedSearchStrategy strategy = GetAdvancedSearchStrategy(advancedSearchDTO);
            return ApplyStrategy(strategy);
        }

        private IAdvancedSearchStrategy GetAdvancedSearchStrategy(AdvancedSearchDTO advancedSearchDTO)
        {
            if (advancedSearchDTO.Priority == SearchPriority.Time)
            {
                ICollection<string> doctors = _doctorRepository.GetIdsBySpecialty(advancedSearchDTO.SpecialtyId);
                return new TimeHasPriorityStrategy(advancedSearchDTO.InitialParameters, doctors);
            }
            else
            {
                return new DoctorHasPriorityStrategy(advancedSearchDTO.InitialParameters, 5);
            }
        }

        private IEnumerable<Examination> ApplyStrategy(IAdvancedSearchStrategy strategy)
        {
            for (BasicSearchDTO parameters = strategy.GetSearchParameters();
                 parameters != null;
                 parameters = strategy.GetSearchParameters())
            {
                IEnumerable<Examination> availableExaminations = BasicSearch(parameters);

                if (availableExaminations.Any())
                    return availableExaminations;
            }

            return Enumerable.Empty<Examination>();
        }

        private ExaminationGeneratorDTO GetExaminationGeneratorDTO(BasicSearchDTO basicSearchDTO)
        {
            Patient patient = _patientRepository.Get(basicSearchDTO.PatientJmbg,
                                                     basicSearchDTO.EarliestDateTime,
                                                     basicSearchDTO.LatestDateTime);
            Doctor doctor = _doctorRepository.Get(basicSearchDTO.DoctorJmbg,
                                                  basicSearchDTO.EarliestDateTime,
                                                  basicSearchDTO.LatestDateTime);
            IEnumerable<Room> rooms = _roomRepository.GetByEquipmentTypes(RoomType.Examination,
                                                                          basicSearchDTO.RequiredEquipmentTypes,
                                                                          basicSearchDTO.EarliestDateTime,
                                                                          basicSearchDTO.LatestDateTime);
            ExaminationGeneratorDTO examinationGeneratorDTO = new ExaminationGeneratorDTO(
                                                              patient,
                                                              doctor,
                                                              rooms,
                                                              FixTime(basicSearchDTO.EarliestDateTime),
                                                              FixTime(basicSearchDTO.LatestDateTime));
            return examinationGeneratorDTO;
        }

        private DateTime FixTime(DateTime time)
        {
            if (time < _clock.GetTimeLimit())
                return _clock.GetTimeLimit();
            else
                return time;
        }
    }
}
