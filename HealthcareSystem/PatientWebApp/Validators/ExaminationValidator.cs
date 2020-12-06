using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Model.Exceptions;
using Backend.Service.ExaminationAndPatientCard;
using Model.PerformingExamination;


namespace PatientWebApp.Validators
{
    public class ExaminationValidator
    {
        private readonly IExaminationService _examinationService;

        public ExaminationValidator(IExaminationService examinationService)
        {
            _examinationService = examinationService;
        }


        public void CheckIfExaminationIsFinished(int id)
        {
            Examination examination = _examinationService.GetExaminationById(id);
            if (examination.ExaminationStatus != Backend.Model.Enums.ExaminationStatus.FINISHED)
                throw new ValidationException("Examination is not finished.");
        }

        public void CheckIfSurveyAboutExaminationIsCompleted(int id)
        {
            Examination examination = _examinationService.GetExaminationById(id);
            if (examination.IsSurveyCompleted)
                throw new ValidationException("The survey for this examination has already been completed.");
        }

        public void CheckIfExaminationCanBeCanceled(int id)
        {
            Examination examination = _examinationService.GetExaminationById(id);
            DateTime restrictDate = examination.DateAndTime.AddDays(-2);
            DateTime todayDate = DateTime.Now;

            if (examination.ExaminationStatus != Backend.Model.Enums.ExaminationStatus.CREATED)
                throw new ValidationException("Examination has already been finished or canceled");
            if (todayDate > restrictDate)
                throw new ValidationException("The examination is in less than 48 hours.");
        }

    }
}
