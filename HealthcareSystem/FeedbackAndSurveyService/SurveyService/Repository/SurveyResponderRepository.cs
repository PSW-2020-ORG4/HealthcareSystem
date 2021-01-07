using Backend.Model;
using Backend.Model.Enums;
using FeedbackAndSurveyService.CustomException;
using FeedbackAndSurveyService.SurveyService.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackAndSurveyService.SurveyService.Repository
{
    public class SurveyResponderRepository : ISurveyResponderRepository
    {
        private readonly MyDbContext _context;

        public SurveyResponderRepository(MyDbContext context)
        {
            _context = context;
        }

        public SurveyResponder Get(string id)
        {
            try
            {
                var examinations = _context.Examinations.Where(e => e.PatientCard.Patient.Jmbg == id &&
                                                                   e.ExaminationStatus == ExaminationStatus.FINISHED &&
                                                                   !e.IsSurveyCompleted);
                var permissions = examinations.Select(e => new SurveyPermission(e.Id, e.DoctorJmbg));
                return new SurveyResponder(id, permissions.ToList());
            }
            catch (Exception e)
            {
                throw new DataStorageException(e.Message);
            }
        }

        public void Update(SurveyResponder entity)
        {
            try
            {
                var memento = entity.GetMemento();
                foreach (SurveyResponse response in memento.Responses)
                    AddResponse(response);
                _context.SaveChanges();
            }
            catch (FeedbackAndSurveyServiceException)
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

        private void AddResponse(SurveyResponse response)
        {
            var examination = _context.Examinations.Find(response.UsedPermission.Id);
            if (examination == null)
                throw new ActionNotPermittedException("Permission with the id " + response.UsedPermission.Id + " does not exist.");
            examination.IsSurveyCompleted = true;
            _context.Update(examination);
            _context.Add(new SurveyAboutDoctor()
            {
                ExaminationId = examination.Id,
                AvailabilityOfDoctor = response.DoctorSurveyResponse.Availability.Value,
                BehaviorOfDoctor = response.DoctorSurveyResponse.Behavior.Value,
                GettingAdviceByDoctor = response.DoctorSurveyResponse.GivingAdvice.Value,
                DoctorProfessionalism = response.DoctorSurveyResponse.Professionalism.Value
            });
            _context.Add(new SurveyAboutHospital()
            {
                ExaminationId = examination.Id,
                Cleanliness = response.HospitalSurveyResponse.Cleanliness.Value,
                Nursing = response.HospitalSurveyResponse.Nursing.Value,
                OverallRating = response.HospitalSurveyResponse.General.Value,
                SatisfiedWithDrugAndInstrument = response.HospitalSurveyResponse.MedicationAndInstrumments.Value
            });
            _context.Add(new SurveyAboutMedicalStaff()
            {
                ExaminationId = examination.Id,
                BehaviorOfMedicalStaff = response.MedicalStaffSurveyResponse.Behavior.Value,
                EaseInObtainingFollowUpInformation = response.MedicalStaffSurveyResponse.FollowUp.Value,
                GettingAdviceByMedicalStaff = response.MedicalStaffSurveyResponse.GivingAdvice.Value,
                MedicalStaffProfessionalism = response.MedicalStaffSurveyResponse.Professionalism.Value
            });
        }
    }
}
