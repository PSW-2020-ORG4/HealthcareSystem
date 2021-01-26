using Backend.Model.PerformingExamination;
using System.Collections.Generic;

namespace Backend.Service.ExaminationAndPatientCard
{
    public interface IEquipmentInExaminationService
    {
        EquipmentInExamination AddEquipmentInExamination(EquipmentInExamination equipmentInExamination);
        List<EquipmentInExamination> GetEquipmentInExaminationFromExaminationID(int examinationID);
    }
}
