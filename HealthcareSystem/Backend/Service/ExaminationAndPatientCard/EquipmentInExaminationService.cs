using Backend.Model.PerformingExamination;
using Backend.Repository.EquipmentInExaminationRepository;
using System.Collections.Generic;

namespace Backend.Service.ExaminationAndPatientCard
{
    public class EquipmentInExaminationService : IEquipmentInExaminationService
    {
        private IEquipmentInExaminationRepository _equipmentInExaminationRepository;
        public EquipmentInExaminationService(IEquipmentInExaminationRepository equipmentInExaminationRepository)
        {
            _equipmentInExaminationRepository = equipmentInExaminationRepository;
        }

        public EquipmentInExamination AddEquipmentInExamination(EquipmentInExamination equipmentInExamination)
        {
            return _equipmentInExaminationRepository.AddEquipmentInExamination(equipmentInExamination);
        }

        public List<EquipmentInExamination> GetEquipmentInExaminationFromExaminationID(int examinationID)
        {
            return _equipmentInExaminationRepository.GetEquipmentInExaminationByExaminationId(examinationID);
        }
    }
}
