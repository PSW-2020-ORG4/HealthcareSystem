using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
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

        public void CheckIfExaminationCanBeCanceled(int id)
        {
            Examination examination = _examinationService.GetExaminationById(id);
            if (examination.ExaminationStatus != Backend.Model.Enums.ExaminationStatus.CREATED)
                throw new ValidationException("Examination can't be canceled.");
        }
    }
}
