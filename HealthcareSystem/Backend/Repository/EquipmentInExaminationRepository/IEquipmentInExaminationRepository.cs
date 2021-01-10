using Backend.Model.PerformingExamination;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Repository.EquipmentInExaminationRepository
{
    public interface IEquipmentInExaminationRepository
    {
        EquipmentInExamination AddEquipmentInExamination(EquipmentInExamination equipmentInExamination);
        List<EquipmentInExamination> GetEquipmentInExaminationByExaminationId(int examinationId);
    }
}
