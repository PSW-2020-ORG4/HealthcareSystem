using Backend.Model.PerformingExamination;
using System.Collections.Generic;

namespace Backend.Repository.EquipmentInExaminationRepository
{
    public interface IEquipmentInExaminationRepository
    {
        EquipmentInExamination AddEquipmentInExamination(EquipmentInExamination equipmentInExamination);
        List<EquipmentInExamination> GetEquipmentInExaminationByExaminationId(int examinationId);
    }
}
