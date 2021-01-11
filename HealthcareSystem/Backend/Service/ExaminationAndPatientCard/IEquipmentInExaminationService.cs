using Backend.Model.PerformingExamination;
using Backend.Repository.EquipmentInExaminationRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Service.ExaminationAndPatientCard
{
    public interface IEquipmentInExaminationService
    {
        EquipmentInExamination AddEquipmentInExamination(EquipmentInExamination equipmentInExamination);       
        List<EquipmentInExamination> GetEquipmentInExaminationFromExaminationID(int examinationID);
    }
}
