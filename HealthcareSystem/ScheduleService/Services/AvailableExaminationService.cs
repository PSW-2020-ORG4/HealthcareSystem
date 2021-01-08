using ScheduleService.DTO;
using ScheduleService.Model;
using ScheduleService.Model.DomainServices;
using ScheduleService.Repository;
using ScheduleService.Services.AdvancedSearchStrategy;
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
        public AvailableExaminationService(IPatientRepository patientRepository, IDoctorRepository doctorRepository, IRoomRepository roomRepository)
        {
            _patientRepository = patientRepository;
            _doctorRepository = doctorRepository;
            _roomRepository = roomRepository;
            _availableExaminationGenerator = new AvailableExaminationGenerator();
        }
        public IEnumerable<Examination> AdvancedSearch(AdvancedSearchDTO advancedSearchDTO)
        {
            IEnumerable<Examination> examinations = BasicSearch(advancedSearchDTO.InitialParameters);

            if (!examinations.Any() && advancedSearchDTO.Priority == SearchPriority.Time)
            {
                ICollection<string> doctors = _doctorRepository.GetIdsBySpecialty(advancedSearchDTO.SpecialtyId);
                TimeHasPriorityStrategy timePriority = new TimeHasPriorityStrategy(advancedSearchDTO.InitialParameters, doctors);
               
                return SearchAvailableExaminationsByTimePriority(timePriority);
            }
            else if (!examinations.Any() && advancedSearchDTO.Priority == SearchPriority.Doctor)
            {
                DoctorHasPriorityStrategy doctorPriority = new DoctorHasPriorityStrategy(advancedSearchDTO.InitialParameters);

                return SearchAvailableExaminationsByDoctorPriority(doctorPriority);
            }

            return examinations;
        }

        public IEnumerable<Examination> BasicSearch(BasicSearchDTO basicSearchDTO)
        {
            return _availableExaminationGenerator.Generate(GetExaminationGeneratorDTO(basicSearchDTO));
        }

        private IEnumerable<Examination> SearchAvailableExaminationsByDoctorPriority(DoctorHasPriorityStrategy doctorPriority)
        {
            BasicSearchDTO searchParameters = doctorPriority.GetSearchParameters();

            while (searchParameters != null)
            {
                IEnumerable<Examination> availableExaminations = BasicSearch(searchParameters);

                if (availableExaminations.Any())
                    return availableExaminations;

                searchParameters = doctorPriority.GetSearchParameters();
            }
            return Enumerable.Empty<Examination>();
        }

        private IEnumerable<Examination> SearchAvailableExaminationsByTimePriority(TimeHasPriorityStrategy timePriority)
        {
            BasicSearchDTO searchParameters = timePriority.GetSearchParameters();

            while (searchParameters != null)
            {
                IEnumerable<Examination> availableExaminations = BasicSearch(searchParameters);

                if (availableExaminations.Any())
                    return availableExaminations;

                searchParameters = timePriority.GetSearchParameters();
            }
            return Enumerable.Empty<Examination>();
        }

        private ExaminationGeneratorDTO GetExaminationGeneratorDTO(BasicSearchDTO basicSearchDTO)
        {
            Patient patient = _patientRepository.Get(basicSearchDTO.PatientJmbg, basicSearchDTO.EarliestDateTime, basicSearchDTO.LatestDateTime);
            Doctor doctor = _doctorRepository.Get(basicSearchDTO.DoctorJmbg, basicSearchDTO.EarliestDateTime, basicSearchDTO.LatestDateTime);
            IEnumerable<Room> rooms = _roomRepository.GetByEquipmentTypes(basicSearchDTO.RequiredEquipmentTypes, 
                                                      basicSearchDTO.EarliestDateTime, basicSearchDTO.LatestDateTime);
            

            ExaminationGeneratorDTO examinationGeneratorDTO = new ExaminationGeneratorDTO(patient, doctor, rooms, 
                                                                  basicSearchDTO.EarliestDateTime, basicSearchDTO.LatestDateTime);

            return examinationGeneratorDTO;
        }
    }
}
