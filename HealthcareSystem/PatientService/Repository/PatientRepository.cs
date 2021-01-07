using Backend.Model;
using Backend.Repository;
using PatientService.CustomException;
using PatientService.Model;
using PatientService.Model.Memento;
using PatientService.Repository.BackendMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientService.Repository
{
    public class PatientRepository : IPatientRepository
    {
        private readonly Backend.Model.MyDbContext _context;

        public PatientRepository(MyDbContext context)
        {
            _context = context;
        }

        public Patient Get(string jmbg)
        {
            try
            {
                var patient = _context.Patients.Find(jmbg);
                if (patient is null)
                    throw new NotFoundException("Patient with jmbg " + jmbg + " not found.");
                return new Patient(new PatientMemento()
                {
                    Jmbg = patient.Jmbg,
                    Name = patient.Name,
                    Surname = patient.Surname,
                    Allergies = patient.PatientCard.Alergies,
                    MedicalHistory = patient.PatientCard.MedicalHistory,
                    InsuranceNumber = patient.PatientCard.Lbo,
                    BloodType = patient.PatientCard.BloodType.ToBloodType(),
                    RhFactor = patient.PatientCard.RhFactor.ToRhFactor(),
                    Examinations = patient.PatientCard.Examinations.Where(
                        e => e.ExaminationStatus == Backend.Model.Enums.ExaminationStatus.FINISHED).Select(
                        e => new ExaminationMemento()
                        {
                            Anamnesis = e.Anamnesis,
                            DateAndTime = e.DateAndTime,
                            DoctorJmbg = e.DoctorJmbg,
                            DoctorName = e.Doctor.Name,
                            DoctorSurname = e.Doctor.Surname,
                            Id = e.Id
                        }),
                    Therapies = _context.Therapies.Where(
                        t => t.Examination.PatientCard.Patient.Jmbg == jmbg).Select(
                        t => new TherapyMemento()
                        {
                            Id = t.Id,
                            DailyDose = t.DailyDose,
                            Diagnosis = t.Diagnosis,
                            DoctorJmbg = t.Examination.DoctorJmbg,
                            DoctorName = t.Examination.Doctor.Name,
                            DoctorSurname = t.Examination.Doctor.Surname,
                            DrugId = t.Drug.Id,
                            DrugName = t.Drug.Name,
                            EndDate = t.EndDate,
                            StartDate = t.StartDate
                        })
                });
            }
            catch (PatientServiceException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new DataStorageException(e.Message);
            }
        }

        public Patient GetLazy(string jmbg)
        {
            try
            {
                var patient = _context.Patients.Find(jmbg);
                if (patient is null)
                    throw new NotFoundException("Patient with jmbg " + jmbg + " not found.");
                return new Patient(new PatientMemento()
                {
                    Jmbg = patient.Jmbg,
                    Name = patient.Name,
                    Surname = patient.Surname,
                    Allergies = patient.PatientCard.Alergies,
                    MedicalHistory = patient.PatientCard.MedicalHistory,
                    InsuranceNumber = patient.PatientCard.Lbo,
                    BloodType = patient.PatientCard.BloodType.ToBloodType(),
                    RhFactor = patient.PatientCard.RhFactor.ToRhFactor()
                });
            }
            catch (PatientServiceException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new DataStorageException(e.Message);
            }
        }
    }
}
